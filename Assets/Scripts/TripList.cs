using UnityEngine;

public class TripList : MonoBehaviour
{
    [SerializeField]
    private GameObject _tripPrefab;

    [SerializeField]
    private Transform _content;

    private GameClock _clock;

    private void Awake()
    {
        _clock = FindAnyObjectByType<GameClock>();
    }

    public void AddTrip(TripData data)
    {
        GameObject tripObj = GameObject.Instantiate(_tripPrefab, _content);
        Trip trip = tripObj.GetComponent<Trip>();
        trip.Initialize(data, _clock);
    }
}
