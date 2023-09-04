using HairSaloonAPI.Data;
using HairSaloonAPI.Interfaces.Services;
using HairSaloonAPI.Models;

namespace HairSaloonAPI.Services;

public class UserService : IUserService
{
    private DataContext _db;

    public UserService(DataContext db)
    {
        _db = db;
    }

    public void DeleteUser(int id)
    {
        User _user = _db.Users.First(u => u.Id == id);

        _db.Users.Remove(_user);
        _db.SaveChanges();
    }

    public int GetLastUserId()
    {
        User _lastUser = _db.Users.OrderByDescending(u => u.Id).First();
        return _lastUser.Id;
    }
}