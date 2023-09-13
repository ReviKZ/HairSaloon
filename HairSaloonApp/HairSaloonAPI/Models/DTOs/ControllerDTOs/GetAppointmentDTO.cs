using HairSaloonAPI.Structs;

namespace HairSaloonAPI.Models.DTOs.ControllerDTOs;

public class GetAppointmentDTO
{
    public int Id { get; set; }
    public DateFormat Date { get; set; }
    public TimeFormat StartTime { get; set; }
    public TimeFormat EndTime { get; set; }
    public int GuestId { get; set; }
    public int HairDresserId { get; set; }
    public string Description { get; set; }
    public bool Verified { get; set; }
}