using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    private List<Airport> _airports;

    public Airport GetAirport(AirportName airportName)
    {
        foreach (Airport airport in _airports)
        {
            if (airport.AirportName == airportName)
            {
                return airport;
            }
        }
        throw new System.Exception($"Airport {airportName} not found in dataset");
    }
}
