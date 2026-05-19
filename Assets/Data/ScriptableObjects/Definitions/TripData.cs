using UnityEngine;

[CreateAssetMenu(fileName = "TripData", menuName = "Scriptable Objects/TripData")]
public class TripData : ScriptableObject
{
    public Airport Start;
    public Airport Destination;

    public TimeData ArriveAtTime;

    public PassengerData PassengerData;
}
