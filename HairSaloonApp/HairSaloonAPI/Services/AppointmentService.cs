using HairSaloonAPI.Data;
using HairSaloonAPI.Interfaces;
using HairSaloonAPI.Interfaces.DTOs;
using HairSaloonAPI.Interfaces.Services;
using HairSaloonAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HairSaloonAPI.Services;

public class AppointmentService : IAppointmentService
{   
    private DataContext _db;

    public AppointmentService(DataContext db)
    {
        _db = db;
    }

    public void CreateAppointment(IAppointmentDTO appointment)
    {
        Appointment _appointment = new Appointment();
        _appointment.Date = appointment.Date;
        _appointment.Description = appointment.Description;
        _appointment.StartTime = appointment.StartTime;
        _appointment.EndTime = appointment.EndTime;
        _appointment.HairDresser = appointment.HairDresser;
        _appointment.Guest = appointment.Guest;
        _appointment.Verified = false;

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

    public void UpdateAppointment(int id, IAppointmentDTO appointment)
    {
        Appointment _appointment = _db.Appointments.First(a => a.Id == id);
        _appointment.Date = appointment.Date;
        _appointment.Description = appointment.Description;
        _appointment.StartTime = appointment.StartTime;
        _appointment.EndTime = appointment.EndTime;
        _appointment.HairDresser = appointment.HairDresser;
        _appointment.Guest = appointment.Guest;
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