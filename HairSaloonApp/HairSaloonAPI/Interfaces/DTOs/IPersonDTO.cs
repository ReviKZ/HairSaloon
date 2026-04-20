namespace HairSaloonAPI.Interfaces.DTOs;

/// Author:
/// Kovács Zoárd Gábor
/// A6I2XW
public interface IPersonDTO
{
    public string FirstName { get; }
    public string LastName { get; }
    public string PhoneNumber { get; }
    public string EmailAddress { get; }
}