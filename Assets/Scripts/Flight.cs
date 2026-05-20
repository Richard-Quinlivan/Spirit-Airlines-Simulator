using TMPro;
using UnityEngine;

public class Flight : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _departureCity;

    [SerializeField]
    private TextMeshProUGUI _arrivalCity;

    [SerializeField]
    private TextMeshProUGUI _departureTime;

    [SerializeField]
    private TextMeshProUGUI _arrivalTime;

    private FlightData _data;

    public void Initialize(FlightData data)
    {
        _data = data;

        _departureCity.text = data.DepartureCity.ToString();
        _arrivalCity.text = data.ArrivalCity.ToString();

        _departureTime.text = data.DepartureTime.Get12HourString();
        _arrivalTime.text = data.ArrivalTime.Get12HourString();
    }
}
