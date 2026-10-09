using CommentSystem.Application.Posts.DTOs;
using CommentSystem.Domain.Entities;
using System.Linq.Expressions;

namespace CommentSystem.Application.Posts
{
    public static class PostMapping
    {
        public static readonly Expression<Func<Post, PostDto>> ToDto = post => new PostDto
        {
            PostId = post.PostId,
            ImageUrl = post.ImageUrl,
            Caption = post.Caption,
            CreatedAt = post.CreatedAt,
            Author = new UserDto
            {
                UserId = post.User.UserId,
                DisplayName = post.User.DisplayName,
                AvatarUrl = post.User.AvatarUrl
            }
        };
    }
}
