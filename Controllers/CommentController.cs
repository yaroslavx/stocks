using Microsoft.AspNetCore.Mvc;
using stocks.Data;
using stocks.Interfaces;
using stocks.Mappers;

namespace stocks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ICommentRepository _commentRepository;

    public CommentController(ApplicationDbContext context, ICommentRepository commentRepository)
    {
        _context = context;
        _commentRepository = commentRepository;
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
            return NotFound();
        }
        
        return Ok(stock.ToCommentDto());
    }
}