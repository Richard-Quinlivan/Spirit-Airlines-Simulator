using UnityEngine;

[CreateAssetMenu(fileName = "TripData", menuName = "Scriptable Objects/TripData")]
public class TripData : ScriptableObject
{
    public AirportName Start;
    public AirportName Destination;

    public TimeData StartTime;

    public PassengerData PassengerData;
}
