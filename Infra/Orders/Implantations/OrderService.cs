using Application.Common;
using Application.Order.CommandAndQueries;
using Application.Order.Interfaces;
using Application.Order.Responses;
using Domain.Common.Api;
using Domain.Orders;
using Infra.Utils;

namespace Infra.Orders.Implantations;

public class OrderService(IBaseHttpClient client) : IOrderService
{
    public async Task<ApiResult?> AddOrderItem(AddOrderItemCommand command)
    {
        return await client.PostAsync<AddOrderItemCommand, ApiResult>
            (OrderRoutes.PostOrder, command);
    }

    public async Task<ApiResult?> CheckoutOrder(CheckOutOrderCommand command)
    {
        return await client.PostAsync<CheckOutOrderCommand, ApiResult>(OrderRoutes.PostOrderCheckout, command);
    }

    public async Task<ApiResult?> IncreaseOrderItem(IncreaseOrderItemCountCommand command)
    {
        return await client.PutAsync<IncreaseOrderItemCountCommand, ApiResult>(OrderRoutes.PutIncreaseOrderItemCount,
            command);
    }

    public async Task<ApiResult> DecreaseOrderItem(DecreaseOrderItemCountCommand command)
    {
        return (await client.PutAsync<DecreaseOrderItemCountCommand, ApiResult>(OrderRoutes.PutDecreaseOrderItemCount,
            command))!;
    }

    public async Task<ApiResult?> DeleteOrderItem(DeleteOrderItemCommand command)
    {
        return await client.DeleteAsync<ApiResult>(
            OrderRoutes.DeleteOrderItemById.BuildRequestUrl([command.OrderItemId])!);
    }

    public async Task<ApiResult?> SendOrder(long orderId)
    {
        return await client.PutAsync<object, ApiResult>
            (OrderRoutes.PutSendOrderById.BuildRequestUrl([orderId])!, null!);
    }

    public async Task<ApiResult<OrderDto?>?> GetOrderById(long orderId)
    {
        return await client.GetAsync<ApiResult<OrderDto?>>(OrderRoutes.GetOrderById.BuildRequestUrl([orderId])!);
    }

    public async Task<ApiResult<OrderDto?>?> GetCurrentOrder()
    {
        return await client.GetAsync<ApiResult<OrderDto?>>(OrderRoutes.GetCurrentOrder);
    }

    public async Task<ApiResult<OrderFilterResult>?> GetOrders(OrderFilterParams filterParams)
    {
        return await client.GetAsync<ApiResult<OrderFilterResult>>(OrderRoutes.GetFilteredCurrentOrders.ToQueryString(
            filterParams)!);
    }

    public async Task<ApiResult<OrderFilterResult>?> GetUserOrders(int pageId, int take, OrderStatus? orderStatus)
    {
        var requestParams = new List<Dictionary<string, string>>
        {
            new() { { "pageId", pageId.ToString() } },
            new() { { "take", take.ToString() } },
            new() { { "status", (orderStatus is null ? null : orderStatus.ToString())! } }
        };

        return await client.GetAsync<ApiResult<OrderFilterResult>>(
            OrderRoutes.GetFilteredCurrentOrders.BuildRequestUrl(requestParams)!);
    }
}