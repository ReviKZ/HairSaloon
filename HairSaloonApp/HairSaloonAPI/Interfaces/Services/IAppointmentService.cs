using HairSaloonAPI.Interfaces.DTOs;

namespace HairSaloonAPI.Interfaces.Services;

public interface IAppointmentService
{
    /// <summary>
    /// Creates an appointment in the database using the recieved values.
    /// </summary>
    /// <param name="_appointment"></param>
    public void CreateAppointment(IAppointmentDTO _appointment);

    /// <summary>
    /// Updates the appointment in the database with the given id using the recieved values.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="_appointment"></param>
    public void UpdateAppointment(int id, IAppointmentDTO _appointment);

    /// <summary>
    /// Deletes the appointment in the database with the given id.
    /// </summary>
    /// <param name="id"></param>
    public void DeleteAppointment(int id);

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