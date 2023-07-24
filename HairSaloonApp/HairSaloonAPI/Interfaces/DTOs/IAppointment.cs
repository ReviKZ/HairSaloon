namespace HairSaloonAPI.Interfaces.DTOs;

public interface IAppointment
{
    // Properties

    public int _id { get; }
    public DateOnly _date { get; }
    public TimeOnly _startTime { get; }
    public TimeOnly _endTime { get; }
    public IGuest _guest { get; }
    public IHairDresser _hairDresser { get; }
    public string _description { get; }
    public bool _verified { get; set; }


    // Methods

    /// <summary>
    /// Updates the Appointment information
    /// </summary>
    /// <param name="updatedDTO">DTO format coming from the HTTP request</param>
    public void EditAppointment(IAppointmentDTO updatedDTO);

    /// <summary>
    /// Verifies that the Appointment information are good
    /// </summary>
    public void Verify();
}