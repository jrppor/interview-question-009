using CommentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentSystem.Infrastructure.Data.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");

            builder.HasKey(x => x.PostId);

            builder.Property(x => x.PostId)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ImageUrl)
                .HasMaxLength(500);

            builder.Property(x => x.Caption)
                .HasMaxLength(1000);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);
        }
    }
}
