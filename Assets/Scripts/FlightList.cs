using System.Collections.Generic;
using UnityEngine;

public class FlightList : MonoBehaviour
{
    [SerializeField]
    private GameObject _flightPrefab;

    [SerializeField]
    private Transform _content;

    public void AddFlights(List<FlightData> dataList)
    {
        foreach (FlightData data in dataList)
        {
            GameObject flightObj = GameObject.Instantiate(_flightPrefab, _content);
            Flight flight = flightObj.GetComponent<Flight>();
            flight.Initialize(data);
        }
    }
}
