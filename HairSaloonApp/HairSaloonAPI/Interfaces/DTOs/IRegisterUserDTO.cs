using System.ComponentModel.DataAnnotations;

namespace HairSaloonAPI.Interfaces.DTOs;

public interface IRegisterUserDTO
{
    [Required]
    public string _userName { get; }
    [Required, MinLength(6)]
    public string _password { get; }
    [Required, Compare("_password")]
    public string _confirmPassword { get; }
}