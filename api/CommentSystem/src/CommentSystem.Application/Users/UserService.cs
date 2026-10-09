using CommentSystem.Application.Abstractions;
using CommentSystem.Application.Interfaces;
using CommentSystem.Application.Posts.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CommentSystem.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(IAppDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<UserDto?> GetByIdAsync(long userId, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => new UserDto
                {
                    UserId = x.UserId,
                    DisplayName = x.DisplayName,
                    AvatarUrl = x.AvatarUrl
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
            {
                _logger.LogWarning("User {UserId} not found - check CurrentUser:UserId in appsettings", userId);
            }

            return user;
        }
    }
}
