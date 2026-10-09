namespace CommentSystem.Domain.Entities
{
    public class Post
    {
        public long PostId { get; set; }
        public long UserId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string? Caption { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } = null!;
    }
}
