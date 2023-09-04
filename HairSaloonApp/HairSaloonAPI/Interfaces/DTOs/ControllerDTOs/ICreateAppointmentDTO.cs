﻿using HairSaloonAPI.Structs;

namespace HairSaloonAPI.Interfaces.DTOs.ControllerDTOs;

public interface ICreateAppointmentDTO
{
    public DateFormat Date { get; set; }
    public TimeFormat StartTime { get; set; }
    public TimeFormat EndTime { get; set; }
    public int GuestId { get; set; }
    public int HairDresserId { get; set; }
    public string Description { get; set; }
}