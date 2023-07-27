using System.Text.RegularExpressions;
using HairSaloonAPI.Enums;
using HairSaloonAPI.Models;

namespace HairSaloonAPI.Interfaces;

public interface IPerson
{
    public int Id { get; }
    public User User { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public Gender Gender { get; }
    public PersonType Type { get; }
    public string PhoneNumber { get; }
    public string EmailAddress { get; }
}