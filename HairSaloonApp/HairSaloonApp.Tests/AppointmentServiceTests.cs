using HairSaloonAPI.Data;
using HairSaloonAPI.Enums;
using HairSaloonAPI.Models;
using HairSaloonAPI.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Security.Cryptography;
using HairSaloonAPI.Models.DTOs.ControllerDTOs;
using HairSaloonAPI.Structs;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HairSaloonApp.Tests;

[TestFixture]
public class AppointmentServiceTests
{
    public DataContext _InMemoryDb;
    public AppointmentService service;

    [SetUp]
    public void Setup()
    {
        var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: myDatabaseName)
            .Options;
        _InMemoryDb = new DataContext(options);

        service = new AppointmentService(_InMemoryDb);

        byte[] _hdBytes = System.Text.Encoding.UTF8.GetBytes("password");
        string hdToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        User _hdUser = new User { Id = 1, PasswordHash = _hdBytes, PasswordSalt = _hdBytes, Token = hdToken, Username = "hairdresser" };

        byte[] _guestBytes = System.Text.Encoding.UTF8.GetBytes("password");
        string guestToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        User _guestUser = new User { Id = 2, PasswordHash = _guestBytes, PasswordSalt = _guestBytes, Token = guestToken, Username = "guest" };
        _InMemoryDb.Users.AddRange(_hdUser, _guestUser);
        _InMemoryDb.SaveChanges();

        Person _hairDresser = new Person
        {
            EmailAddress = "hd@test.com",
            FirstName = "hair",
            LastName = "dresser",
            Gender = Gender.Male,
            Id = 1,
            PhoneNumber = "0101",
            Type = PersonType.HairDresser,
            User = _InMemoryDb.Users.First(u => u.Id == 1)
        };

        Person _guest = new Person
        {
            EmailAddress = "guest@test.com",
            FirstName = "guest",
            LastName = "guest",
            Gender = Gender.Female,
            Id = 2,
            PhoneNumber = "0101",
            Type = PersonType.Guest,
            User = _InMemoryDb.Users.First(u => u.Id == 2)
        };

