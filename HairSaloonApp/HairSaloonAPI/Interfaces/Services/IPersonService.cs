using HairSaloonAPI.Interfaces.DTOs;
using HairSaloonAPI.Models;

namespace HairSaloonAPI.Interfaces.Services;

public interface IPersonService
{
    /// <summary>
    /// Creates a person in the database using the logged in user, a chosen gender, a chosen type and given information.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="gender"></param>
    /// <param name="PersonType"></param>
    /// <param name="personInfo"></param>
    public void CreatePerson(int userId, string gender, string PersonType, IPersonDTO personInfo);

    /// <summary>
    /// Updates the person in the database with the given id using received values.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="personInfo"></param>
    public void EditPerson(int id, IPersonDTO personInfo);

    /// <summary>
    /// Removes the person with the given id from the database.
    /// </summary>
    /// <param name="id"></param>
    public void DeletePerson(int id);

    /// <summary>
    /// Get the Person with the given id from the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>The Person with the matching id.</returns>
    public Person GetPerson(int id);
}