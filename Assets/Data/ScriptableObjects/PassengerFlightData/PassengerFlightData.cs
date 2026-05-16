using UnityEngine;

[CreateAssetMenu(fileName = "PassengerFlightData", menuName = "Scriptable Objects/PassengerFlightData")]
public class PassengerFlightData : ScriptableObject
{
    public Airport Start;
    public Airport Destination;

    public TimeData ArriveAtTime;
}
