using System.ComponentModel.DataAnnotations;

namespace HairSaloonAPI.Interfaces.DTOs;

public interface ILoginUserDTO
{
    [Required]
    public string UserName { get; }
    [Required, MinLength(6)]
    public string Password { get; }
}