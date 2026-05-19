using UnityEngine;

[CreateAssetMenu(fileName = "FlightData", menuName = "Scriptable Objects/FlightData" +
    "")]
public class FlightData : ScriptableObject
{
    public int ID;

    public Airport Start;
    public Airport Destination;

    public TimeData StartArriveTime;
    public TimeData DestinationArriveTime;

    public int[] Seats;

    public float SpeedMultiplier = 1.0f;

}
