using System.ComponentModel.DataAnnotations;

namespace stocks.Dtos.Comment;

public class UpdateCommentRequestDto
{
    [Required]
    [MaxLength(150, ErrorMessage = "Content must not be longer than 300 characters.")]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(300, ErrorMessage = "Content must not be longer than 300 characters.")]
    public string Content { get; set; } = string.Empty;
}