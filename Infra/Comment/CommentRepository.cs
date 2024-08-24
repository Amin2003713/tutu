using Application.Common;
using Domain.Common;
using Domain.Utils;
using Infra.Category;

namespace Infra.Comment;

public class CommentRepository(IBaseHttpClient client)  : ICommentRepository
{
    public async Task<ApiResult?> ChangeStatus(ChangeCommentStatusCommand command)
    {
        return await client.PutAsync<ChangeCommentStatusCommand, ApiResult>(CommentRoutes.PutCommentStatusChange, command);
    }

    public async Task<ApiResult?> CreateComment(CreateCommentCommand command)
    {
        return await client.PostAsync<CreateCommentCommand, ApiResult>(CommentRoutes.PostComment , command);
    }

    public async Task<ApiResult?> EditComment(EditCommentCommand command)
    {
        return await client.PutAsync<EditCommentCommand, ApiResult>(CommentRoutes.PutComment, command);
    }

    public async Task<ApiResult> DeleteComment(DeleteCommentCommand command)
    {
        return (await client.DeleteAsync<ApiResult?>
            (CommentRoutes.DeleteCommentById.BuildRequestUrl([command.CommentId])!))!;
    }

  

    public async Task<ApiResult<CommentFilterResult>> GetProductComments(ProductCommentsFilterParams filterParams)
    {
        var apiDictionary = new List<Dictionary<string, string>>()
        {
            new() { { "ProductId", filterParams.ProductId.ToString()! } },
            new() { { "PageId", filterParams.PageId.ToString()! } },
            new() { { "Take", filterParams.Take.ToString()! } },
        };

        return (await client.GetAsync<ApiResult<CommentFilterResult>>
            (CommentRoutes.GetProductComments.BuildRequestUrl(apiDictionary)!))!;

    }


    public async Task<ApiResult<CommentDto?>> GetCommentById(long id)
    {
        return (await client.GetAsync<ApiResult<CommentDto?>>
            (CommentRoutes.GetCommentsList.BuildRequestUrl([id])!))!;
    }


    public async Task<ApiResult<CommentFilterResult>> GetCommentsByFilter(CommentFilterParams filterParams)
    {
        var apiDictionary = new List<Dictionary<string, string>>()
        {
            new() { { "UserId", filterParams.UserId.ToString()! } },
            new() { { "ProductId", filterParams.ProductId.ToString()! } },
            new() { { "StartDate", filterParams.StartDate.ToString()! } },
            new() { { "EndDate", filterParams.EndDate.ToString()! } },
            new() { { "CommentStatus", filterParams.CommentStatus.ToString()! } },
            new() { { "PageId", filterParams.PageId.ToString()! } },
            new() { { "Take", filterParams.Take.ToString()! } },
        };

        return (await client.DeleteAsync<ApiResult<CommentFilterResult>>
            (CommentRoutes.GetCommentsList.BuildRequestUrl(apiDictionary)!))!;
    }
}