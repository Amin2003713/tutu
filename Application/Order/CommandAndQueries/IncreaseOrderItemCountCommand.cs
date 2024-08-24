namespace Application.Order.CommandAndQueries;

public class IncreaseOrderItemCountCommand
{
    public long UserId { get; set; }
    public long ItemId { get; set; }
    public int Count { get; set; }
}