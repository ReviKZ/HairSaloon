using HairSaloonAPI.Interfaces;
using HairSaloonAPI.Interfaces.DTOs;

namespace HairSaloonAPI.Models.DTOs;

public class AppointmentDTO : IAppointmentDTO
{
    public DateTime _date { get; set; }
    public DateTime _startTime { get; set; }
    public DateTime _endTime { get; set; }
    public Person _guest { get; set; }
    public Person _hairDresser { get; set; }
    public string _description { get; set; }
}