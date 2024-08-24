namespace Application.ShippingMethods.Responses;

public class ShippingMethodDto : BaseDto
{
    public string Title { get; set; }
    public int Cost { get; set; }
}