using System;
using UnityEngine;

public class Airport : MonoBehaviour
{
    [SerializeField]
    private AirportName _airportName;

    public static Action<AirportName> OnHighlightAirport;
    public static Action<AirportName> OnUnHighlightAirport;

    private void Awake()
    {
        OnHighlightAirport += HighlightAirport;
        OnUnHighlightAirport += UnHighlightAirport;
    }

    private void OnDestroy()
    {
        OnHighlightAirport -= HighlightAirport;
        OnUnHighlightAirport -= UnHighlightAirport;        
    }

    public void HighlightAirport(AirportName name)
    {
        if (_airportName == name)
        {
            transform.localScale = Vector3.one * 2;
        }
    }

    public void UnHighlightAirport(AirportName name)
    {
        if (_airportName == name)
        {
            transform.localScale = Vector3.one;
        }
    }
}
