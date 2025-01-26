using Microsoft.AspNetCore.Mvc;
using stocks.Data;
using stocks.Dtos.Comment;
using stocks.Interfaces;
using stocks.Mappers;

namespace stocks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IStockRepository _stockRepository;

    public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
    {
        _commentRepository = commentRepository;
        _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _commentRepository.GetAllAsync();

        var commentDtos = comments.Select(c => c.ToCommentDto());
        
        return Ok(commentDtos);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid  id)
    {
        var stock = await _commentRepository.GetByIdAsync(id);
        
        if (stock == null)
        {
            return NotFound("Comment not found");
        }
        
        return Ok(stock.ToCommentDto());
    }

    [HttpPost("{stockId}")]
    public async Task<IActionResult> Create([FromRoute] Guid stockId, [FromBody] CreateCommentRequestDto createCommentDto)
    {
        if (!await _stockRepository.StockExists(stockId))
        {
            return BadRequest("Stock does not exist");
        }

        var commentModel = createCommentDto.ToCommentFromCreateDto(stockId);

        await _commentRepository.CreateAsync(commentModel);
        
        return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCommentRequestDto updateCommentDto)
    {
        var commentModel = await _commentRepository.UpdateAsync(id, updateCommentDto.ToCommentFromUpdateDto());

        if (commentModel == null)
        {
            return NotFound("Comment not found");
        }
        
        return Ok(commentModel.ToCommentDto());
    }
}