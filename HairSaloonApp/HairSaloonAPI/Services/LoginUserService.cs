using System.Security.Cryptography;
using HairSaloonAPI.Data;
using HairSaloonAPI.Interfaces;
using HairSaloonAPI.Interfaces.Services;
using HairSaloonAPI.Models;
using HairSaloonAPI.Models.DTOs;

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

    public int Login(LoginUserDTO userData)
    {
        if (!CheckIfUsernameExist(userData.UserName))
        {
            throw new BadHttpRequestException("We haven't found a user with this username");
        }

        if (!VerifyPasswordHash(userData.Password, userData.UserName))
        {
            throw new BadHttpRequestException("The password is incorrect");
        }

        return _db.Users.First(u => u.Username == userData.UserName).Id;
    }
}