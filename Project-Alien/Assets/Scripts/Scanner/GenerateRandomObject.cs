using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomObject : MonoBehaviour
{
    [SerializeField] private List<PointOfInterest> objects = new();
    [SerializeField] private Vector3 location;

    public PointOfInterest GetRandomPoint()
    {
        return objects[Random.Range(0, objects.Count)];
    }

    public GameObject SpawnObjectAtLocation(PointOfInterest point)
    {
        return Instantiate(point.objectToRetrieve, location, Quaternion.identity);
    }
}
