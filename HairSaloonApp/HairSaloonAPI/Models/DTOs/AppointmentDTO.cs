using HairSaloonAPI.Interfaces;
using HairSaloonAPI.Interfaces.DTOs;

namespace HairSaloonAPI.Models.DTOs;

/// Author:
/// Kovács Zoárd Gábor
/// A6I2XW

public class AppointmentDTO : IAppointmentDTO
{
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Person Guest { get; set; }
    public Person HairDresser { get; set; }
    public string Description { get; set; }
}