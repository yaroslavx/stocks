using stocks.Models;

namespace stocks.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
}