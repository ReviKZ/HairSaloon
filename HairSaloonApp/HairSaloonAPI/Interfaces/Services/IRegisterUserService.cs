using HairSaloonAPI.Interfaces.DTOs;

namespace HairSaloonAPI.Interfaces.Services;

public interface IRegisterUserService
{
    /// <summary>
    /// Checks whether the username already exist in the database or not.
    /// </summary>
    /// <param name="username"></param>
    /// <returns>True if the username exist, otherwise False</returns>
    public bool CheckIfUsernameExist(string username);

    /// <summary>
    /// Hashes the password.
    /// </summary>
    /// <param name="password"></param>
    /// <param name="passwordHash"></param>
    /// <param name="passwordSalt"></param>
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

    /// <summary>
    /// Creates the user in the database if everything is in order with the values.
    /// </summary>
    /// <param name=""></param>
    public int CreateUser(IRegisterUserDTO user);
}