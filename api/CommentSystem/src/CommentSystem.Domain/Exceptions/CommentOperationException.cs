namespace CommentSystem.Domain.Exceptions
{
    public class CommentOperationException : Exception
    {
        public CommentOperationException(string message) : base(message)
        {
        }
    }
}
