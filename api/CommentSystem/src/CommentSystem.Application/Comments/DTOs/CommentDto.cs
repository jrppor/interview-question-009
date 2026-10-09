using CommentSystem.Application.Posts.DTOs;

namespace CommentSystem.Application.Comments.DTOs
{
    public class CommentDto
    {
        public long CommentId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public UserDto User { get; set; } = new();
    }
}
