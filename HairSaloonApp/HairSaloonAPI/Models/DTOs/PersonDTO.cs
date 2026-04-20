using HairSaloonAPI.Interfaces.DTOs;

namespace HairSaloonAPI.Models.DTOs;

/// Author:
/// Kovács Zoárd Gábor
/// A6I2XW
public class PersonDTO : IPersonDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
}