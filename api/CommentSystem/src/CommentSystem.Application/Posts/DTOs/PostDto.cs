namespace CommentSystem.Application.Posts.DTOs
{
    public class PostDto
    {
        public long PostId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string? Caption { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDto Author { get; set; } = new();
    }
}
