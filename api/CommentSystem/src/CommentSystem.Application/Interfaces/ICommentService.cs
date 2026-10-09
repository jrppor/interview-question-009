using CommentSystem.Application.Comments.DTOs;

namespace CommentSystem.Application.Interfaces
{
    public interface ICommentService
    {
        Task<CommentPageDto?> GetByPostAsync(long postId, long? before, int? limit, CancellationToken cancellationToken = default);

        Task<CommentDto?> CreateAsync(long postId, long userId, string? content, CancellationToken cancellationToken = default);
    }
}
