using HairSaloonAPI.Interfaces;

namespace HairSaloonAPI.Models;

public class Appointment : IAppointment
{
    public int _id { get; set; }
    public DateOnly _date { get; set; }
    public TimeOnly _startTime { get; set; }
    public TimeOnly _endTime { get; set; }
    public IPerson _guest { get; set; }
    public IPerson _hairDresser { get; set; }
    public string _description { get; set; }
    public bool _verified { get; set; }
}