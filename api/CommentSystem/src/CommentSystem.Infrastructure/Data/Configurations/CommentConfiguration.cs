using CommentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentSystem.Infrastructure.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(x => x.CommentId);

            builder.Property(x => x.CommentId)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Content)
                .HasMaxLength(500);

            builder.HasOne(x => x.Post)
                .WithMany()
                .HasForeignKey(x => x.PostId);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            builder.HasIndex(x => new { x.PostId, x.CommentId })
                .IsDescending(false, true)
                .HasDatabaseName("IX_Comments_PostId_CommentId");
        }
    }
}
