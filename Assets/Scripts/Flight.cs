using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Flight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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

    [SerializeField]
    private Color _defaultColor;

    [SerializeField]
    private Color _highlightedColor;

    private Image _background;

    private FlightData _flightData;
    private GameClock _clock;
    private PlanePool _planePool;
    private Plane _plane;
    private bool _isHovered = false;
    private bool _isActive = false;

    private int _departTimeInMinutes;
    private Airport _startingAirport;
    private Airport _endingAirport;
    private TimeData _departureTimeData;
    private TimeData _arivalTimeData;

    private PassengerData _potentialPassenger = null;

    public Airport EndingAirport => _endingAirport;

    private Action _unHighlightAll;

    private void Awake()
    {
        _background = GetComponent<Image>();
    }

    public void Initialize(FlightData data, GameClock clock, PlanePool planePool, Map map)
    {
        _flightData = data;
        _clock = clock;
        _planePool = planePool;

        _departTimeInMinutes = TimeData.TimeToMinutes(data.DepartureTime);
        _startingAirport = map.GetAirport(data.DepartureCity);
        _endingAirport = map.GetAirport(data.ArrivalCity);
        _departureTimeData = data.DepartureTime;
        _arivalTimeData = data.ArrivalTime;

        _departureCity.text = data.DepartureCity.ToString();
        _arrivalCity.text = data.ArrivalCity.ToString();

        _departureTime.text = data.DepartureTime.Get12HourString();
        _arrivalTime.text = data.ArrivalTime.Get12HourString();
        UpdateAvailableSeats();
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
        Airport.OnHighlightAirport?.Invoke(_startingAirport.AirportName);
        Airport.OnHighlightAirport?.Invoke(_endingAirport.AirportName);
        if (_plane)
        {
            _plane.HighlightPlane();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovered = false;
        Airport.OnUnHighlightAirport?.Invoke(_startingAirport.AirportName);
        Airport.OnUnHighlightAirport?.Invoke(_endingAirport.AirportName);
        if (_plane)
        {
            _plane.UnHighlightPlane();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isActive)
        {
            AssignPassenger();

            _unHighlightAll?.Invoke();
        }
    }

    private void UpdateAvailableSeats()
    {
        int availableSeats = 0;
        foreach (int  seat in _flightData.Seats)
        {
            if (seat == -1)
            {
                availableSeats++;
            }
        }

        _availableSeats.text = $"Available Seats: {availableSeats}";
    }

    public bool IsAvailable(AirportName airportName)
    {
        if (_startingAirport.AirportName == airportName)
        {
            foreach (int seat in _flightData.Seats)
            {
                if (seat == -1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void AssignPassenger()
    {
        for(int i = 0; i < _flightData.Seats.Length; i++)
        {
            if (_flightData.Seats[i] == -1)
            {
                _flightData.Seats[i] = _potentialPassenger.ID;
                break;
            }
        }
        UpdateAvailableSeats();
    }

    public void Activate(PassengerData passenger, Action unHighlightAll)
    {
        _unHighlightAll = unHighlightAll;
        _isActive = true;
        _background.color = _highlightedColor;
        _potentialPassenger = passenger;
    }


    public void Deactivate()
    {
        _background.color = _defaultColor;
        
        _isActive = false;
    }

    private void CheckDepartureTime(int currTime)
    {
        if (currTime >= _departTimeInMinutes)
        {
            _plane = _planePool.GetPlane();
            _plane.SetTrip(_startingAirport, _endingAirport, _departureTimeData, _arivalTimeData, _planePool.ReturnPlane);
            if (_isHovered)
            {
                _plane.HighlightPlane();
            }
            _clock.OnTimeUpdated -= CheckDepartureTime;
        }
    }
}
