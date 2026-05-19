using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LevelData[] Levels;

    [SerializeField]
    private PassengerData[] _passengers;

    private GameClock _gameClock;

    public LevelData CurrentLevel => Levels[_currentLevelIndex];
    private int _currentLevelIndex;


    private FlightData[] _flights;
    private TripData[] _trips;

    private void Awake()
    {
        _gameClock = FindAnyObjectByType<GameClock>();

        _gameClock.OnTimeUpdated += CheckForNewPassengers;
        LoadLevel(0);
    }

    private void OnDestroy()
    {
        _gameClock.OnTimeUpdated -= CheckForNewPassengers;        
    }

    private void LoadLevel(int index)
    {
        _currentLevelIndex = index;
        //needs to be deep copy
        _flights = CurrentLevel.Flights.ToArray();
        _trips = CurrentLevel.Trips.ToArray();
    }

    private void CheckForNewPassengers(int time)
    {
        foreach (TripData tripData in _trips)
        {
            if (TimeData.TimeToMinutes(tripData.ArriveAtTime) <= time)
            {
                PassengerData passenger = _passengers[tripData.PassengerData.Index];
                Debug.Log($"{passenger.Name} Needs a flight from {tripData.Start} to {tripData.Destination}");
            }
        }
    }
}
