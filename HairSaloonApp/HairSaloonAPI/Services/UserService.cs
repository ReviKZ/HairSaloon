using HairSaloonAPI.Data;
using HairSaloonAPI.Enums;
using HairSaloonAPI.Interfaces.Services;
using HairSaloonAPI.Models;
using HairSaloonAPI.Models.DTOs.ControllerDTOs;
using Microsoft.EntityFrameworkCore;

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

    public List<UserListDTO> GetAllUsers()
    {
        List<Person> dbUserList = _db.Persons.Include(p => p.User).ToList();
        List<UserListDTO> users = new List<UserListDTO>();
        dbUserList.ForEach(p => users.Add(new UserListDTO(p.FirstName, p.LastName, p.User.Id)));

        return users;
    }

    public List<UserListDTO> GetAllHairDressers()
    {
        List<Person> dbUserList = _db.Persons.Where(p => p.Type == PersonType.HairDresser).ToList();
        List<UserListDTO> users = new List<UserListDTO>();
        dbUserList.ForEach(p => users.Add(new UserListDTO(p.FirstName, p.LastName, p.User.Id)));

        return users;
    }

    public int ConvertTokenToId(string token)
    {
        if (!_db.Users.Any(u => u.Token == token))
        {
            throw new BadHttpRequestException("No User was found");
        }

        return _db.Users.First(u => u.Token == token).Id;
    }
}