using UnityEngine;

[CreateAssetMenu(fileName = "TripData", menuName = "Scriptable Objects/TripData")]
public class TripData : ScriptableObject
{
    public AirportName StartCity;
    public AirportName DestinationCity;

    public TimeData StartTime;

    public PassengerData PassengerData;
}
