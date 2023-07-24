namespace HairSaloonAPI.Interfaces.DTOs;

public interface IAppointment
{
    public int _id { get; }
    public DateOnly _date { get; }
    public TimeOnly _startTime { get; }
    public TimeOnly _endTime { get; }
    public IGuest _guest { get; }
    public IHairDresser _hairDresser { get; }
    public string _description { get; }
    public bool _verified { get; set; }

    public void EditAppointment(IAppointmentDTO updatedDTO);
    public void Verify();
}