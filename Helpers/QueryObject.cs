namespace stocks.Helpers;

public class QueryObject
{
    public string? Symbol { get; set; } = null;
    public string? CompanyName { get; set; } = null;
    public string SortBy { get; set; } = null;
    public bool IsDecsending { get; set; } = false;
    public int Current { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}