public class CommentFilterParams : BaseFilterParam
{
    public long? UserId { get; set; }
    public long? ProductId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public CommentStatus? CommentStatus { get; set; }

}


public class ProductCommentsFilterParams : BaseFilterParam
{
  
    public long? ProductId { get; set; }

}