using Microsoft.AspNetCore.Mvc;
using stocks.Data;
using stocks.Dtos.Stock;
using stocks.Mappers;

namespace stocks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public StockController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var stocks = _context.Stocks.ToList()
            .Select(s => s.ToStockDto());
        
        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int  id)
    {
        var stock = _context.Stocks.Find(id);

        if (stock == null)
        {
            return NotFound();
        }
        
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDto();
        
        _context.Stocks.Add(stockModel);
        
        _context.SaveChanges();
        
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }
}