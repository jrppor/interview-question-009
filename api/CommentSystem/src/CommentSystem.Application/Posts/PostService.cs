using CommentSystem.Application.Abstractions;
using CommentSystem.Application.Interfaces;
using CommentSystem.Application.Posts.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CommentSystem.Application.Posts
{
    public class PostService : IPostService
    {
        private readonly IAppDbContext _context;

        public PostService(IAppDbContext context)
        {
            _context = context;
        }

        public Task<PostDto?> GetByIdAsync(long postId, CancellationToken cancellationToken = default)
        {
            return _context.Posts
                .AsNoTracking()
                .Where(x => x.PostId == postId)
                .Select(PostMapping.ToDto)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
