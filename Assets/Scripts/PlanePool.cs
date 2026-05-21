using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanePool : MonoBehaviour
{
    [SerializeField]
    private GameObject _planePrefab;

    [SerializeField]
    private List<Plane> _allPlanes;

    private List<Plane> _availablePlanes = new();
    private List<Plane> _inUsePlanes = new();

    private void Awake()
    {
        _availablePlanes = _allPlanes.ToList();
        _inUsePlanes.Clear();
    }

    public Plane GetPlane()
    {
        if (_availablePlanes.Count <= 0)
        {
            GameObject planeObj = GameObject.Instantiate(_planePrefab, transform);
            Plane newPlane = planeObj.GetComponent<Plane>();
            _allPlanes.Add(newPlane);
            _availablePlanes.Add(newPlane);
        }

        Plane plane = _availablePlanes[0];
        _availablePlanes.RemoveAt(0);
        _inUsePlanes.Add(plane);
        return plane;
    }


    public void ReturnPlane(Plane plane)
    {
        _inUsePlanes.Remove(plane);
        _availablePlanes.Add(plane);
    }
}
