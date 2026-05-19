using System;
using TMPro;
using UnityEngine;

public class GameClock : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _clockText;

    [Tooltip("Amount of Game time in minutes that passes every second." +
        "\ni.e. if set to 3, then 15 game minutes pass every 5 seconds")]
    [SerializeField]
    private int _timeRatio = 3;
    //480 minutes is 8am
    private readonly int _startingTime = 480;

    private int _realTime = 0;
    private int _currentTimeInMinutes;

    public Action<int> OnTimeUpdated;


    private void Awake()
    {
        _clockText.text = "8:00 AM";
        _realTime = (int)Time.time;
    }

    private void Update()
    {
        //only update the clock every 5 seconds
        if (Time.time - _realTime >= 5f)
        {
            _realTime = (int)Time.time;
            _currentTimeInMinutes = (_realTime * _timeRatio) + _startingTime;

            _clockText.text = ConvertRealTimeToGameTime();
            OnTimeUpdated?.Invoke(_currentTimeInMinutes);
        }
    }


    private string ConvertRealTimeToGameTime()
    {
        return TimeData.ConvertMinutesTo12HourString(_currentTimeInMinutes);
    }
}
