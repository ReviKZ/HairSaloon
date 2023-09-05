using HairSaloonAPI.Interfaces.DTOs;
using HairSaloonAPI.Interfaces.DTOs.ControllerDTOs;
using HairSaloonAPI.Models;
using HairSaloonAPI.Models.DTOs.ControllerDTOs;

namespace HairSaloonAPI.Interfaces.Services;

public interface IAppointmentService
{
    /// <summary>
    /// Convert the received values and creates a new appointment
    /// </summary>
    /// <param name="_appointment"></param>
    public void CreateAppointment(ICreateAppointmentDTO _appointment);

    /// <summary>
    /// Updates the appointment in the database with the given id using the received values.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="_appointment"></param>
    public void UpdateAppointment(int id, ICreateAppointmentDTO _appointment);

    /// <summary>
    /// Deletes the appointment in the database with the given id.
    /// </summary>
    /// <param name="id"></param>
    public void DeleteAppointment(int id);

    /// <summary>
    /// Gets all User related Appointments.
    /// </summary>
    /// <returns>Every Appointment with relations to the User</returns>
    public List<CreateAppointmentDTO> GetAppointmentListByUserId(int UserId);

    /// <summary>
    /// Gets the Appointment with the given id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>An Appointment with the matching id.</returns>
    public Appointment GetAppointment(int id);

    /// <summary>
    /// Changes the verification status of the appointment in the database with the given id.
    /// </summary>
    /// <param name="id"></param>
    public void VerifyAppointment(int id);

    /// <summary>
    /// A method for checking whether a Person is a Hair Dresser or not
    /// </summary>
    /// <param name="person"></param>
    /// <returns>True if it's a hair dresser, otherwise False.</returns>
    public bool CheckIfHairDresser(IPerson person);

}