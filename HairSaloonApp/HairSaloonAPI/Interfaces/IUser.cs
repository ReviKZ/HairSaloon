namespace HairSaloonAPI.Interfaces;

public interface IUser
{
    public int _id { get; }
    public string _username { get; }
    public byte[] _passwordHash { get; }
    public byte[] _passwordSalt { get; }
}