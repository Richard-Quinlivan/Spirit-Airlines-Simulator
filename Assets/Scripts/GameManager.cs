using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LevelData[] Levels;

    private GameClock _clock;
    private TripList _tripList;
    private FlightList _flightList;

    public LevelData CurrentLevel => Levels[_currentLevelIndex];
    private int _currentLevelIndex;


    private List<FlightData> _flights;
    private List<TripData> _trips;

    private void Awake()
    {
        _clock = FindAnyObjectByType<GameClock>();
        _tripList = FindAnyObjectByType<TripList>();
        _flightList = FindAnyObjectByType<FlightList>();

        _clock.OnTimeUpdated += CheckForNewPassengers;
        LoadLevel(0);
    }

    private void OnDestroy()
    {
        if (_clock != null)
        {
            _clock.OnTimeUpdated -= CheckForNewPassengers;
        }
    }

    private void LoadLevel(int index)
    {
        _currentLevelIndex = index;
        _flights = CurrentLevel.Flights.ToList();
        _trips = CurrentLevel.Trips.ToList();

        _flightList.AddFlights(_flights);
    }

    private void CheckForNewPassengers(int time)
    {
        List<TripData> toDelete = new();
        foreach (TripData tripData in _trips)
        {
            if (TimeData.TimeToMinutes(tripData.StartTime) <= time)
            {
                PassengerData passenger = tripData.PassengerData;
                Debug.Log($"{passenger.Name} Needs a flight from {tripData.Start} to {tripData.Destination}");
                _tripList.AddTrip(tripData);
                toDelete.Add(tripData);
            }
        }
        foreach (TripData tripData in toDelete)
        {
            _trips.Remove(tripData);
        }
    }
}
