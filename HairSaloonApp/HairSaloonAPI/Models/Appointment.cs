using HairSaloonAPI.Interfaces;

namespace HairSaloonAPI.Models;

public class Appointment : IAppointment
{
    public int _id { get; set; }
    public DateTime _date { get; set; }
    public DateTime _startTime { get; set; }
    public DateTime _endTime { get; set; }
    public Person _guest { get; set; }
    public Person _hairDresser { get; set; }
    public string _description { get; set; }
    public bool _verified { get; set; }
}