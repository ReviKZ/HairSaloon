using HairSaloonAPI.Data;
using HairSaloonAPI.Models;
using HairSaloonAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Security.Cryptography;
using HairSaloonAPI.Enums;
using HairSaloonAPI.Models.DTOs.ControllerDTOs;

namespace HairSaloonApp.Tests
{
    public class UserServiceTests
    {
        public DataContext _InMemoryDb;
        public UserService service;

        [SetUp]
        public void Setup()
        {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            _InMemoryDb = new DataContext(options);

            service = new UserService(_InMemoryDb);
        }

        [TearDown]
        public void Cleanup()
        {
            _InMemoryDb.Dispose();
        }

        [Test]
        public void DeleteUser_NoUserFound_ThrowBadHttpReqException()
        {
            //Arrange
            Random _randomId = new Random();
            
            //Act

            //Assert
            Assert.Throws<BadHttpRequestException>(() => service.DeleteUser(_randomId.Next()));
        }

        [Test]
        public void DeleteUser_UserFound_RemovedFromDatabase()
        {
            //Arrange
            byte[] _bytes = System.Text.Encoding.UTF8.GetBytes("password");
            string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            User _user = new User { Id = 1, PasswordHash = _bytes, PasswordSalt = _bytes, Token = token, Username = "test" };
            _InMemoryDb.Users.Add(_user);
            _InMemoryDb.SaveChanges();

            //Act
            service.DeleteUser(1);

            //Assert
            Assert.That(_InMemoryDb.Users.ToList(), Is.Empty);
        }

        [Test]
        public void GetLastUserId_NoUserFound_ThrowsBadHttpReqException()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<BadHttpRequestException>(() => service.GetLastUserId());
        }

        [Test]
        public void GetLastUserId_UserFound_ReturnsLastUserId()
        {
            //Arrange
            byte[] _bytes = System.Text.Encoding.UTF8.GetBytes("password");
            string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            User _user1 = new User { Id = 1, PasswordHash = _bytes, PasswordSalt = _bytes, Token = token, Username = "test1" };
            User _user2 = new User { Id = 2, PasswordHash = _bytes, PasswordSalt = _bytes, Token = token, Username = "test2" };
            _InMemoryDb.Users.AddRange(_user1, _user2);
            _InMemoryDb.SaveChanges();

            //Act
            var _result = service.GetLastUserId();

            //Assert
            Assert.That(_result, Is.EqualTo(2));
        }

        [Test]
        public void GetAllUsers_NoUserFound_ThrowsBadHttpReqException()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<BadHttpRequestException>(() => service.GetAllUsers());
        }

        [Test]
        public void GetAllUsers_UserFound_ReturnsListOfUserListDTO()
        {
            //Arrange
            byte[] _bytes = System.Text.Encoding.UTF8.GetBytes("password");
            string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            User _user1 = new User { Id = 1, PasswordHash = _bytes, PasswordSalt = _bytes, Token = token, Username = "test1" };
            User _user2 = new User { Id = 2, PasswordHash = _bytes, PasswordSalt = _bytes, Token = token, Username = "test2" };
            Person _person1 = new Person {Id = 1, 
                EmailAddress = "test1", 
                FirstName = "test1", 
                LastName = "test1", 
                Gender = Gender.Male, 
                PhoneNumber = "0101",
                Type = PersonType.HairDresser,
                User = _user1
            };
            Person _person2 = new Person
            {
                Id = 2,
                EmailAddress = "test2",
                FirstName = "test2",
                LastName = "test2",
                Gender = Gender.Else,
                PhoneNumber = "0201",
                Type = PersonType.Guest,
                User = _user2
            };
            UserListDTO _returnPerson1 = new UserListDTO(_person1.FirstName, _person1.LastName, _person1.User.Id);
            UserListDTO _returnPerson2 = new UserListDTO(_person2.FirstName, _person2.LastName, _person2.User.Id);
            List<UserListDTO> _returnList = new List<UserListDTO> { _returnPerson1, _returnPerson2 };

            _InMemoryDb.Persons.AddRange(_person1, _person2);
            _InMemoryDb.SaveChanges();

            // Act
            var _result = service.GetAllUsers();

            // Assert
            Assert.That(_result[0].UserId, Is.EqualTo(_returnList[0].UserId));
            Assert.That(_result[0].Name, Is.EqualTo(_returnList[0].Name));
            Assert.That(_result[1].UserId, Is.EqualTo(_returnList[1].UserId));
            Assert.That(_result[1].Name, Is.EqualTo(_returnList[1].Name));
        }

        [Test]
        public void GetAllHairDressers_NoUserFound_ThrowsBadHttpReqException()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<BadHttpRequestException>(() => service.GetAllUsers());
        }

        [Test]
        public void GetAllHairDressers_OneGuestOneHairDresser_ReturnsOnlyHairDresser()
        {
            //Arrange
            byte[] _bytes = System.Text.Encoding.UTF8.GetBytes("password");
            string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            User _user1 = new User { Id = 1, PasswordHash = _bytes, PasswordSalt = _bytes, Token = token, Username = "test1" };
            User _user2 = new User { Id = 2, PasswordHash = _bytes, PasswordSalt = _bytes, Token = token, Username = "test2" };
            Person _person1 = new Person
            {
                Id = 1,
                EmailAddress = "test1",
                FirstName = "test1",
                LastName = "test1",
                Gender = Gender.Male,
                PhoneNumber = "0101",
                Type = PersonType.HairDresser,
                User = _user1
            };
            Person _person2 = new Person
            {
                Id = 2,
                EmailAddress = "test2",
                FirstName = "test2",
                LastName = "test2",
                Gender = Gender.Else,
                PhoneNumber = "0201",
                Type = PersonType.Guest,
                User = _user2
            };
            UserListDTO _returnPerson1 = new UserListDTO(_person1.FirstName, _person1.LastName, _person1.User.Id);
            List<UserListDTO> _returnList = new List<UserListDTO> { _returnPerson1 };

            _InMemoryDb.Persons.AddRange(_person1, _person2);
            _InMemoryDb.SaveChanges();

            // Act
            var _result = service.GetAllHairDressers();

            // Assert
            Assert.That(_result[0].UserId, Is.EqualTo(_returnList[0].UserId));
            Assert.That(_result[0].Name, Is.EqualTo(_returnList[0].Name));
        }

        [Test]
        public void ConvertTokenToId_NoTokenMatchFound_ThrowsBadHttpReqException()
        {
            //Arrange
            string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            //Act

            //Assert
            Assert.Throws<BadHttpRequestException>(() => service.ConvertTokenToId(token));
        }

        [Test]
        public void ConvertTokenToId_TokenMatchFound_ReturnsId()
        {
            //Arrange
            byte[] _bytes = System.Text.Encoding.UTF8.GetBytes("password");
            string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            User _user = new User { Id = 1, PasswordHash = _bytes, PasswordSalt = _bytes, Token = token, Username = "test" };
            _InMemoryDb.Users.Add(_user);
            _InMemoryDb.SaveChanges();

            //Act
            var _result = service.ConvertTokenToId(token);

            //Assert
            Assert.That(_result, Is.EqualTo(1));
        }
    }
}