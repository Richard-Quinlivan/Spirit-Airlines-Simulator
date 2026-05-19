using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Passenger : MonoBehaviour
{
    [SerializeField]
    private Image _profilePicture;

    [SerializeField]
    private TextMeshProUGUI _passengerName;

    [SerializeField]
    private TextMeshProUGUI _timeWasted;

    private PassengerData _data;
    private TimeData _timeWastedData;
    private GameClock _clock;

    public void Initialize(PassengerData data, GameClock clock)
    {
        _data = data;
        _clock = clock;
        //_profilePicture = ;
        _passengerName.text = data.name;
        _timeWasted.text = "00:00";
        _timeWastedData = new TimeData(0, 0);


        _clock.OnTimeUpdated += UpdateTimeWasted;
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
