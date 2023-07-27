namespace HairSaloonAPI.Interfaces.Services;

public interface ILoginUserService
{
    /// <summary>
    /// Checks whether the username already exist in the database or not.
    /// </summary>
    /// <param name="username"></param>
    /// <returns>True if the username exist, otherwise False</returns>
    public bool CheckIfUsernameExist(string username);

    /// <summary>
    /// Hashes the password using the given salt and compares it to the database value.
    /// </summary>
    /// <param name="password"></param>
    /// <param name="passwordHash"></param>
    /// <param name="passwordSalt"></param>
    /// <returns>True if the values match, otherwise False</returns>
    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
}