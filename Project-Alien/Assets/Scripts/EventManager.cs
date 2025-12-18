using System;
using UnityEngine;

public static class EventManager
{
    public static event Action<GenerateRandomObject> OnSetTarget;

    public static void SetTarget(GenerateRandomObject point)
    {
        OnSetTarget?.Invoke(point);
    }
}