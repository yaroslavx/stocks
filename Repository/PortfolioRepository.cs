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
    
    public async Task<List<Stock>> GetPortfolioAsync(string userId)
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

    public async Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio)
    {
        await _context.Portfolios.AddAsync(portfolio);
        await _context.SaveChangesAsync();
        return portfolio;
    }

    public async Task<Portfolio?> DeletePortfolioAsync(Guid portfolioId)
    {
        var portfolio = await _context.Portfolios.FirstOrDefaultAsync(x => x.Stock.Id == portfolioId);

        if (portfolio == null)
        {
            return null;
        }
        
        _context.Portfolios.Remove(portfolio);
        await _context.SaveChangesAsync();
        return portfolio;
    }
}