using stocks.Dtos.Comment;
using stocks.Models;

namespace stocks.Mappers;

public static class CommentMapper
{
    public static CommentDto ToCommentDto(this Comment commentModel)
    {
        return new CommentDto()
        {
            Id = commentModel.Id,
            Title = commentModel.Title,
            Content = commentModel.Content,
            Created = commentModel.Created,
            StockId = commentModel.StockId,
        };
    }
}