namespace HairSaloonAPI.Structs;

public struct DateFormat
{
    private int _year;
    private int _month;
    private int _day;

    public DateFormat(int year, int month, int day)
    {
        _year = year;
        _month = month;
        _day = day;
    }
    public int Year
    {
        get
        {
            return _year;
        }
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Year cannot be lower than 0");
            }

            if (value > 2999)
            {
                throw new ArgumentOutOfRangeException("Year cannot be higher than 2999");
            }

            _year = value;
        }
    }
    public int Month
    {
        get
        {
            return _month;
        }
        set
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException("Month cannot be lower than 1");
            }

            if (value > 12)
            {
                throw new ArgumentOutOfRangeException("Month cannot be higher than 12");
            }

            _month = value;
        }
    }
    public int Day
    {
        get
        {
            return _day;
        }
        set
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException("Day cannot be lower than 1");
            }

            if (value > 31)
            {
                throw new ArgumentOutOfRangeException("Day cannot be higher than 31");
            }

            _day = value;
        }
    }
}