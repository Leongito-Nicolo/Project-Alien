using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomObject : MonoBehaviour
{
    [SerializeField] private List<PointOfInterest> objects = new();

    public PointOfInterest GetRandomPoint()
    {
        return objects[Random.Range(0, objects.Count)];
    }

    public void SpawnObjectAtLocation(PointOfInterest point, Vector3 location)
    {
        Instantiate(point.objectToRetrieve, location, Quaternion.identity);
    }
}
