﻿using Domain.Common.Filter;

namespace Domain.Orders;

public class OrderFilterParams : BaseFilterParam
{
    public long? UserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public OrderStatus? Status { get; set; }
}