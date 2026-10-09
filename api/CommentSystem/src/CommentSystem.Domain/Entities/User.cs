namespace CommentSystem.Domain.Entities
{
    public class User
    {
        public long UserId { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
