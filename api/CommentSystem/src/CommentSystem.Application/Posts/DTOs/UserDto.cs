namespace CommentSystem.Application.Posts.DTOs
{
    public class UserDto
    {
        public long UserId { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
    }
}
