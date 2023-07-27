using HairSaloonAPI.Interfaces.DTOs;
using HairSaloonAPI.Models;

namespace HairSaloonAPI.Interfaces;

public interface IAppointment
{
    public int Id { get; }
    public DateTime Date { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    public Person Guest { get; }
    public Person HairDresser { get; }
    public string Description { get; }
    public bool Verified { get; set; }
}