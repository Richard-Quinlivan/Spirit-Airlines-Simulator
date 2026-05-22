using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Flight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private TextMeshProUGUI _departureCity;

    [SerializeField]
    private TextMeshProUGUI _arrivalCity;

    [SerializeField]
    private TextMeshProUGUI _departureTime;

    [SerializeField]
    private TextMeshProUGUI _arrivalTime;

    [SerializeField]
    private TextMeshProUGUI _availableSeats;

    private FlightData _data;
    private GameClock _clock;
    private PlanePool _planePool;
    private Plane _plane;
    private bool _isHovered = false;

    private int _departTimeInMinutes;
    private Airport _startingAirport;
    private Airport _endingAirport;

    public void Initialize(FlightData data, GameClock clock, PlanePool planePool, Map map)
    {
        _data = data;
        _clock = clock;
        _planePool = planePool;

        _departTimeInMinutes = TimeData.TimeToMinutes(data.DepartureTime);
        _startingAirport = map.GetAirport(data.DepartureCity);
        _endingAirport = map.GetAirport(data.ArrivalCity);

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovered = true;
        Airport.OnHighlightAirport?.Invoke(_data.DepartureCity);
        Airport.OnHighlightAirport?.Invoke(_data.ArrivalCity);
        if (_plane)
        {
            _plane.HighlightPlane();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovered = false;
        Airport.OnUnHighlightAirport?.Invoke(_data.DepartureCity);
        Airport.OnUnHighlightAirport?.Invoke(_data.ArrivalCity);
        if (_plane)
        {
            _plane.UnHighlightPlane();
        }
    }

    private void CheckDepartureTime(int currTime)
    {
        if (currTime >= _departTimeInMinutes)
        {
            _plane = _planePool.GetPlane();
            _plane.SetTrip(_startingAirport, _endingAirport, _data, _planePool.ReturnPlane);
            if (_isHovered)
            {
                _plane.HighlightPlane();
            }
            _clock.OnTimeUpdated -= CheckDepartureTime;
        }
    }
}
