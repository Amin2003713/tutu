namespace Application.Order.CommandAndQueries;

public class DecreaseOrderItemCountCommand
{
    public long UserId { get; set; }
    public long ItemId { get; set; }
    public int Count { get; set; }
}