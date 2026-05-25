using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Trip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private Image _profilePicture;

    [SerializeField]
    private TextMeshProUGUI _passengerName;

    [SerializeField]
    private TextMeshProUGUI _currentCity;

    [SerializeField]
    private TextMeshProUGUI _destinationCity;

    [SerializeField]
    private TextMeshProUGUI _timeWasted;

    private TimeData _timeWastedData;
    private TripData _tripData;
    private GameClock _clock;

    private AirportName _currentAirport;
    private AirportName _destinationAirport;

    public AirportName CurrentAirport => _currentAirport;
    public PassengerData Passenger => _tripData.PassengerData;

    private bool _isSelected = false;

    public static Action<Trip> OnTripSelected;

    public void Initialize(TripData data, GameClock clock)
    {
        _tripData = data;
        _clock = clock;
        //_profilePicture = ;
        _passengerName.text = data.PassengerData.Name;
        _currentCity.text = data.StartCity.ToString();
        _destinationCity.text = data.DestinationCity.ToString();
        _timeWasted.text = "00:00";
        _timeWastedData = new TimeData(0, 0);

        _currentAirport = data.StartCity;
        _destinationAirport = data.DestinationCity;

        _clock.OnTimeUpdated += UpdateTimeWasted;
    }

    private void OnDestroy()
    {
        if (_clock != null)
        {
            _clock.OnTimeUpdated -= UpdateTimeWasted;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Airport.OnHighlightAirport?.Invoke(_currentAirport);
        Airport.OnHighlightAirport?.Invoke(_destinationAirport);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //do not unhighlight if you are choosing a plane to book for this trip
        if (_isSelected)
        {
            return;
        }
        Airport.OnUnHighlightAirport?.Invoke(_currentAirport);
        Airport.OnUnHighlightAirport?.Invoke(_destinationAirport);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _isSelected = true;

            OnTripSelected?.Invoke(this);
        }
    }

    private void UpdateTimeWasted(int _)
    {
        _timeWastedData.AddTime(0, 15);
        _timeWasted.text = $"{_timeWastedData.GetTimeInHours()} Hours";
    }

    public void PlaneAssigned()
    {
        _isSelected = false;
        OnPointerExit(null);
    }
}
