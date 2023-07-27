namespace HairSaloonAPI.Interfaces.DTOs;

public interface IAppointmentDTO
{
    public DateTime _date { get; }
    public DateTime _startTime { get; }
    public DateTime _endTime { get; }
    public IPerson _guest { get; }
    public IPerson _hairDresser { get; }
    public string _description { get; }
}