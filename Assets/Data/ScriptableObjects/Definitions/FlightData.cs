using UnityEngine;

[CreateAssetMenu(fileName = "FlightData", menuName = "Scriptable Objects/FlightData")]
public class FlightData : ScriptableObject
{
    public int ID;

    public AirportName DepartureCity;
    public AirportName ArrivalCity;

    public TimeData DepartureTime;
    public TimeData ArrivalTime;

    public int[] Seats;

    public float SpeedMultiplier = 1.0f;

}
