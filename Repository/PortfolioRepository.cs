using Microsoft.EntityFrameworkCore;
using stocks.Data;
using stocks.Interfaces;
using stocks.Models;

namespace stocks.Repository;

public class PortfolioRepository : IPortfolioRepository
{
    private readonly ApplicationDbContext _context;
    
    public PortfolioRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Stock>> GetUserPortfolio(string userId)
    {
        return await _context.Portfolios.Where(u => u.AppUserId == userId)
            .Select(portfolio => new Stock
            {
                Id = portfolio.Stock.Id,
                Symbol = portfolio.Stock.Symbol,
                CompanyName = portfolio.Stock.CompanyName,
                Purchase = portfolio.Stock.Purchase,
                LastDiv = portfolio.Stock.LastDiv,
                Industry = portfolio.Stock.Industry,
                MarketCap = portfolio.Stock.MarketCap,
            }).ToListAsync();
    }
}