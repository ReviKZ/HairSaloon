using System.Text.RegularExpressions;
using HairSaloonAPI.Enums;

namespace HairSaloonAPI.Interfaces;

public interface IPerson
{
    public int _id { get; }
    public IUser _user { get; }
    public string firstName { get; }
    public string lastName { get; }
    public Gender _gender { get; }
    public PersonType _type { get; }
    public string _phoneNumber { get; }
    public string _emailAddress { get; }
}