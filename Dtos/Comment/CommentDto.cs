namespace stocks.Dtos.Comment;

public class CommentDto
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;
    
    public DateTime Created { get; set; } = DateTime.Now;
    
    public string CreatedBy { get; set; } = string.Empty;
    
    public Guid? StockId { get; set; }
}