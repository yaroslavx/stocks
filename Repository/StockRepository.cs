using Microsoft.EntityFrameworkCore;
using stocks.Data;
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
    
    public Task<List<Stock>> GetAllAsync()
    {
        return _context.Stocks.ToListAsync();
    }
}