using CommentSystem.Application.Comments.DTOs;
using CommentSystem.Application.Posts.DTOs;
using CommentSystem.Domain.Entities;
using System.Linq.Expressions;

namespace CommentSystem.Application.Comments
{
    public static class CommentMapping
    {
        public static readonly Expression<Func<Comment, CommentDto>> ToDto = comment => new CommentDto
        {
            CommentId = comment.CommentId,
            Content = comment.Content,
            CreatedAt = comment.CreatedAt,
            User = new UserDto
            {
                UserId = comment.User.UserId,
                DisplayName = comment.User.DisplayName,
                AvatarUrl = comment.User.AvatarUrl
            }
        };
    }
}