        _InMemoryDb.Persons.AddRange(_hairDresser, _guest);
        _InMemoryDb.SaveChanges();
    }

    [TearDown]
    public void Cleanup()
    {
        _InMemoryDb.Dispose();
    }

    [Test]
    public void CreateAppointment_HairDresserOrGuestOrBothNotFound_ThrowsBadHttpRequest()
    {
        //Arrange
        CreateAppointmentDTO _appointmentDataNoHairDresser = new CreateAppointmentDTO
        {
            GuestId = 2,
            Date = new DateFormat(2020, 1, 1),
            StartTime = new TimeFormat(10, 0, 0),
            EndTime = new TimeFormat(11, 0, 0),
            Description = "test",
            HairDresserId = 3
        };

        CreateAppointmentDTO _appointmentDataNoGuest = new CreateAppointmentDTO
        {
            GuestId = 3,
            Date = new DateFormat(2020, 1, 1),
            StartTime = new TimeFormat(10, 0, 0),
            EndTime = new TimeFormat(11, 0, 0),
            Description = "test",
            HairDresserId = 1
        };

        CreateAppointmentDTO _appointmentDataBothNotFound = new CreateAppointmentDTO
        {
            GuestId = 4,
            Date = new DateFormat(2020, 1, 1),
            StartTime = new TimeFormat(10, 0, 0),
            EndTime = new TimeFormat(11, 0, 0),
            Description = "test",
            HairDresserId = 3
        };

        //Act

        //Assert
        Assert.Throws<BadHttpRequestException>(() => service.CreateAppointment(_appointmentDataNoHairDresser));
        Assert.Throws<BadHttpRequestException>(() => service.CreateAppointment(_appointmentDataNoGuest));
        Assert.Throws<BadHttpRequestException>(() => service.CreateAppointment(_appointmentDataBothNotFound));
    }

    [Test]
    public void CreateAppointment_PutGuestAsHairDresser_ThrowsBadHttpRequest()
    {
        //Arrange
        CreateAppointmentDTO _appointmentData = new CreateAppointmentDTO
        {
            GuestId = 1,
            Date = new DateFormat(2020, 1, 1),
            StartTime = new TimeFormat(10, 0, 0),
            EndTime = new TimeFormat(11, 0, 0),
            Description = "test",
            HairDresserId = 2
        };

        //Act

        //Assert
        Assert.Throws<BadHttpRequestException>(() => service.CreateAppointment(_appointmentData));
    }

    [Test]
    public void CreateAppointment_CorrectData_AddAppointmentToDbs()
    {
        //Arrange
        CreateAppointmentDTO _appointmentData = new CreateAppointmentDTO
        {
            GuestId = 2,
            Date = new DateFormat(2020, 1, 1),
            StartTime = new TimeFormat(10, 0, 0),
            EndTime = new TimeFormat(11, 0, 0),
            Description = "test",
            HairDresserId = 1
        };

        Appointment _appointment = new Appointment
        {
            Date = new DateTime(2020, 1, 1, 0, 0, 0),
            StartTime = new DateTime(2020, 1, 1, 10, 0, 0),
            EndTime = new DateTime(2020, 1, 1, 11, 0, 0),
            Description = "test",
            Guest = _InMemoryDb.Persons.First(g => g.Id == 2),
            HairDresser = _InMemoryDb.Persons.First(g => g.Id == 1),
            Id = 1,
            Verified = false
        };

        //Act
        service.CreateAppointment(_appointmentData);
        var expectedJson = JsonConvert.SerializeObject(_appointment);
        var actualJson = JsonConvert.SerializeObject(_InMemoryDb.Appointments.FirstOrDefault(a => a.Id == 1));

        //Assert
        Assert.That(actualJson, Is.EqualTo(expectedJson));
    }

    [Test]
    public void GetAppointment_NoAppointmentFound_ThrowsBadHttpReqException()
    {
        //Arrange

        //Act

        //Assert
        Assert.Throws<BadHttpRequestException>(() => service.GetAppointment(1));
    }

    [Test]
    public void GetAppointment_AppointmentFound_ReturnCorrectData()
    {
        //Arrange

        Appointment _appointment = new Appointment
        {
            Date = new DateTime(2020, 1, 1, 0, 0, 0),
            StartTime = new DateTime(2020, 1, 1, 10, 0, 0),
            EndTime = new DateTime(2020, 1, 1, 11, 0, 0),
            Description = "test",
            Guest = _InMemoryDb.Persons.First(g => g.Id == 2),
            HairDresser = _InMemoryDb.Persons.First(g => g.Id == 1),
            Id = 1,
            Verified = false
        };

        _InMemoryDb.Appointments.Add(_appointment);
        _InMemoryDb.SaveChanges();

        Appointment _appointmentDb = _InMemoryDb.Appointments.FirstOrDefault();

        GetAppointmentDTO _appointmentData = new GetAppointmentDTO
        {
            Date = new DateFormat(_appointmentDb.Date.Year, _appointmentDb.Date.Month, _appointmentDb.Date.Day),
            StartTime = new TimeFormat(_appointmentDb.StartTime.Hour, _appointmentDb.StartTime.Minute, _appointmentDb.StartTime.Second),
            EndTime = new TimeFormat(_appointmentDb.EndTime.Hour, _appointmentDb.EndTime.Minute, _appointmentDb.EndTime.Second),
            Description = _appointmentDb.Description,
            GuestId = _appointmentDb.Guest.Id,
            HairDresserId = _appointmentDb.HairDresser.Id,
            Id = _appointmentDb.Id,
            Verified = _appointmentDb.Verified
        };

        //Act
        var _result = service.GetAppointment(1);
        var expectedJson = JsonConvert.SerializeObject(_appointmentData);
        var actualJson = JsonConvert.SerializeObject(_result);

        //Assert
        Assert.That(actualJson, Is.EqualTo(expectedJson));
    }

    [Test]
    public void GetAppointmentListByUserId_NoUserFound_ThrowsBadHttpReqException()
    {
        //Arrange

        //Act

        //Assert
        Assert.Throws<BadHttpRequestException>(() => service.GetAppointmentListByUserId(3));
    }

    [Test]
    public void GetAppointmentListByUserId_UserFoundWithNoAppointment_ReturnsEmptyList()
    {
        //Arrange

        //Act
        var _result = service.GetAppointmentListByUserId(1);

        //Assert
        Assert.That(_result, Is.Empty);
    }

    [Test]
    public void GetAppointmentListByUserId_UserFoundWithAppointments_ReturnsListOfAppointments()
    {
        //Arrange
        Appointment _appointment = new Appointment
        {
            Date = new DateTime(2020, 1, 1, 0, 0, 0),
            StartTime = new DateTime(2020, 1, 1, 10, 0, 0),
            EndTime = new DateTime(2020, 1, 1, 11, 0, 0),
            Description = "test",
            Guest = _InMemoryDb.Persons.First(g => g.Id == 2),
            HairDresser = _InMemoryDb.Persons.First(g => g.Id == 1),
            Id = 1,
            Verified = false
        };

        Appointment _appointment2 = new Appointment
        {
            Date = new DateTime(2020, 1, 2, 0, 0, 0),
            StartTime = new DateTime(2020, 1, 2, 10, 0, 0),
            EndTime = new DateTime(2020, 1, 2, 11, 0, 0),
            Description = "test2",
            Guest = _InMemoryDb.Persons.First(g => g.Id == 2),
            HairDresser = _InMemoryDb.Persons.First(g => g.Id == 1),
            Id = 2,
            Verified = false
        };

        _InMemoryDb.Appointments.AddRange(_appointment, _appointment2);
        _InMemoryDb.SaveChanges();

        var _appointmentsDb = _InMemoryDb.Appointments.Include(a => a.HairDresser.User)
            .Where(a => a.HairDresser.User.Id == 1).ToList();

        var _appointmentsData = new List<GetAppointmentDTO>();

        foreach (var appointment in _appointmentsDb)
        {
            _appointmentsData.Add(
                new GetAppointmentDTO
                {
                    Date = new DateFormat(appointment.Date.Year, appointment.Date.Month, appointment.Date.Day),
                    StartTime = new TimeFormat(appointment.StartTime.Hour, appointment.StartTime.Minute, appointment.StartTime.Second),
                    EndTime = new TimeFormat(appointment.EndTime.Hour, appointment.EndTime.Minute, appointment.EndTime.Second),
                    Description = appointment.Description,
                    GuestId = appointment.Guest.Id,
                    HairDresserId = appointment.HairDresser.Id,
                    Id = appointment.Id,
                    Verified = appointment.Verified
                }
                );
        }

        //Act
        var _result = service.GetAppointmentListByUserId(1);
        var expectedJson = JsonConvert.SerializeObject(_appointmentsData);
        var actualJson = JsonConvert.SerializeObject(_result);

        //Assert
        Assert.That(actualJson, Is.EqualTo(expectedJson));
    }

    [Test]
    public void UpdateAppointment_NoAppointmentFound_ThrowsBadHttpReqException()
    {
        //Arrange
        CreateAppointmentDTO _appointmentData = new CreateAppointmentDTO
        {
            GuestId = 2,
            Date = new DateFormat(2020, 1, 1),
            StartTime = new TimeFormat(10, 0, 0),
            EndTime = new TimeFormat(11, 0, 0),
            Description = "test",
            HairDresserId = 1
        };

        //Act

        //Assert
        Assert.Throws<BadHttpRequestException>(() => service.UpdateAppointment(1, _appointmentData));
    }

    [Test]
    public void UpdateAppointment_AppointmentFoundButPutGuestAsAHairDresser_ThrowsBadHttpReqException()
    {
        //Arrange
        Appointment _appointment = new Appointment
        {
            Date = new DateTime(2020, 1, 1, 0, 0, 0),
            StartTime = new DateTime(2020, 1, 1, 10, 0, 0),
            EndTime = new DateTime(2020, 1, 1, 11, 0, 0),
            Description = "test",
            Guest = _InMemoryDb.Persons.First(g => g.Id == 2),
            HairDresser = _InMemoryDb.Persons.First(g => g.Id == 1),
            Id = 1,
            Verified = false
        };
        
        _InMemoryDb.Appointments.Add(_appointment);
        _InMemoryDb.SaveChanges();

        CreateAppointmentDTO _appointmentData = new CreateAppointmentDTO
        {
            GuestId = 1,
            Date = new DateFormat(2020, 1, 1),
            StartTime = new TimeFormat(10, 0, 0),
            EndTime = new TimeFormat(11, 0, 0),
            Description = "test",
            HairDresserId = 2
        };

        //Act

        //Assert
        Assert.Throws<BadHttpRequestException>(() => service.UpdateAppointment(1, _appointmentData));
    }

    [Test]
    public void UpdateAppointment_AppointmentFoundAndCorrectData_UpdatesAppointmentInDb()
    {
        //Arrange
        Appointment _appointment = new Appointment
        {
            Date = new DateTime(2020, 1, 1, 0, 0, 0),
            StartTime = new DateTime(2020, 1, 1, 10, 0, 0),
            EndTime = new DateTime(2020, 1, 1, 11, 0, 0),
            Description = "test",
            Guest = _InMemoryDb.Persons.First(g => g.Id == 2),
            HairDresser = _InMemoryDb.Persons.First(g => g.Id == 1),
            Id = 1,
            Verified = false
        };

        _InMemoryDb.Appointments.Add(_appointment);
        _InMemoryDb.SaveChanges();

        Appointment _appointmentDb = _InMemoryDb.Appointments.FirstOrDefault();

        GetAppointmentDTO _appointmentData = new GetAppointmentDTO
        {
            Date = new DateFormat(_appointmentDb.Date.Year, _appointmentDb.Date.Month, _appointmentDb.Date.Day),
            StartTime = new TimeFormat(_appointmentDb.StartTime.Hour, _appointmentDb.StartTime.Minute, _appointmentDb.StartTime.Second),
            EndTime = new TimeFormat(_appointmentDb.EndTime.Hour, _appointmentDb.EndTime.Minute, _appointmentDb.EndTime.Second),
            Description = _appointmentDb.Description,
            GuestId = _appointmentDb.Guest.Id,
            HairDresserId = _appointmentDb.HairDresser.Id,
            Id = _appointmentDb.Id,
            Verified = _appointmentDb.Verified
        };

        CreateAppointmentDTO _appointmentUpdateData = new CreateAppointmentDTO
        {
            GuestId = 2,
            Date = new DateFormat(2020, 1, 1),
            StartTime = new TimeFormat(10, 0, 0),
            EndTime = new TimeFormat(11, 0, 0),
            Description = "test",
            HairDresserId = 1
        };

        //Act
        service.UpdateAppointment(1, _appointmentUpdateData);
        var _result = _InMemoryDb.Appointments.FirstOrDefault();
        var _expectedJson = JsonConvert.SerializeObject(_appointmentData);
        var _actualJson = JsonConvert.SerializeObject(_result);

        //Assert
        Assert.That(_actualJson, Is.Not.EqualTo(_expectedJson));
    }

    [Test]
    public void DeleteAppointment_AppointmentNotFound_ThrowsBadHttpReqException()
    {
        //Arrange

        //Act

        //Assert
        Assert.Throws<BadHttpRequestException>(() => service.DeleteAppointment(1));
    }

    [Test]
    public void DeleteAppointment_AppointmentFound_RemovesAppointmentFromDb()
    {
        //Arrange
        Appointment _appointment = new Appointment
        {
            Date = new DateTime(2020, 1, 1, 0, 0, 0),
            StartTime = new DateTime(2020, 1, 1, 10, 0, 0),
            EndTime = new DateTime(2020, 1, 1, 11, 0, 0),
            Description = "test",
            Guest = _InMemoryDb.Persons.First(g => g.Id == 2),
            HairDresser = _InMemoryDb.Persons.First(g => g.Id == 1),
            Id = 1,
            Verified = false
        };

        _InMemoryDb.Appointments.Add(_appointment);
        _InMemoryDb.SaveChanges();

        //Act
        service.DeleteAppointment(1);
        var _result = _InMemoryDb.Appointments;

        //Assert
        Assert.That(_result, Is.Empty);
    }

    [Test]
    public void CheckIfHairDresser_NotHairDresser_ReturnsFalse()
    {
        //Arrange
        Person _person = new Person
        {
            EmailAddress = "test@test.com",
            FirstName = "test",
            LastName = "test",
            Gender = Gender.Male,
            Id = 1,
            PhoneNumber = "0101",
            Type = PersonType.Guest,
            User = _InMemoryDb.Users.First(u => u.Id == 2)
        };

        //Act
        var _result = service.CheckIfHairDresser(_person);

        //Assert
        Assert.That(_result, Is.False);
    }

    [Test]
    public void CheckIfHairDresser_IsDresser_ReturnsTrue()
    {
        //Arrange
        Person _person = new Person
        {
            EmailAddress = "test@test.com",
            FirstName = "test",
            LastName = "test",
            Gender = Gender.Male,
            Id = 1,
            PhoneNumber = "0101",
            Type = PersonType.HairDresser,
            User = _InMemoryDb.Users.First(u => u.Id == 1)
        };

        //Act
        var _result = service.CheckIfHairDresser(_person);

        //Assert
        Assert.That(_result, Is.True);
    }

    [Test]
    public void VerifyAppointment_NoAppointmentFound_ThrowsBadHttpReqException()
    {
        //Arrange

        //Act

        //Assert
        Assert.Throws<BadHttpRequestException>(() => service.VerifyAppointment(1));
    }

    [Test]
    public void VerifyAppointment_AppointmentFound_ChangesVerifiedFieldToTrue()
    {
        //Arrange
        Appointment _appointment = new Appointment
        {
            Date = new DateTime(2020, 1, 1, 0, 0, 0),
            StartTime = new DateTime(2020, 1, 1, 10, 0, 0),
            EndTime = new DateTime(2020, 1, 1, 11, 0, 0),
            Description = "test",
            Guest = _InMemoryDb.Persons.First(g => g.Id == 2),
            HairDresser = _InMemoryDb.Persons.First(g => g.Id == 1),
            Id = 1,
            Verified = false
        };

        _InMemoryDb.Appointments.Add(_appointment);
        _InMemoryDb.SaveChanges();

        //Act
        service.VerifyAppointment(1);
        var _result = _InMemoryDb.Appointments.FirstOrDefault(a => a.Id == 1).Verified;

        //Assert
        Assert.That(_result, Is.True);
    }
}