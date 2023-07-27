using System.ComponentModel.DataAnnotations;
using HairSaloonAPI.Interfaces.DTOs;

namespace HairSaloonAPI.Models.DTOs;

public class LoginUserDTO : ILoginUserDTO
{
    [Required]
    public string UserName { get; set; }
    [Required, MinLength(6)]
    public string Password { get; set; }
}