using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stocks.Data;
using stocks.Dtos.Comment;
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
        return await _context.Comments.Include(a => a.AppUser).ToListAsync();
    }

    public Task<Comment?> GetByIdAsync(Guid id)
    {
        var comment = _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);
 
        return comment;
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> UpdateAsync(Guid id, Comment comment)
    {
        var commentToUpdate = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

        if (commentToUpdate == null)
        {
            return null;
        }
        
        commentToUpdate.Title = comment.Title;
        commentToUpdate.Content = comment.Content;
        
        await _context.SaveChangesAsync();
        
        return commentToUpdate;
    }

    public async Task<Comment?> DeleteAsync(Guid id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

        if (comment == null)
        {
            return null;
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return comment;
    }
}