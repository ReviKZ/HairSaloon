using HairSaloonAPI.Enums;
using HairSaloonAPI.Interfaces;

namespace HairSaloonAPI.Models;

public class Person : IPerson
{
    public int _id { get; set; }
    public IUser _user { get; set; }
    public string _firstName { get; set; }
    public string _lastName { get; set; }
    public Gender _gender { get; set; }
    public PersonType _type { get; set; }
    public string _phoneNumber { get; set; }
    public string _emailAddress { get; set; }
}