using System.ComponentModel.DataAnnotations;
using HairSaloonAPI.Interfaces.DTOs;

namespace HairSaloonAPI.Models.DTOs;

public class RegisterUserDTO : IRegisterUserDTO
{
    [Required]
    public string _userName { get; set; }
    [Required, MinLength(6)]
    public string _password { get; set; }
    [Required, Compare("_password")]
    public string _confirmPassword { get; set; }
}