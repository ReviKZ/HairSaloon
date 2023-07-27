using System.ComponentModel.DataAnnotations;

namespace HairSaloonAPI.Interfaces.DTOs;

public interface ILoginUserDTO
{
    [Required]
    public string _userName { get; }
    [Required, MinLength(6)]
    public string _password { get; }
}