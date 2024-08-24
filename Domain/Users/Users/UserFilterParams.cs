﻿using Domain.Common.Filter;

namespace Application.Users.Users.Users;

public class UserFilterParams:BaseFilterParam
{
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public long? Id { get; set; }
}