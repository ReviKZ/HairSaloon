﻿namespace HairSaloonAPI.Interfaces.DTOs;

public interface IPersonDTO : IDTO
{
    public string _firstName { get; }
    public string _lastName { get; }
    public string _phoneNumber { get; }
    public string _emailAddress { get; }
}