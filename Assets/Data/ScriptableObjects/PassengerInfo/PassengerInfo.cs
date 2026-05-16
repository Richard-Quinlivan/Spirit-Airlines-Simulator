using UnityEngine;

[CreateAssetMenu(fileName = "PassengerData", menuName = "Scriptable Objects/PassengerData")]
public class PassengerInfo : ScriptableObject
{
    public string Name;
    public string Descriptor;
    public int ID;
}
