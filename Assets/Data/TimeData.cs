using System;

[Serializable]
public class TimeData
{
    public int Hours;
    public int Minutes;

    public string Get12HourString()
    {
        return Get12HourString(Hours, Minutes);
    }

    public static string Get12HourString(int hours, int minutes)
    {
        (string, string) hoursValues = Convert24HourTo12Hour(hours);
        return $"{hoursValues.Item1}:{minutes} {hoursValues.Item2}";
    }

    /// <summary>
    /// Converts a 24 hour alue to a 12 hour value
    /// </summary>
    /// <param name="hour"> the hour value in 24 hour formt</param>
    /// <returns> a Tuple where the first value is the 12 hour value and the second is the AM/PM value</returns>
    private static (string, string) Convert24HourTo12Hour(int hour)
    {
        if (hour < 12)
        {
            if (hour == 0)
            {
                hour = 12;
            }

            return (hour.ToString(), "AM");
        }
        else
        {
            if (hour != 12)
            {
                hour -= 12;
            }            

            return (hour.ToString(), "PM");
        }
    }

    public static string ConvertMinutesTo12HourString(int minutes)
    {
        int hours = minutes / 60;
        minutes %= 60;
        return Get12HourString(hours, minutes);
    }

    public static int TimeToMinutes(TimeData timeData)
    {
        return timeData.Hours * 60 + timeData.Minutes;
    }








    //might not need any below
    public override bool Equals(Object o)
    {
        if (o is TimeData timeData)
        {
            return this == timeData;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return TimeToMinutes(this).GetHashCode();
    }

    public static bool operator ==(TimeData left, TimeData right)
    {
        return left.Hours == right.Hours && left.Minutes == right.Minutes;
    }

    public static bool operator !=(TimeData left, TimeData right)
    {
        return !(left == right);
    }

    public static bool operator <(TimeData left, TimeData right)
    {
        return TimeToMinutes(left) < TimeToMinutes(right);
    }

    public static bool operator >(TimeData left, TimeData right)
    {
        return TimeToMinutes(left) > TimeToMinutes(right);
    }

    public static bool operator <=(TimeData left, TimeData right)
    {
        return !(left > right);
    }

    public static bool operator >=(TimeData left, TimeData right)
    {
        return !(left < right);
    }
}
