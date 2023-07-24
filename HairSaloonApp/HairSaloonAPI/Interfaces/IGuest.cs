using HairSaloonAPI.Interfaces.DTOs;

namespace HairSaloonAPI.Interfaces;

public interface IGuest
{
    // Properties

    public int _id { get; }
    public string _firstName { get; }
    public string _lastName { get; }
    public string _phoneNumber { get; }
    public string _emailAddress { get; }


    //Methods

    /// <summary>
    /// Edit Guest information
    /// </summary>
    /// <param name="updatedDTO">DTO format coming from the HTTP request</param>
    public void Edit(IGuestDTO updatedDTO);
}