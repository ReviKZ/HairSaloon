using HairSaloonAPI.Interfaces.DTOs;

namespace HairSaloonAPI.Interfaces;

public interface IAppointment
{
    public int _id { get; }
    public DateOnly _date { get; }
    public TimeOnly _startTime { get; }
    public TimeOnly _endTime { get; }
    public IPerson _guest { get; }
    public IPerson _hairDresser { get; }
    public string _description { get; }
    public bool _verified { get; set; }
}