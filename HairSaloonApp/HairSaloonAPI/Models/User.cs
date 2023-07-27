using HairSaloonAPI.Interfaces;

namespace HairSaloonAPI.Models;

public class User : IUser
{
    public int _id { get; }
    public string _username { get; set; } = string.Empty;
    public byte[] _passwordHash { get; set; } = new byte[32];
    public byte[] _passwordSalt { get; set; } = new byte[32];
}