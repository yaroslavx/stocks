using System.ComponentModel.DataAnnotations.Schema;

namespace stocks.Models;

[Table("Stocks")]
public class Stock
{
    public Guid Id { get; set; }
    
    public string Symbol { get; set; } = string.Empty;
    
    public string CompanyName { get; set; } = string.Empty;
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Purchase { get; set; }
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal LastDiv { get; set; }
    
    public string Industry { get; set; } = string.Empty;
    
    public long MarketCap { get; set; }
    
    public List<Comment> Comments { get; set; } = new List<Comment>();
    
    public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
}