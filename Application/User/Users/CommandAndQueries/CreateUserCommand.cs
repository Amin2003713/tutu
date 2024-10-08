﻿using Domain.User.Users;

namespace Application.User.Users.CommandAndQueries;

public class CreateUserCommand
{
    public string Name { get; set; }
    public string Family { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Gender Gender { get; set; }
}