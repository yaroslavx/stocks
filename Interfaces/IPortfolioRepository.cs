using stocks.Models;

namespace stocks.Interfaces;

public interface IPortfolioRepository
{
    public Task<List<Stock>> GetUserPortfolio(string userId);
}