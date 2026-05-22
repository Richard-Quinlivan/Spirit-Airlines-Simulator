using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Plane : MonoBehaviour
{
    private Airport _startingLocation;
    private Airport _endingLocation;
    private float _flightStartTime;
    private int _flightTimeInRealSeconds;
    private Action<Plane> _returnPlane;

    public void SetTrip(Airport startingLocation, Airport endingLocation, TimeData departureTime,
        TimeData arrivalTime, Action<Plane> returnPlane)
    {
        _startingLocation = startingLocation;
        _endingLocation = endingLocation;
        _returnPlane = returnPlane;
        int flightTimeInGame = TimeData.TimeToMinutes(arrivalTime) - TimeData.TimeToMinutes(departureTime);
        _flightTimeInRealSeconds = flightTimeInGame / GameClock.TimeRatio;
        _flightStartTime = Time.time;
        SetRotation();
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float progress = Mathf.Clamp01((Time.time - _flightStartTime) / _flightTimeInRealSeconds);
        transform.position = Vector2.Lerp
        (
            _startingLocation.transform.position,
            _endingLocation.transform.position,
            progress
        );
        if (progress == 1)
        {
            EndFlight();
        }
    }

    private void SetRotation()
    {
        Vector3 line = _startingLocation.transform.position - _endingLocation.transform.position;
        Vector3 axis = Vector3.up;

        float angle = -Vector3.SignedAngle(axis, line, Vector3.back) - 90;

        transform.localEulerAngles = new Vector3(0f, 0f, angle);
        if (MathF.Abs(angle) >= 90)
        {
            transform.Rotate(new Vector3(180f, 0f, 0f));
        }
    }

    private void EndFlight()
    {
        gameObject.SetActive(false);
        _returnPlane.Invoke(this);
    }

    public void HighlightPlane()
    {
        transform.localScale *= 2;
    }

    public void UnHighlightPlane()
    {
        transform.localScale /= 2;
    }
}
