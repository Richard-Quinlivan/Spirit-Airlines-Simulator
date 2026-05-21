using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Plane : MonoBehaviour
{
    private Airport _startingLocation;
    private Airport _endingLocation;
    private FlightData _flightData;
    private float _flightStartTime;
    private int _flightTimeInRealSeconds;


    public void SetTrip(Airport startingLocation, Airport endingLocation, FlightData flightData)
    {
        _startingLocation = startingLocation;
        _endingLocation = endingLocation;
        _flightData = flightData;
        int flightTimeInGame = TimeData.TimeToMinutes(flightData.ArrivalTime) - TimeData.TimeToMinutes(flightData.DepartureTime);
        _flightTimeInRealSeconds = flightTimeInGame / GameClock.TimeRatio;
        _flightStartTime = Time.time;
        gameObject.SetActive(true);
    }


    private void EndFlight()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        float progress = Mathf.Clamp01((Time.time - _flightStartTime) / _flightTimeInRealSeconds);
        transform.position = Vector2.Lerp
        (
            _startingLocation.transform.position,
            _endingLocation.transform.position,
            progress
        );
        if (progress == 1)
        {
            EndFlight();
        }
    }
}
