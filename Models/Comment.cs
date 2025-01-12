namespace stocks.Models;

public class Comment
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;
    
    public DateTime Created { get; set; } = DateTime.Now;
    
    public Guid? StockId { get; set; }
    
    public Stock? Stock { get; set; }
}