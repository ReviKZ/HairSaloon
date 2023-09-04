using HairSaloonAPI.Data;
using HairSaloonAPI.Interfaces;
using HairSaloonAPI.Interfaces.DTOs;
using HairSaloonAPI.Interfaces.DTOs.ControllerDTOs;
using HairSaloonAPI.Interfaces.Services;
using HairSaloonAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HairSaloonAPI.Services;

public class AppointmentService : IAppointmentService
{   
    private DataContext _db;

    public AppointmentService(DataContext db)
    {
        _db = db;
    }

    public void CreateAppointment(ICreateAppointmentDTO appointment)
    {
        if (!(_db.Users.Any(u => u.Id == appointment.HairDresserId) &&
              _db.Users.Any(u => u.Id == appointment.GuestId)))
        {
            throw new BadHttpRequestException("The HairDresser or The Guest was not found");
        }

        if (!CheckIfHairDresser(_db.Persons.First(u => u.User.Id == appointment.HairDresserId)))
        {
            throw new BadHttpRequestException("You put a Guest as a HairDresser");
        }

        Appointment _appointment = new Appointment
        {
            Date = new DateTime(appointment.Date.Year, appointment.Date.Month, appointment.Date.Day,
                0, 0, 0),
            Description = appointment.Description,
            StartTime = new DateTime(appointment.Date.Year, appointment.Date.Month, appointment.Date.Day,
                appointment.StartTime.Hour, appointment.StartTime.Minute, appointment.StartTime.Second),
            EndTime = new DateTime(appointment.Date.Year, appointment.Date.Month, appointment.Date.Day,
                appointment.EndTime.Hour, appointment.EndTime.Minute, appointment.EndTime.Second),
            HairDresser = _db.Persons.First(u => u.User.Id == appointment.HairDresserId),
            Guest = _db.Persons.First(u => u.User.Id == appointment.GuestId),
            Verified = false

        };

        _db.Appointments.Add(_appointment);
        _db.SaveChanges();

    }

    public Appointment GetAppointment(int id)
    {
        Appointment _appointment = _db.Appointments.First(a => a.Id == id);
        return _appointment;
    }

    public List<Appointment> GetAppointmentList()
    {
        List<Appointment> _appointments = _db.Appointments.ToList();
        return _appointments;
    }

    public void UpdateAppointment(int id, ICreateAppointmentDTO appointment)
    {
        if (!_db.Appointments.Any(a => a.Id == id))
        {
            throw new BadHttpRequestException("This Appointment doesn't exist");
        }

        if (!CheckIfHairDresser(_db.Persons.First(u => u.User.Id == appointment.HairDresserId)))
        {
            throw new BadHttpRequestException("You put a Guest as a HairDresser");
        }

        Appointment _appointment = _db.Appointments.First(a => a.Id == id);
        _appointment.Date = new DateTime(appointment.Date.Year, appointment.Date.Month, appointment.Date.Day,
            0, 0, 0);
        _appointment.Description = appointment.Description;
        _appointment.StartTime = new DateTime(appointment.Date.Year, appointment.Date.Month, appointment.Date.Day,
            appointment.StartTime.Hour, appointment.StartTime.Minute, appointment.StartTime.Second);
        _appointment.EndTime = new DateTime(appointment.Date.Year, appointment.Date.Month, appointment.Date.Day,
            appointment.EndTime.Hour, appointment.EndTime.Minute, appointment.EndTime.Second);
        _appointment.HairDresser = _db.Persons.First(u => u.User.Id == appointment.HairDresserId);
        _appointment.Guest = _db.Persons.First(u => u.User.Id == appointment.GuestId);
        _appointment.Verified = false;

        _db.SaveChanges();
    }

    public void DeleteAppointment(int id)
    {
        Appointment _appointment = _db.Appointments.First(a => a.Id == id);

        _db.Appointments.Remove(_appointment);
        _db.SaveChanges();
    }

    public bool CheckIfHairDresser(IPerson person)
    {
        if (person.Type.ToString() == "HairDresser")
        {
            return true;
        }

        return false;
    }

    public void VerifyAppointment(int id)
    {
        Appointment _appointment = _db.Appointments.First(a => a.Id == id);
        _appointment.Verified = true;

        _db.SaveChanges();
    }
}