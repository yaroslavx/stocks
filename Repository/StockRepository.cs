using Microsoft.EntityFrameworkCore;
using stocks.Data;
using stocks.Dtos.Stock;
using stocks.Interfaces;
using stocks.Models;

namespace stocks.Repository;

public class StockRepository : IStockRepository
{
    private readonly ApplicationDbContext _context;
    
    public StockRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Stock>> GetAllAsync()
    {
        return await _context.Stocks.Include(s => s.Comments).ToListAsync();
    }
    
    public async Task<Stock?> GetByIdAsync(Guid id) 
    {
        return await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Stock> CreateAsync(Stock stock)
    {
        await _context.Stocks.AddAsync(stock);
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> UpdateAsync(Guid id, UpdateStockRequestDto stock)
    {
        var stockToUpdate = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

        if (stockToUpdate == null)
        {
            return null;
        }
        
        stockToUpdate.Symbol = stock.Symbol;
        stockToUpdate.CompanyName = stock.CompanyName;
        stockToUpdate.Purchase = stock.Purchase;
        stockToUpdate.LastDiv = stock.LastDiv;
        stockToUpdate.Industry = stock.Industry;
        stockToUpdate.MarketCap = stock.MarketCap;
        
        await _context.SaveChangesAsync();
        
        return stockToUpdate;
    }

    public async Task<Stock?> DeleteAsync(Guid id)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

        if (stock == null)
        {
            return null;
        }
        
        _context.Stocks.Remove(stock);
        await _context.SaveChangesAsync();  
        return stock;
    }

    public async Task<bool> StockExists(Guid id)
    {
        return await _context.Stocks.AnyAsync(s => s.Id == id);
    }
}