using CommentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentSystem.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.UserId);

            builder.Property(x => x.UserId)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.DisplayName)
                .HasMaxLength(100);

            builder.Property(x => x.AvatarUrl)
                .HasMaxLength(500);
        }
    }
}
