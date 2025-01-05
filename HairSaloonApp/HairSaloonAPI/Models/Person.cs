using System.ComponentModel.DataAnnotations;
using HairSaloonAPI.Enums;
using HairSaloonAPI.Interfaces;

namespace HairSaloonAPI.Models;

public class Person : IPerson
{
    [Key]
    public int Id { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public PersonType Type { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
}