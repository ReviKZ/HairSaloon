using System.Text.RegularExpressions;
using HairSaloonAPI.Enums;
using HairSaloonAPI.Models;

namespace HairSaloonAPI.Interfaces;

public interface IPerson
{
    public int _id { get; }
    public User _user { get; }
    public string _firstName { get; }
    public string _lastName { get; }
    public Gender _gender { get; }
    public PersonType _type { get; }
    public string _phoneNumber { get; }
    public string _emailAddress { get; }
}