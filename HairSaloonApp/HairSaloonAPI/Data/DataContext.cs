using HairSaloonAPI.Models;
using Microsoft.EntityFrameworkCore;

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
}