using UnityEngine;

[CreateAssetMenu(fileName = "PassengerData", menuName = "Scriptable Objects/PassengerData")]
public class PassengerData : ScriptableObject
{
    public string Name;
    public string Descriptor;
    public int ID;

    //Maybe best not to do as SO, since it is variable?
    //public Airport Start;
    //public Airport Destination;


}
