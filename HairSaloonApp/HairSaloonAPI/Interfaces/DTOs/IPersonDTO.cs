namespace HairSaloonAPI.Interfaces.DTOs;

public interface IPersonDTO
{
    public string FirstName { get; }
    public string LastName { get; }
    public string PhoneNumber { get; }
    public string EmailAddress { get; }
}