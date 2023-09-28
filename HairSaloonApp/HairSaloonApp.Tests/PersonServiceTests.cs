using HairSaloonAPI.Data;
using HairSaloonAPI.Models;
using HairSaloonAPI.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Security.Cryptography;
using HairSaloonAPI.Enums;
using HairSaloonAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HairSaloonApp.Tests;

[TestFixture]
public class PersonServiceTests
{
    public DataContext _InMemoryDb;
    public PersonService service;

    [SetUp]
    public void Setup()
    {
        var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: myDatabaseName)
            .Options;
        _InMemoryDb = new DataContext(options);

        service = new PersonService(_InMemoryDb);

        byte[] _bytes = System.Text.Encoding.UTF8.GetBytes("password");
        string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        User _user = new User { Id = 1, PasswordHash = _bytes, PasswordSalt = _bytes, Token = token, Username = "test" };
        _InMemoryDb.Users.Add(_user);
        _InMemoryDb.SaveChanges();
    }

    [TearDown]
    public void Cleanup()
    {
        _InMemoryDb.Dispose();
    }

    [Test]
    public void CreatePerson_DataGiven_PersonAddedToDb()
    {
        //Arrange
        PersonDTO _personData = new PersonDTO{EmailAddress = "test@test.com", FirstName = "test", LastName = "test", PhoneNumber = "0101"};

        //Act
        service.CreatePerson(1, "Male", "HairDresser", _personData);

        //Assert
        Assert.That(_InMemoryDb.Persons, Is.Not.Empty);
    }

    [Test]
    public void CreatePerson_DataGiven_PersonInDbMatchesAddedPerson()
    {
        //Arrange
        PersonDTO _personData = new PersonDTO { EmailAddress = "test@test.com", FirstName = "test", LastName = "test", PhoneNumber = "0101" };

        User _userData = _InMemoryDb.Users.FirstOrDefault();

        Person _personWithSameData = new Person
        {
            EmailAddress = "test@test.com",
            FirstName = "test",
            LastName = "test",
            Gender = Gender.Male,
            Id = 1,
            PhoneNumber = "0101",
            Type = PersonType.HairDresser,
            User = _userData
        };

        //Act
        service.CreatePerson(1, "Male", "HairDresser", _personData);
        var _expectedJson = JsonConvert.SerializeObject(_personWithSameData);
        var _actualJson = JsonConvert.SerializeObject(_InMemoryDb.Persons.FirstOrDefault());

        //Assert
        Assert.That(_actualJson, Is.EqualTo(_expectedJson));
    }

    [Test]
    public void GetPerson_NoPersonFound_ThrowsBadHttpReqException()
    {
        //Arrange


        //Act

        //Assert
        Assert.Throws<BadHttpRequestException>(() => service.GetPerson(1));
    }

    [Test]
    public void GetPerson_PersonFound_ReturnPersonWithSameData()
    {
        //Arrange
        User _userData = _InMemoryDb.Users.FirstOrDefault();

        Person _person = new Person
        {
            EmailAddress = "test@test.com",
            FirstName = "test",
            LastName = "test",
            Gender = Gender.Male,
            Id = 1,
            PhoneNumber = "0101",
            Type = PersonType.HairDresser,
            User = _userData
        };

        _InMemoryDb.Persons.Add(_person);
        _InMemoryDb.SaveChanges();

        //Act
        var _result = service.GetPerson(1);

        //Assert
        Assert.That(_result, Is.EqualTo(_person));
    }

    [Test]
    public void EditPerson_NoPersonFound_ThrowsBadHttpReqException()
    {
        //Arrange
        PersonDTO _personData = new PersonDTO { EmailAddress = "test@test.com", FirstName = "test", LastName = "test", PhoneNumber = "0101" };

        //Act

        //Assert
        Assert.Throws<BadHttpRequestException>(() => service.EditPerson(1, _personData));
    }

    [Test]
    public void EditPerson_PersonFound_ReturnPersonWithEditedData()
    {
        //Arrange
        User _userData = _InMemoryDb.Users.FirstOrDefault();

        Person _person = new Person
        {
            EmailAddress = "test@test.com",
            FirstName = "test",
            LastName = "test",
            Gender = Gender.Male,
            Id = 1,
            PhoneNumber = "0101",
            Type = PersonType.HairDresser,
            User = _userData
        };

        _InMemoryDb.Persons.Add(_person);
        _InMemoryDb.SaveChanges();

        Person _personEdited = new Person
        {
            EmailAddress = "edited@test.com",
            FirstName = "edited",
            LastName = "edited",
            Gender = Gender.Male,
            Id = 1,
            PhoneNumber = "0202",
            Type = PersonType.HairDresser,
            User = _userData
        };

        PersonDTO _personData = new PersonDTO { EmailAddress = "edited@test.com", FirstName = "edited", LastName = "edited", PhoneNumber = "0202" };

        //Act
        service.EditPerson(1, _personData);
        var _expectedJson = JsonConvert.SerializeObject(_personEdited);
        var _actualJson = JsonConvert.SerializeObject(_InMemoryDb.Persons.FirstOrDefault());

        //Assert
        Assert.That(_actualJson, Is.EqualTo(_expectedJson));
    }

    [Test]
    public void DeletePerson_PersonNot_ThrowsBadHttpReqException()
    {
        //Arrange

        //Act

        //Assert
        Assert.Throws<BadHttpRequestException>(() => service.DeletePerson(1));
    }

    [Test]
    public void DeletePerson_PersonFoundWithoutConnectedAppointment_RemovesPersonFromDb()
    {
        //Arrange
        User _userData = _InMemoryDb.Users.FirstOrDefault();

        Person _person = new Person
        {
            EmailAddress = "test@test.com",
            FirstName = "test",
            LastName = "test",
            Gender = Gender.Male,
            Id = 1,
            PhoneNumber = "0101",
            Type = PersonType.HairDresser,
            User = _userData
        };

        _InMemoryDb.Persons.Add(_person);
        _InMemoryDb.SaveChanges();

        //Act
        service.DeletePerson(1);
        var _result = _InMemoryDb.Persons;

        //Assert
        Assert.That(_result, Is.Empty);
    }

    [Test]
    public void DeletePerson_PersonFoundWithConnectedAppointment_RemovesPersonAndAppointmentFromDb()
    {
        //Arrange
        byte[] _bytes = System.Text.Encoding.UTF8.GetBytes("password");
        string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        User _user = new User { Id = 2, PasswordHash = _bytes, PasswordSalt = _bytes, Token = token, Username = "testguest" };
        _InMemoryDb.Users.Add(_user);
        _InMemoryDb.SaveChanges();

        User _hairDresserUserData = _InMemoryDb.Users.FirstOrDefault(u => u.Id == 1);
        User _guestUserData = _InMemoryDb.Users.FirstOrDefault(u => u.Id == 2);



        Person _hairDresser = new Person
        {
            EmailAddress = "test@test.com",
            FirstName = "test",
            LastName = "test",
            Gender = Gender.Male,
            Id = 1,
            PhoneNumber = "0101",
            Type = PersonType.HairDresser,
            User = _hairDresserUserData
        };

        _InMemoryDb.Persons.Add(_hairDresser);
        _InMemoryDb.SaveChanges();

        Person _guest = new Person
        {
            EmailAddress = "guest@test.com",
            FirstName = "guest",
            LastName = "guest",
            Gender = Gender.Female,
            Id = 2,
            PhoneNumber = "0101",
            Type = PersonType.Guest,
            User = _guestUserData
        };

        _InMemoryDb.Persons.Add(_guest);
        _InMemoryDb.SaveChanges();

        var _hairDresserData = _InMemoryDb.Persons.FirstOrDefault(u => u.Id == 1);
        var _guestData = _InMemoryDb.Persons.FirstOrDefault(u => u.Id == 2);

        Appointment _appointment = new Appointment
        {
            Date = new DateTime(2020, 1, 1, 0, 0, 0),
            StartTime = new DateTime(2020, 1, 1, 7, 20, 00),
            EndTime = new DateTime(2020, 1, 1, 10, 10, 00),
            Description = "test",
            Guest = _guestData,
            HairDresser = _hairDresserData,
            Id = 1,
            Verified = false
        };

        //Act
        service.DeletePerson(1);
        var _personResult = _InMemoryDb.Persons.FirstOrDefault(u => u.Id == 1);
        var _appointmentResult = _InMemoryDb.Appointments;

        //Assert
        Assert.That(_personResult, Is.Null);
        Assert.That(_appointmentResult, Is.Empty);
    }
}