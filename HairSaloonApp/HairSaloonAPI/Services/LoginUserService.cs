using System.Security.Cryptography;
using HairSaloonAPI.Data;
using HairSaloonAPI.Interfaces.Services;

namespace HairSaloonAPI.Services;

public class LoginUserService : ILoginUserService
{
    private DataContext _db;

    public LoginUserService(DataContext db)
    {
        _db = db;
    }

    public bool CheckIfUsernameExist(string username)
    {
        if (_db.Users.Any(u => u.Username == username))
        {
            return true;
        }

        return false;
    }

    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);
        }
    }
}