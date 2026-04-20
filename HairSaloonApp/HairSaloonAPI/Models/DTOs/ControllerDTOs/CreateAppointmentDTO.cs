using HairSaloonAPI.Interfaces.DTOs.ControllerDTOs;
using HairSaloonAPI.Structs;

namespace HairSaloonAPI.Models.DTOs.ControllerDTOs;

/// Author:
/// Kovács Zoárd Gábor
/// A6I2XW
public class CreateAppointmentDTO : ICreateAppointmentDTO
{
    public DateFormat Date { get; set; }
    public TimeFormat StartTime { get; set; }
    public TimeFormat EndTime { get; set; }
    public int GuestId { get; set; }
    public int HairDresserId { get; set; }
    public string Description { get; set; }

}