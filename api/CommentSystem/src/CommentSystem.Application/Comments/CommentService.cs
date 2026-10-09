using CommentSystem.Application.Abstractions;
using CommentSystem.Application.Comments.DTOs;
using CommentSystem.Application.Interfaces;
using CommentSystem.Domain.Entities;
using CommentSystem.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CommentSystem.Application.Comments
{
    public class CommentService : ICommentService
    {
        private const int MaxContentLength = 500;
        private const int DefaultPageSize = 20;
        private const int MaxPageSize = 50;

        private readonly IAppDbContext _context;
        private readonly ILogger<CommentService> _logger;

        public CommentService(IAppDbContext context, ILogger<CommentService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<CommentPageDto?> GetByPostAsync(long postId, long? before, int? limit, CancellationToken cancellationToken = default)
        {
            if (!await PostExistsAsync(postId, cancellationToken))
            {
                _logger.LogWarning("Comments requested for missing post {PostId}", postId);
                return null;
            }

            var pageSize = Math.Clamp(limit ?? DefaultPageSize, 1, MaxPageSize);

            var query = _context.Comments
                .AsNoTracking()
                .Where(x => x.PostId == postId);

            if (before.HasValue)
            {
                query = query.Where(x => x.CommentId < before.Value);
            }

            // fetch one extra row to know whether another page exists
            var items = await query
                .OrderByDescending(x => x.CommentId)
                .Take(pageSize + 1)
                .Select(CommentMapping.ToDto)
                .ToListAsync(cancellationToken);

            long? nextCursor = null;
            if (items.Count > pageSize)
            {
                items.RemoveAt(pageSize);
                nextCursor = items[^1].CommentId;
            }

            return new CommentPageDto { Items = items, NextCursor = nextCursor };
        }

        public async Task<CommentDto?> CreateAsync(long postId, long userId, string? content, CancellationToken cancellationToken = default)
        {
            var text = content?.Trim() ?? string.Empty;

            if (text.Length == 0 || text.Length > MaxContentLength)
            {
                _logger.LogWarning(
                    "Rejected comment from user {UserId} on post {PostId}: length {Length} is outside 1-{Max}",
                    userId, postId, text.Length, MaxContentLength);

                throw new CommentOperationException($"Comment must be 1-{MaxContentLength} characters");
            }

            if (!await PostExistsAsync(postId, cancellationToken))
            {
                _logger.LogWarning("User {UserId} tried to comment on missing post {PostId}", userId, postId);
                return null;
            }

            var comment = new Comment
            {
                PostId = postId,
                UserId = userId,
                Content = text,
                CreatedAt = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "User {UserId} added comment {CommentId} to post {PostId}",
                userId, comment.CommentId, postId);

            return await _context.Comments
                .AsNoTracking()
                .Where(x => x.CommentId == comment.CommentId)
                .Select(CommentMapping.ToDto)
                .FirstAsync(cancellationToken);
        }

        private Task<bool> PostExistsAsync(long postId, CancellationToken cancellationToken)
        {
            return _context.Posts
                .AsNoTracking()
                .AnyAsync(x => x.PostId == postId, cancellationToken);
        }
    }
}
