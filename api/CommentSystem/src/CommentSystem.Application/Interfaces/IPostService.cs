using CommentSystem.Application.Posts.DTOs;

namespace CommentSystem.Application.Interfaces
{
    public interface IPostService
    {
        Task<PostDto?> GetByIdAsync(long postId, CancellationToken cancellationToken = default);
    }
}
