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
public class RegisterUserServiceTests
{
    public DataContext _InMemoryDb;
    public RegisterUserService service;

    [SetUp]
    public void Setup()
    {
        var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: myDatabaseName)
            .Options;
        _InMemoryDb = new DataContext(options);

        service = new RegisterUserService(_InMemoryDb);
    }

    [TearDown]
    public void Cleanup()
    {
        _InMemoryDb.Dispose();
    }

    [Test]
    public void CheckIfUsernameExist_NoUserFound_ReturnsFalse()
    {
        //Arrange

        //Act
        var _result = service.CheckIfUsernameExist("test");

        //Assert
        Assert.That(_result, Is.False);
    }

    [Test]
    public void CheckIfUsernameExist_UserFound_ReturnsFalse()
    {
        //Arrange
        byte[] _bytes = System.Text.Encoding.UTF8.GetBytes("password");
        string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        User _user = new User { Id = 1, PasswordHash = _bytes, PasswordSalt = _bytes, Token = token, Username = "test" };
        _InMemoryDb.Users.Add(_user);
        _InMemoryDb.SaveChanges();

        //Act
        var _result = service.CheckIfUsernameExist("test");

        //Assert
        Assert.That(_result, Is.True);
    }

    [Test]
    public void CreatePasswordHash_GetsString_GivesOutTwoByteArrays()
    {
        //Arrange

        //Act
        service.CreatePasswordHash("password", out byte[] _passwordHash, out byte[] _passwordSalt);

        //Assert
        Assert.That(_passwordHash, Is.Not.Empty);
        Assert.That(_passwordSalt, Is.Not.Empty);
        Assert.That(_passwordHash, Is.TypeOf<byte[]>());
        Assert.That(_passwordSalt, Is.TypeOf<byte[]>());
    }

    [Test]
    public void CreatePasswordHash_UsingSalt_ReturnsSameHash()
    {
        //Arrange
        service.CreatePasswordHash("password", out byte[] _passwordHashOriginal, out byte[] _passwordSaltOriginal);

        //Act
        byte[] _passwordHashReturned;
        using (var hmac = new HMACSHA512(_passwordSaltOriginal))
        {
            _passwordHashReturned = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("password"));
            
        }


        //Assert
        Assert.That(_passwordHashOriginal, Is.EqualTo(_passwordHashReturned));
    }

    [Test]
    public void CreateToken_CreatesRandomToken_TokenIsValidLengthAndFormat()
    {
        //Arrange

        //Act
        var _result = service.CreateToken();

        //Assert
        Assert.That(_result, Is.TypeOf<string>());
        Assert.That(_result.Length, Is.EqualTo(128));
    }

    [Test]
    public void CreateUser_UsernameExistInDb_ThrowsBadHttpReqException()
    {
        //Arrange
        byte[] _bytes = System.Text.Encoding.UTF8.GetBytes("password");
        string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        User _user = new User { Id = 1, PasswordHash = _bytes, PasswordSalt = _bytes, Token = token, Username = "test" };
        _InMemoryDb.Users.Add(_user);
        _InMemoryDb.SaveChanges();

        
        RegisterUserDTO _userWithSameUserName = new RegisterUserDTO {UserName = "test", Password = "password", ConfirmPassword = "password"};

        //Act

        //Assert
        Assert.Throws<BadHttpRequestException>(() => service.CreateUser(_userWithSameUserName));
    }

    [Test]
    public void CreateUser_CreatesUser_UserIsInDatabase()
    {
        //Arrange
        RegisterUserDTO _user = new RegisterUserDTO { UserName = "test", Password = "password", ConfirmPassword = "password" };

        //Act
        service.CreateUser(_user);
        var _result = _InMemoryDb.Users.FirstOrDefault();

        //Assert
        Assert.That(_InMemoryDb.Users.ToList(), Is.Not.Empty);
        Assert.That(_result, Is.TypeOf<User>());
        Assert.That(_result.Id, Is.EqualTo(1));
    }
}