using System.ComponentModel.DataAnnotations;
using HairSaloonAPI.Interfaces;

namespace HairSaloonAPI.Models;

public class Appointment : IAppointment
{
    [Key]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Person Guest { get; set; }
    public Person HairDresser { get; set; }
    public string Description { get; set; }
    public bool Verified { get; set; }
}