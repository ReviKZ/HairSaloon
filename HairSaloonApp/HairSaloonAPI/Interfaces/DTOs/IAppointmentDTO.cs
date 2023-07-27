using HairSaloonAPI.Models;

namespace HairSaloonAPI.Interfaces.DTOs;

public interface IAppointmentDTO
{
    public DateTime Date { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    public Person Guest { get; }
    public Person HairDresser { get; }
    public string Description { get; }
}