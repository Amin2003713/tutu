﻿namespace Application.Roles.CommandAndQueries;

public class EditRoleCommand
{
    public long Id { get; set; }
    public string Title { get; set; }
    public List<Permission> Permissions { get; set; }
}