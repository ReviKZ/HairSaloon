using HairSaloonAPI.Data;
using HairSaloonAPI.Enums;
using HairSaloonAPI.Interfaces;
using HairSaloonAPI.Interfaces.DTOs;
using HairSaloonAPI.Interfaces.Services;
using HairSaloonAPI.Models;

namespace HairSaloonAPI.Services;

public class PersonService : IPersonService
{
    private DataContext _db;

    public PersonService(DataContext db)
    {
        _db = db;
    }

    public void CreatePerson(int userId, string gender, string personType, IPersonDTO personInfo)
    {
        Person _person = new Person();

        _person.User = _db.Users.First(u => u.Id == userId);

        var _gender = new Gender();
        {
            if (gender == Gender.Male.ToString())
            {
               _gender = Gender.Male;
            }
            else if (gender == Gender.Female.ToString())
            {
                _gender = Gender.Female;
            }
            else
            {
                _gender = Gender.Else;
            }
        };
        _person.Gender = _gender;

        var _personType = new PersonType();
        {
            if (personType == PersonType.HairDresser.ToString())
            {
                _personType = PersonType.HairDresser;
            }
            else
            {
                _personType = PersonType.Guest;
            }
        };
        _person.Type = _personType;

        _person.FirstName = personInfo.FirstName;
        _person.LastName = personInfo.LastName;
        _person.EmailAddress = personInfo.EmailAddress;
        _person.PhoneNumber = personInfo.PhoneNumber;

        _db.Persons.Add(_person);
        _db.SaveChanges();

    }

    public Person GetPerson(int id)
    {
        Person _person = _db.Persons.First(u => u.Id == id);

        return _person;
    }

    public void EditPerson(int id, IPersonDTO personInfo)
    {
        Person _person = _db.Persons.First(u => u.Id == id);

        _person.FirstName = personInfo.FirstName;
        _person.LastName = personInfo.LastName;
        _person.EmailAddress = personInfo.EmailAddress;
        _person.PhoneNumber = personInfo.PhoneNumber;

        _db.SaveChanges();
    }

    public void DeletePerson(int id)
    {
        Person _person = _db.Persons.First(u => u.Id == id);

        _db.Remove(_person);
        _db.SaveChanges();
    }
}