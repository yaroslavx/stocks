using stocks.Dtos.Comment;
using stocks.Models;

namespace stocks.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
    
    Task<Comment?> GetByIdAsync(Guid id);
    
    Task<Comment> CreateAsync(Comment comment);
}