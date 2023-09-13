using HairSaloonAPI.Interfaces.DTOs;

namespace HairSaloonAPI.Interfaces.Services;

public interface IRegisterUserService
{
    /// <summary>
    /// Checks whether the username already exist in the database or not.
    /// </summary>
    /// <param name="username"></param>
    /// <returns>True if the username exist, otherwise False</returns>
    bool CheckIfUsernameExist(string username);

    /// <summary>
    /// Hashes the password.
    /// </summary>
    /// <param name="password"></param>
    /// <param name="passwordHash"></param>
    /// <param name="passwordSalt"></param>
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

    /// <summary>
    /// Creates Random Token
    /// </summary>
    /// <returns></returns>
    string CreateToken();

    /// <summary>
    /// Creates the user in the database if everything is in order with the values.
    /// </summary>
    /// <param name=""></param>
    public void CreateUser(IRegisterUserDTO user);
}