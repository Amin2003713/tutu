using Application.Order.CommandAndQueries;
using Application.Order.Responses;
using Domain.Orders;

namespace Application.Order.Interfaces;

public interface IOrderService
{
    Task<ApiResult?> AddOrderItem(AddOrderItemCommand command);
    Task<ApiResult?> CheckoutOrder(CheckOutOrderCommand command);
    Task<ApiResult?> IncreaseOrderItem(IncreaseOrderItemCountCommand command);
    Task<ApiResult> DecreaseOrderItem(DecreaseOrderItemCountCommand command);
    Task<ApiResult?> DeleteOrderItem(DeleteOrderItemCommand command);
    Task<ApiResult?> SendOrder(long orderId);


    Task<ApiResult<OrderDto?>?> GetOrderById(long orderId);
    Task<ApiResult<OrderDto?>?> GetCurrentOrder();
    Task<ApiResult<OrderFilterResult>?> GetOrders(OrderFilterParams filterParams);
    Task<ApiResult<OrderFilterResult>?> GetUserOrders(int pageId, int take, OrderStatus? orderStatus);
}