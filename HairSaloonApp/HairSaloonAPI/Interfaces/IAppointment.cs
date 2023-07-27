using HairSaloonAPI.Interfaces.DTOs;

namespace HairSaloonAPI.Interfaces;

public interface IAppointment
{
    public int _id { get; }
    public DateTime _date { get; }
    public DateTime _startTime { get; }
    public DateTime _endTime { get; }
    public IPerson _guest { get; }
    public IPerson _hairDresser { get; }
    public string _description { get; }
    public bool _verified { get; set; }
}