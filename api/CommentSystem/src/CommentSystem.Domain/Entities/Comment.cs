namespace CommentSystem.Domain.Entities
{
    public class Comment
    {
        public long CommentId { get; set; }
        public long PostId { get; set; }
        public long UserId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public Post Post { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
