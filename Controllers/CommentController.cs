using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using stocks.Data;
using stocks.Dtos.Comment;
using stocks.Extensions;
using stocks.Interfaces;
using stocks.Mappers;
using stocks.Models;

namespace stocks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IStockRepository _stockRepository;
    private readonly UserManager<AppUser> _userManager;

    public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository, UserManager<AppUser> userManager)
    {
        _commentRepository = commentRepository;
        _stockRepository = stockRepository;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var comments = await _commentRepository.GetAllAsync();

        var commentDtos = comments.Select(c => c.ToCommentDto());
        
        return Ok(commentDtos);
    }
    
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid  id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var stock = await _commentRepository.GetByIdAsync(id);
        
        if (stock == null)
        {
            return NotFound("Comment not found");
        }
        
        return Ok(stock.ToCommentDto());
    }

    [HttpPost("{stockId:Guid}")]
    public async Task<IActionResult> Create([FromRoute] Guid stockId, [FromBody] CreateCommentRequestDto createCommentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!await _stockRepository.StockExists(stockId))
        {
            return BadRequest("Stock does not exist");
        }

        var username = User.GetUsername();
        var user = await _userManager.FindByNameAsync(username);

        var commentModel = createCommentDto.ToCommentFromCreateDto(stockId);
        commentModel.AppUserId = user.Id;
        
        await _commentRepository.CreateAsync(commentModel);
        
        return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCommentRequestDto updateCommentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var commentModel = await _commentRepository.UpdateAsync(id, updateCommentDto.ToCommentFromUpdateDto());

        if (commentModel == null)
        {
            return NotFound("Comment not found");
        }
        
        return Ok(commentModel.ToCommentDto());
    }
    
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var comment = await _commentRepository.DeleteAsync(id);

        if (comment == null)
        {
            return NotFound("Comment not found");
        }
        
        return Ok(comment.ToCommentDto());
    }
}