using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using UnityEngine;

public class FlightList : MonoBehaviour
{
    [SerializeField]
    private GameObject _flightPrefab;

    [SerializeField]
    private Transform _content;

    [SerializeField]
    private List<Flight> _flights;

    private GameClock _clock;
    private PlanePool _planePool;
    private Map _map;

    private void Awake()
    {
        _clock = FindAnyObjectByType<GameClock>();
        _planePool = FindAnyObjectByType<PlanePool>();
        _map = FindAnyObjectByType<Map>();
    }

    public void AddFlights(List<FlightData> dataList)
    {
        SortFlightDataByDepartureTime flightDataComparer = new();
        dataList.Sort(flightDataComparer);
        foreach (FlightData data in dataList)
        {
            GameObject flightObj = GameObject.Instantiate(_flightPrefab, _content);
            Flight flight = flightObj.GetComponent<Flight>();
            flight.Initialize(data, _clock, _planePool, _map);
            _flights.Add(flight);
        }
    }

    public void HighlightAvailableFlights(AirportName airport)
    {
        UnHighlightAll();
        foreach (Flight flight in _flights)
        {
            if (flight.StartingAirport.AirportName == airport)
            {
                flight.Highlight(UnHighlightAll);
            }
        }
    }

    public void UnHighlightAll()
    {
        foreach (Flight flight in _flights)
        {
            flight.UnHighlight();
        }
    }
}
