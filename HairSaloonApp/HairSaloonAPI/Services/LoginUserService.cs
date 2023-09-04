using System.Security.Cryptography;
using HairSaloonAPI.Data;
using HairSaloonAPI.Interfaces.Services;
using HairSaloonAPI.Models;

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

    public bool VerifyPasswordHash(string password, string username)
    {
        User user = _db.Users.First(u => u.Username == username);
        using (var hmac = new HMACSHA512(user.PasswordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(user.PasswordHash);
        }
    }
}