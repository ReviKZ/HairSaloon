using HairSaloonAPI.Enums;
using HairSaloonAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace HairSaloonAPI.Data;

public class DataContext : DbContext
{
    public DataContext()
    {
    }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        byte[] _passwordHash;
        byte[] _passwordSalt;
        using (var hmac = new HMACSHA512())
        {
            _passwordSalt = hmac.Key;
            _passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("admin01"));
        }
        string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        User _user = new User { Id = 1, PasswordHash = _passwordHash, PasswordSalt = _passwordSalt, Token = token, Username = "admin" };

        Person _hairDresser = new Person
        {
            EmailAddress = "hd@test.com",
            FirstName = "hair",
            LastName = "dresser",
            Gender = Gender.Male,
            Id = 1,
            PhoneNumber = "0101",
            Type = PersonType.HairDresser,
            UserId = 1,
        };

        modelBuilder.Entity<User>().HasData(_user);
        modelBuilder.Entity<Person>().HasData(_hairDresser);

        base.OnModelCreating(modelBuilder);
    }
}