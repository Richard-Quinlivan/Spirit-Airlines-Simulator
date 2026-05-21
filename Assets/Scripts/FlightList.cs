using System.Collections.Generic;
using UnityEngine;

public class FlightList : MonoBehaviour
{
    [SerializeField]
    private GameObject _flightPrefab;

    [SerializeField]
    private Transform _content;

    private GameClock _clock;
    private PlanePool _planePool;

    private void Awake()
    {
        _clock = FindAnyObjectByType<GameClock>();
        _planePool = FindAnyObjectByType<PlanePool>();
    }

    public void AddFlights(List<FlightData> dataList)
    {
        foreach (FlightData data in dataList)
        {
            GameObject flightObj = GameObject.Instantiate(_flightPrefab, _content);
            Flight flight = flightObj.GetComponent<Flight>();
            flight.Initialize(data, _clock, _planePool);
        }
    }
}
