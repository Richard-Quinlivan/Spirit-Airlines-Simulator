using System.Collections.Generic;

public class SortFlightDataByDepartureTime : IComparer<FlightData>
{
    public int Compare(FlightData x, FlightData y)
    {
        int xVal = TimeData.TimeToMinutes(x.DepartureTime);
        int yVal = TimeData.TimeToMinutes(y.DepartureTime);

        return xVal.CompareTo(yVal);
    }
}
