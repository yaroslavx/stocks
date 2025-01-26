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
    private readonly IStockRepository _stockRepository;

    public StockController(IStockRepository stockRepository)
    {
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
    public async Task<IActionResult> GetById([FromRoute] Guid  id)
    {
        var stock = await _stockRepository.GetByIdAsync(id);
        
        if (stock == null)
        {
            return NotFound("Stock not found");
        }
        
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto createStockDto)
    {
        var stockModel = createStockDto.ToStockFromCreateDto();

        await _stockRepository.CreateAsync(stockModel);
        
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateStockRequestDto updateStockDto)
    {
        var stockModel = await _stockRepository.UpdateAsync(id, updateStockDto.ToStockFromUpdateDto());

        if (stockModel == null)
        {
            return NotFound("Stock not found");
        }
        
        return Ok(stockModel.ToStockDto());
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var stockModel = await _stockRepository.DeleteAsync(id);
        
        if (stockModel == null)
        {
            return NotFound("Stock not found");
        }
        
        return NoContent();
    }
}