using TMPro;
using UnityEngine;

public class GameClock : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _clockText;

    [Tooltip("Amount of Game time in minutes that passes every second." +
        "\ni.e. if set to 3, then 15 game minutes pass every 5 seconds")]
    [SerializeField]
    private readonly float _timeRatio = 3f;
    //480 minutes is 8am
    private readonly int _startingTime = 480;

    private float _realTime = 0;


    private void Awake()
    {
        _clockText.text = "8:00 AM";
        _realTime = Time.time;
    }

    private void Update()
    {
        //only update the clock every 5 seconds
        if (Time.time - _realTime > 5f)
        {
            _realTime = Time.time;

            _clockText.text = ConvertRealTimeToGameTime();
        }
    }


    private string ConvertRealTimeToGameTime()
    {
        float numberOfMinutes = _realTime * _timeRatio;
        return TimeData.ConvertMinutesTo12HourString((int)numberOfMinutes + _startingTime);
    }
}
