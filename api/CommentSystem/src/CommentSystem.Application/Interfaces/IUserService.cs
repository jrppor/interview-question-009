using CommentSystem.Application.Posts.DTOs;

namespace CommentSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetByIdAsync(long userId, CancellationToken cancellationToken = default);
    }
}
