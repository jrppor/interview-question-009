using CommentSystem.Application.Abstractions;
using CommentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommentSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>((provider, options) =>
            {
                var connectionString = provider.GetRequiredService<IConfiguration>()
                    .GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found");

                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());

            return services;
        }
    }
}
