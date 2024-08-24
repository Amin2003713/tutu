using Domain.Common.Filter;

namespace Domain.Comment;

public class ProductCommentsFilterParams : BaseFilterParam
{
  
    public long? ProductId { get; set; }

}