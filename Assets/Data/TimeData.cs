using System;

[Serializable]
public class TimeData
{
    public int Hours;
    public int Minutes;

    public string Get12HourString()
    {
        (string, string) hoursValues = Convert24HourTo12Hour(Hours);
        string minutes = Minutes.ToString();
        return $"{hoursValues.Item1}:{minutes} {hoursValues.Item2}";
    }

    /// <summary>
    /// Converts a 24 hour alue to a 12 hour value
    /// </summary>
    /// <param name="hour"> the hour value in 24 hour formt</param>
    /// <returns> a Tuple where the first value is the 12 hour value and the second is the AM/PM value</returns>
    private (string, string) Convert24HourTo12Hour(int hour)
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
}
