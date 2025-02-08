using stocks.Models;

namespace stocks.Interfaces;

public interface IPortfolioRepository
{ 
     Task<List<Stock>> GetPortfolioAsync(string userId);
     Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio);
     Task<Portfolio?> DeletePortfolioAsync(Guid portfolioId);
}