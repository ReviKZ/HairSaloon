using HairSaloonAPI.Interfaces.DTOs;

namespace HairSaloonAPI.Models.DTOs;

public class PersonDTO : IPersonDTO
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string _phoneNumber { get; set; }
    public string _emailAddress { get; set; }
}