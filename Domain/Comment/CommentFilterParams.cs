using Domain.Common.Filter;

namespace Domain.Comment;

public class CommentFilterParams : BaseFilterParam
{
    public long? UserId { get; set; }
    public long? ProductId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public CommentStatus? CommentStatus { get; set; }

}