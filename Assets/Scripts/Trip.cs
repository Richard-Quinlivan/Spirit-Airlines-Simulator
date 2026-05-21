using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Trip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    private TripData _data;
    private TimeData _timeWastedData;
    private GameClock _clock;

    public void Initialize(TripData data, GameClock clock)
    {
        _data = data;
        _clock = clock;
        //_profilePicture = ;
        _passengerName.text = data.PassengerData.Name;
        _currentCity.text = data.Start.ToString();
        _destinationCity.text = data.Destination.ToString();
        _timeWasted.text = "00:00";
        _timeWastedData = new TimeData(0, 0);


        _clock.OnTimeUpdated += UpdateTimeWasted;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Airport.OnHighlightAirport?.Invoke(_data.Start);
        Airport.OnHighlightAirport?.Invoke(_data.Destination);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Airport.OnUnHighlightAirport?.Invoke(_data.Start);
        Airport.OnUnHighlightAirport?.Invoke(_data.Destination);
    }

    private void OnDestroy()
    {
        if (_clock != null)
        {
            _clock.OnTimeUpdated -= UpdateTimeWasted;
        }
    }

    private void UpdateTimeWasted(int _)
    {
        _timeWastedData.AddTime(0, 15);
        _timeWasted.text = $"{_timeWastedData.GetTimeInHours()} Hours";
    }
}
