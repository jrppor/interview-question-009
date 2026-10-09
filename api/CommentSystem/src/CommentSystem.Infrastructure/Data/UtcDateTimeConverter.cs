using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CommentSystem.Infrastructure.Data
{
    /// <summary>
    /// Columns hold UTC but SQL Server returns Kind = Unspecified, which would
    /// serialize without a "Z" and be read as local time by the browser.
    /// </summary>
    public class UtcDateTimeConverter : ValueConverter<DateTime, DateTime>
    {
        public UtcDateTimeConverter()
            : base(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
        {
        }
    }
}
