﻿namespace Domain.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base("The requested resource was not found.")
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public NotFoundException(string entityName, object key)
        : base($"The {entityName} with key '{key}' was not found.")
    {
    }
}