using UnityEngine;

[CreateAssetMenu(fileName = "PointOfInterest", menuName = "Scriptable Objects/PointOfInterest")]
public class PointOfInterest : ScriptableObject
{
    public GameObject objectToRetrieve;
    public Material cleanTexture;
    public int targetTemperature;
    public int targetPressure;

    public void CleanObject(GameObject obj)
    {
        obj.GetComponent<MeshRenderer>().material = cleanTexture;
    }
}
