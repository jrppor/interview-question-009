namespace CommentSystem.Application.Comments.DTOs
{
    public class CommentPageDto
    {
        public List<CommentDto> Items { get; set; } = [];
        public long? NextCursor { get; set; }
    }
}
