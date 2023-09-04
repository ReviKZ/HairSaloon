namespace HairSaloonAPI.Interfaces.Services;

public interface IUserService
{
    /// <summary>
    /// Deletes the user with the given id from the database.
    /// </summary>
    /// <param name="id"></param>
    public void DeleteUser(int id);

    /// <summary>
    /// Get the id of last added user from the database.
    /// </summary>
    public int GetLastUserId();
}