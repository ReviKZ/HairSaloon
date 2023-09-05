using HairSaloonAPI.Data;
using HairSaloonAPI.Interfaces;
using HairSaloonAPI.Interfaces.DTOs;
using HairSaloonAPI.Interfaces.DTOs.ControllerDTOs;
using HairSaloonAPI.Interfaces.Services;
using HairSaloonAPI.Models;
using HairSaloonAPI.Models.DTOs.ControllerDTOs;
using HairSaloonAPI.Structs;
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

    public CreateAppointmentDTO GetAppointment(int id)
    {
        if (!_db.Appointments.Any(a => a.Id == id))
        {
            throw new BadHttpRequestException("There isn't an appointment with that Id");
        }

        Appointment _appointment = _db.Appointments
            .Include(a => a.HairDresser.User)
            .Include(a => a.Guest.User)
            .First(a => a.Id == id);
        CreateAppointmentDTO _appointmentDto = new CreateAppointmentDTO
        {
            Date = new DateFormat(_appointment.Date.Year, _appointment.Date.Month, _appointment.Date.Day),
            StartTime = new TimeFormat(_appointment.StartTime.Hour, _appointment.StartTime.Minute, _appointment.StartTime.Second),
            EndTime = new TimeFormat(_appointment.EndTime.Hour, _appointment.EndTime.Minute, _appointment.EndTime.Second),
            GuestId = _appointment.Guest.User.Id,
            HairDresserId = _appointment.HairDresser.User.Id,
            Description = _appointment.Description
        };
        return _appointmentDto;
    }

    public List<CreateAppointmentDTO> GetAppointmentListByUserId(int userId)
    {
        if (!_db.Users.Any(u => u.Id == userId))
        {
            throw new BadHttpRequestException("There isn't a user with that Id");
        }

        List<Appointment> _appointments = _db.Appointments
            .Include(a => a.HairDresser.User)
            .Include(a => a.Guest.User)
            .Where(a => a.Guest.User.Id == userId || a.HairDresser.User.Id == userId)
            .ToList();
        List<CreateAppointmentDTO> _appointmentList = new List<CreateAppointmentDTO>();
        foreach (Appointment appointment in _appointments)
            _appointmentList.Add(new CreateAppointmentDTO
            {
                Date = new DateFormat(appointment.Date.Year, appointment.Date.Month, appointment.Date.Day),
                StartTime = new TimeFormat(appointment.StartTime.Hour, appointment.StartTime.Minute, appointment.StartTime.Second),
                EndTime = new TimeFormat(appointment.EndTime.Hour, appointment.EndTime.Minute, appointment.EndTime.Second),
                GuestId = appointment.Guest.User.Id,
                HairDresserId = appointment.HairDresser.User.Id,
                Description = appointment.Description
            });
        return _appointmentList;
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
        if (!_db.Appointments.Any(a => a.Id == id))
        {
            throw new BadHttpRequestException("This Appointment doesn't exist");
        }

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
        if (!_db.Appointments.Any(a => a.Id == id))
        {
            throw new BadHttpRequestException("This Appointment doesn't exist");
        }

        Appointment _appointment = _db.Appointments.First(a => a.Id == id);
        _appointment.Verified = true;

        _db.SaveChanges();
    }
}