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


    public CreateUserAddressCommand ToAddCommand() =>
        new()
        {
            Shire = this.Shire,
            City = this.City,
            PostalCode = this.PostalCode,
            PostalAddress = this.PostalAddress,
            PhoneNumber = this.PhoneNumber,
            Name = this.Name,
            Family = this.Family,
            NationalCode = this.NationalCode,
        };

    public EditUserAddressCommand ToEditCommand() =>
        new()
        {
            Shire = this.Shire,
            City = this.City,
            PostalCode = this.PostalCode,
            PostalAddress = this.PostalAddress,
            PhoneNumber = this.PhoneNumber,
            Name = this.Name,
            Family = this.Family,
            NationalCode = this.NationalCode,
            Id = this.Id
        };
}