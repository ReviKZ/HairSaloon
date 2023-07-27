using System.ComponentModel.DataAnnotations;
using HairSaloonAPI.Interfaces.DTOs;

namespace HairSaloonAPI.Models.DTOs;

public class LoginUserDTO : ILoginUserDTO
{
    [Required]
    public string _userName { get; }
    [Required, MinLength(6)]
    public string _password { get; }
}