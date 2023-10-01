using HairSaloonAPI.Data;
using HairSaloonAPI.Models;
using HairSaloonAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Security.Cryptography;
using HairSaloonAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;

namespace HairSaloonApp.Tests;

[TestFixture]
public class LoginUserServiceTests
{
    public DataContext _InMemoryDb;
    public LoginUserService service;

    [SetUp]
    public void Setup()
    {
        var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: myDatabaseName)
            .Options;
        _InMemoryDb = new DataContext(options);

        service = new LoginUserService(_InMemoryDb);
    }

    [TearDown]
    public void Cleanup()
    {
        _InMemoryDb.Dispose();
    }

    [Test]
    public void CheckIfUserNameExist_NoUserFound_ReturnsFalse()
    {
        //Arrange

        //Act
        var _result = service.CheckIfUsernameExist("test");

        //Assert
        Assert.That(_result, Is.False);
    }

    [Test]
    public void CheckIfUserNameExist_UserFound_ReturnsTrue()
    {
        //Arrange
        byte[] _passwordHash;
        byte[] _passwordSalt;
        using (var hmac = new HMACSHA512())
        {
            _passwordSalt = hmac.Key;
            _passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("password"));
        }
        string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        User _user = new User { Id = 1, PasswordHash = _passwordHash, PasswordSalt = _passwordSalt, Token = token, Username = "test" };
        _InMemoryDb.Users.Add(_user);
        _InMemoryDb.SaveChanges();

        //Act
        var _result = service.CheckIfUsernameExist("test");

        //Assert
        Assert.That(_result, Is.True);
    }

    [Test]
    public void VerifyPassword_PasswordIncorrect_ReturnsFalse()
    {
        //Arrange
        byte[] _passwordHash;
        byte[] _passwordSalt;
        using (var hmac = new HMACSHA512())
        {
            _passwordSalt = hmac.Key;
            _passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("password"));
        }
        string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        User _user = new User { Id = 1, PasswordHash = _passwordHash, PasswordSalt = _passwordSalt, Token = token, Username = "test" };
        _InMemoryDb.Users.Add(_user);
        _InMemoryDb.SaveChanges();

        //Act
        var _result = service.VerifyPasswordHash("notthepassword", "test");

        //Assert
        Assert.That(_result, Is.False);
    }

    [Test]
    public void VerifyPassword_PasswordCorrect_ReturnsTrue()
    {
        //Arrange
        byte[] _passwordHash;
        byte[] _passwordSalt;
        using (var hmac = new HMACSHA512())
        {
            _passwordSalt = hmac.Key;
            _passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("password"));
        }
        string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        User _user = new User { Id = 1, PasswordHash = _passwordHash, PasswordSalt = _passwordSalt, Token = token, Username = "test" };
        _InMemoryDb.Users.Add(_user);
        _InMemoryDb.SaveChanges();

        //Act
        var _result = service.VerifyPasswordHash("password", "test");

        //Assert
        Assert.That(_result, Is.True);
    }

    [Test]
    public void Login_WrongUserName_ThrowsBadHttpReqException()
    {
        //Arrange
        byte[] _passwordHash;
        byte[] _passwordSalt;
        using (var hmac = new HMACSHA512())
        {
            _passwordSalt = hmac.Key;
            _passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("password"));
        }
        string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        User _user = new User { Id = 1, PasswordHash = _passwordHash, PasswordSalt = _passwordSalt, Token = token, Username = "test" };
        _InMemoryDb.Users.Add(_user);
        _InMemoryDb.SaveChanges();

        LoginUserDTO _userData = new LoginUserDTO{Password = "password", UserName = "nottest"};

        //Act

        //Assert
        Assert.Throws<BadHttpRequestException>(() => service.Login(_userData));
    }

    [Test]
    public void Login_WrongPassword_ThrowsBadHttpReqException()
    {
        //Arrange
        byte[] _passwordHash;
        byte[] _passwordSalt;
        using (var hmac = new HMACSHA512())
        {
            _passwordSalt = hmac.Key;
            _passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("password"));
        }
        string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        User _user = new User { Id = 1, PasswordHash = _passwordHash, PasswordSalt = _passwordSalt, Token = token, Username = "test" };
        _InMemoryDb.Users.Add(_user);
        _InMemoryDb.SaveChanges();

        LoginUserDTO _userData = new LoginUserDTO { Password = "notthepassword", UserName = "test" };

        //Act

        //Assert
        Assert.Throws<BadHttpRequestException>(() => service.Login(_userData));
    }

    [Test]
    public void Login_RightCredentials_ReturnsCorrectToken()
    {
        //Arrange
        byte[] _passwordHash;
        byte[] _passwordSalt;
        using (var hmac = new HMACSHA512())
        {
            _passwordSalt = hmac.Key;
            _passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("password"));
        }
        string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        User _user = new User { Id = 1, PasswordHash = _passwordHash, PasswordSalt = _passwordSalt, Token = token, Username = "test" };
        _InMemoryDb.Users.Add(_user);
        _InMemoryDb.SaveChanges();

        LoginUserDTO _userData = new LoginUserDTO { Password = "password", UserName = "test" };

        //Act
        var _result = service.Login(_userData);

        //Assert
        Assert.That(_result, Is.EqualTo(_user.Token));
    }
}