using stocks.Dtos.Stock;
using stocks.Helpers;
using stocks.Models;

namespace stocks.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync(QueryObject query);
    Task<Stock?> GetByIdAsync(Guid id);    
    Task<Stock?> GetBySymbolAsync(string symbol);
    Task<Stock> CreateAsync(Stock stock);
    Task<Stock?> UpdateAsync(Guid id, Stock stock);
    Task<Stock?> DeleteAsync(Guid id);
    Task<bool> StockExists(Guid id);
}