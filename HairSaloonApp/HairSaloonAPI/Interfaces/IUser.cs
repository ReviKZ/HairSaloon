namespace HairSaloonAPI.Interfaces;

public interface IUser
{
    public int Id { get; }
    public string Username { get; }
    public byte[] PasswordHash { get; }
    public byte[] PasswordSalt { get; }
    public string Token { get; }
}