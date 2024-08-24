using Application.Comment.CommandAndQueries;
using Application.Comment.Responses;

namespace Application.Comment.Interfaces;

public interface ICommentRepository
{
    Task<ApiResult?> ChangeStatus(ChangeCommentStatusCommand command);
    Task<ApiResult?> CreateComment(CreateCommentCommand command);
    Task<ApiResult?> EditComment(EditCommentCommand command);
    Task<ApiResult> DeleteComment(DeleteCommentCommand command);


    Task<ApiResult<CommentFilterResult>> GetProductComments(ProductCommentsFilterParams filterParams);
    Task<ApiResult<CommentDto?>> GetCommentById(long id);
}