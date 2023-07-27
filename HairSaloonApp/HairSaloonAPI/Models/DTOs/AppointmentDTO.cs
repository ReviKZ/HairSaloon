using HairSaloonAPI.Interfaces;
using HairSaloonAPI.Interfaces.DTOs;

namespace HairSaloonAPI.Models.DTOs;

public class AppointmentDTO : IAppointmentDTO
{
    public DateOnly _date { get; set; }
    public TimeOnly _startTime { get; set; }
    public TimeOnly _endTime { get; set; }
    public IPerson _guest { get; set; }
    public IPerson _hairDresser { get; set; }
    public string _description { get; set; }
}