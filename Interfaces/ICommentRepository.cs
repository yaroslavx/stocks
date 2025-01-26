using stocks.Dtos.Comment;
using stocks.Models;

namespace stocks.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
    
    Task<Comment?> GetByIdAsync(Guid id);
    
    Task<Comment> CreateAsync(Comment comment);
    
    Task<Comment?> UpdateAsync(Guid id, Comment comment);

    Task<Comment?> DeleteAsync(Guid id);
}