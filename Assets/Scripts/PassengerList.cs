using UnityEngine;

public class PassengerList : MonoBehaviour
{
    [SerializeField]
    private GameObject _passengerPrefab;

    [SerializeField]
    private Transform _content;

    private GameClock _clock;

    private void Awake()
    {
        _clock = FindAnyObjectByType<GameClock>();
    }

    public void AddPassenger(PassengerData data)
    {
        GameObject passengerObj = GameObject.Instantiate(_passengerPrefab, _content);
        Passenger passenger = passengerObj.GetComponent<Passenger>();
        passenger.Initialize(data, _clock);
    }
}
