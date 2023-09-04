using HairSaloonAPI.Interfaces.DTOs;
using HairSaloonAPI.Interfaces.DTOs.ControllerDTOs;

namespace HairSaloonAPI.Models.DTOs.ControllerDTOs;

public class RegisterDTO : IRegisterDTO
{
    public RegisterUserDTO user { get; set; }
    public PersonDTO person { get; set; }
    public string personType { get; set; }
    public string gender { get; set; }

}