using CommentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentSystem.Application.Abstractions
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Post> Posts { get; }
        DbSet<Comment> Comments { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
