using HairSaloonAPI.Models.DTOs;

namespace HairSaloonAPI.Interfaces.DTOs.ControllerDTOs;

public interface IRegisterDTO
{
    public RegisterUserDTO user { get; set; }
    public PersonDTO person { get; set; }
    public string personType { get; set; }
    public string gender { get; set; }

}