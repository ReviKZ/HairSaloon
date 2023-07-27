using HairSaloonAPI.Models;

namespace HairSaloonAPI.Interfaces.DTOs;

public interface IAppointmentDTO
{
    public DateTime _date { get; }
    public DateTime _startTime { get; }
    public DateTime _endTime { get; }
    public Person _guest { get; }
    public Person _hairDresser { get; }
    public string _description { get; }
}