using System.ComponentModel.DataAnnotations;

namespace stocks.Dtos.Comment;

public class CreateCommentRequestDto
{
    [Required]
    [MinLength(2, ErrorMessage = "Title must be at least 2 characters long.")]
    [MaxLength(150, ErrorMessage = "Content must not be longer than 300 characters.")]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [MinLength(2, ErrorMessage = "Content must be at least 2 characters long.")]
    [MaxLength(300, ErrorMessage = "Content must not be longer than 300 characters.")]
    public string Content { get; set; } = string.Empty;
}
