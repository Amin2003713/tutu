namespace Infra;

public static class OrderRoutes
{
    public static string GetOrdersList = "api/Order";
    public static string GetFilteredCurrentOrders = "api/Order/current/filter";
    public static string GetCurrentOrder = "api/Order/current";
    public static string GetOrderById = "api/Order/";

    public static string PostOrder = "api/Order";
    public static string PostOrderCheckout = "api/Order/Checkout";

    public static string PutSendOrderById = "api/Order/SendOrder/";
    public static string PutIncreaseOrderItemCount = "api/Order/orderItem/IncreaseCount";
    public static string PutDecreaseOrderItemCount = "api/Order/orderItem/DecreaseCount";

    public static string DeleteOrderItemById = "api/Order/orderItem/";
}