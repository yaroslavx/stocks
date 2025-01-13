using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stocks.Data;
using stocks.Dtos.Stock;
using stocks.Interfaces;
using stocks.Mappers;

namespace stocks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IStockRepository _stockRepository;

    public StockController(ApplicationDbContext context, IStockRepository stockRepository)
    {
        _context = context;
        _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _stockRepository.GetAllAsync();

        var stockDtos = stocks.Select(s => s.ToStockDto());
        
        return Ok(stockDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int  id)
    {
        var stock = await _context.Stocks.FindAsync(id);

        if (stock == null)
        {
            return NotFound();
        }
        
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto createStockDto)
    {
        var stockModel = createStockDto.ToStockFromCreateDto();
        
        await _context.Stocks.AddAsync(stockModel);
        
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateStockRequestDto updateStockDto)
    {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

        if (stockModel == null)
        {
            return NotFound();
        }
        
        stockModel.Symbol = updateStockDto.Symbol;
        stockModel.CompanyName = updateStockDto.CompanyName;
        stockModel.Purchase = updateStockDto.Purchase;
        stockModel.LastDiv = updateStockDto.LastDiv;
        stockModel.Industry = updateStockDto.Industry;
        stockModel.MarketCap = updateStockDto.MarketCap;
        
        await _context.SaveChangesAsync();
        
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        
        if (stockModel == null)
        {
            return NotFound();
        }
        
        _context.Stocks.Remove(stockModel);
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}