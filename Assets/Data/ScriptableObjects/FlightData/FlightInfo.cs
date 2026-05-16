using UnityEngine;

[CreateAssetMenu(fileName = "FlightInfo", menuName = "Scriptable Objects/FlightInfo")]
public class FlightInfo : ScriptableObject
{
    public int ID;

    public Airport Start;
    public Airport Destination;

    public TimeData StartArriveTime;
    public TimeData DestinationArriveTime;

    public int[] AvailableSeats;

    public float SpeedMultiplier = 1.0f;

}
