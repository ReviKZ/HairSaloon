namespace HairSaloonAPI.Interfaces.DTOs;

public interface IAppointmentDTO : IDTO
{
    public DateOnly _date { get; }
    public TimeOnly _startTime { get; }
    public TimeOnly _endTime { get; }
    public string _description { get; }
}