﻿namespace HairSaloonAPI.Interfaces;

public interface IPerson
{
    public int _id { get; }
    public string _firstName { get; }
    public string _lastName { get; }
    public string _phoneNumber { get; }
    public string _emailAddress { get; }
}