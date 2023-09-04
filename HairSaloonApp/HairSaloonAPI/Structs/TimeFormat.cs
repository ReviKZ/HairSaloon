using System;

namespace HairSaloonAPI.Structs;

public struct TimeFormat
{
    private int _hour;
    private int _minute;
    private int _second;
    public int Hour 
    {
        get
        {
            return _hour;
        }
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Hour cannot be lower than 0");
            }

            if (value > 23)
            {
                throw new ArgumentOutOfRangeException("Hour cannot be higher than 23");
            }

            _hour = value;
        }
    }

    public int Minute
    {
        get
        {
            return _minute;
        }
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Minute cannot be lower than 0");
            }

            if (value > 59)
            {
                throw new ArgumentOutOfRangeException("Minute cannot be higher than 59");
            }

            _minute = value;
        }
    }
    public int Second
    {
        get
        {
            return _second;
        }
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Second cannot be lower than 0");
            }

            if (value > 59)
            {
                throw new ArgumentOutOfRangeException("Second cannot be higher than 59");
            }

            _second = value;
        }
    }
}