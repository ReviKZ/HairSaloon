using HairSaloonAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HairSaloonAPI.Data;

public class DataContext : DbContext
{
    public DataContext()
    {
    }
    public DataContext(DbContextOptions<DbContext> options) : base(options)
    {
            
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder
            .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=HairSaloonDB;Trusted_Connection=true;");
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
}