using stocks.Models;

namespace stocks.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync();
}