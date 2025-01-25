using stocks.Dtos.Stock;
using stocks.Models;

namespace stocks.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync();
    Task<Stock?> GetByIdAsync(Guid id);
    Task<Stock> CreateAsync(Stock stock);
    Task<Stock?> UpdateAsync(Guid id, UpdateStockRequestDto stock);
    Task<Stock?> DeleteAsync(Guid id);
}