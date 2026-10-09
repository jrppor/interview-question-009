using System.ComponentModel.DataAnnotations;

namespace CommentSystem.WebApi
{
    public class CorsSettings
    {
        public const string SectionName = "Cors";

        [Required(ErrorMessage = "Cors:AllowedOrigins must be configured with at least one origin")]
        [MinLength(1, ErrorMessage = "Cors:AllowedOrigins must contain at least one origin")]
        public string[] AllowedOrigins { get; set; } = [];
    }
}
