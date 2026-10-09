using CommentSystem.Application.Comments;
using CommentSystem.Application.Interfaces;
using CommentSystem.Application.Posts;
using CommentSystem.Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace CommentSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
