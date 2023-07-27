using System.ComponentModel.DataAnnotations;
using HairSaloonAPI.Interfaces;

namespace HairSaloonAPI.Models;

public class User : IUser
{
    [Key]
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = new byte[32];
    public byte[] PasswordSalt { get; set; } = new byte[32];
}