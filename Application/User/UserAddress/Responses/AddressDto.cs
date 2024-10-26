﻿using Application.User.UserAddress.CommandAndQueries;

namespace Application.User.UserAddress.Responses;

public class AddressDto : BaseDto
{
    public long UserId { get; set; }
    public string Shire { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string PostalAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    public string NationalCode { get; set; }
    public bool ActiveAddress { get; set; }



    public string ContactName()
    {
        return Name ;
    }
    public string FullAddress()
    {
        return $"{PostalAddress}";
    }


    public static AddressDto ToDto(CreateUserAddressCommand command, long userId) =>
        new()
        {
            UserId = userId,
            Shire = command.Shire,
            City = command.City,
            PostalCode = command.PostalCode,
            PostalAddress = command.PostalAddress,
            PhoneNumber = command.PhoneNumber,
            Name = command.Name,
            Family = command.Family,
            NationalCode = command.NationalCode,
        };
}