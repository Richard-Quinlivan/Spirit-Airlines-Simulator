using TMPro;
using UnityEngine;

public class Flight : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _departureCity;

    [SerializeField]
    private TextMeshProUGUI _arrivalCity;

    [SerializeField]
    private TextMeshProUGUI _departureTime;

    [SerializeField]
    private TextMeshProUGUI _arrivalTime;

    private FlightData _data;
    private GameClock _clock;
    private PlanePool _planePool;

    private int _departTimeInMinutes;

    public void Initialize(FlightData data, GameClock clock, PlanePool planePool)
    {
        _data = data;
        _clock = clock;
        _planePool = planePool;
        _departTimeInMinutes = TimeData.TimeToMinutes(data.DepartureTime);

        _departureCity.text = data.DepartureCity.ToString();
        _arrivalCity.text = data.ArrivalCity.ToString();

        _departureTime.text = data.DepartureTime.Get12HourString();
        _arrivalTime.text = data.ArrivalTime.Get12HourString();
        _clock.OnTimeUpdated += CheckDepartureTime;
    }

    private void OnDestroy()
    {
        if (_clock != null)
        {
            _clock.OnTimeUpdated -= CheckDepartureTime;
        }
    }

    private void CheckDepartureTime(int currTime)
    {
        if (currTime >= _departTimeInMinutes)
        {
            Plane plane = _planePool.GetPlane();
            //plane.SetTrip(, , _data);

            _clock.OnTimeUpdated -= CheckDepartureTime;
        }
    }
}
