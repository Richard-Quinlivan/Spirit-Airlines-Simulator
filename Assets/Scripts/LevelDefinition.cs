using UnityEngine;
using System.Collections.Generic;

public class LevelDefinition : MonoBehaviour
{
    [SerializeField]
    private List<PassengerInfo> _passengerInfo = new();

    [SerializeField]
    private List<PassengerFlightData> _flightData = new();


    private void Awake()
    {
        
    }
}
