using HairSaloonAPI.Interfaces.DTOs;
using HairSaloonAPI.Models;

namespace HairSaloonAPI.Interfaces;

public interface IAppointment
{
    public int _id { get; }
    public DateTime _date { get; }
    public DateTime _startTime { get; }
    public DateTime _endTime { get; }
    public Person _guest { get; }
    public Person _hairDresser { get; }
    public string _description { get; }
    public bool _verified { get; set; }
}