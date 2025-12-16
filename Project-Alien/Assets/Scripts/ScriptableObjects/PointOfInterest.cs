using UnityEngine;

[CreateAssetMenu(fileName = "PointOfInterest", menuName = "Scriptable Objects/PointOfInterest")]
public class PointOfInterest : ScriptableObject
{
    public GameObject objectToRetrieve;
    public int targetTemperature;
    public int targetPressure;
}
