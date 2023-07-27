using System.ComponentModel.DataAnnotations;

namespace HairSaloonAPI.Interfaces.DTOs;

public interface IRegisterUserDTO
{
    [Required]
    public string UserName { get; }
    [Required, MinLength(6)]
    public string Password { get; }
    [Required, Compare("Password")]
    public string ConfirmPassword { get; }
}