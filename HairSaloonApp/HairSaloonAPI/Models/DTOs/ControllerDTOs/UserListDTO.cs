using HairSaloonAPI.Interfaces.DTOs.ControllerDTOs;

namespace HairSaloonAPI.Models.DTOs.ControllerDTOs;

public class UserListDTO : IUserListDTO
{
    public UserListDTO(string firstName, string lastName, int userId)
    {
        Name = $"{firstName} {lastName}";
        UserId = userId;

    }
    public string Name { get; set; }
    public int UserId { get; set; }
}