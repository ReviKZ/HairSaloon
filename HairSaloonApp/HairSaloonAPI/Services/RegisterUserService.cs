using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using HairSaloonAPI.Data;
using HairSaloonAPI.Interfaces.DTOs;
using HairSaloonAPI.Interfaces.Services;
using HairSaloonAPI.Models;
using BadHttpRequestException = Microsoft.AspNetCore.Http.BadHttpRequestException;

namespace HairSaloonAPI.Services
{
    public class RegisterUserService : IRegisterUserService
    {
        private DataContext _db;

        public RegisterUserService(DataContext db)
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

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public string CreateToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        public void CreateUser(IRegisterUserDTO user)
        {
            if (CheckIfUsernameExist(user.UserName) == true)
            {
                throw new BadHttpRequestException("There already exists a user with that username!");
            }

            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User _user = new User();
            _user.Username = user.UserName;
            _user.PasswordSalt = passwordSalt;
            _user.PasswordHash = passwordHash;
            _user.Token = CreateToken();

            _db.Users.Add(_user);
            _db.SaveChanges();
        }
    }
}
