using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stocks.Data;
using stocks.Interfaces;
using stocks.Models;

namespace stocks.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context;
    
    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Comment>> GetAllAsync()
    {
        return await _context.Comments.ToListAsync();
    }

    public Task<Comment?> GetByIdAsync([FromRoute]Guid id)
    {
        var comment = _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
 
        return comment;
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;
    }
}