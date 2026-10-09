namespace CommentSystem.WebApi
{
    /// <summary>
    /// Stand-in for authentication: every comment is written by this user.
    /// </summary>
    public class CurrentUserSettings
    {
        public const string SectionName = "CurrentUser";

        public long UserId { get; set; }
    }
}
