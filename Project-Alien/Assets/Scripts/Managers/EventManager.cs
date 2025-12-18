using System;
using UnityEngine;

public static class EventManager
{
    public static event Action<GenerateRandomObject> OnSetTarget;
    public static event Action<int> OnEndCleaning;
    public static event Action OnDestroyObject;

    public static void SetTarget(GenerateRandomObject point)
    {
        OnSetTarget?.Invoke(point);
    }

    public static void EndCleaning(int moneyEarned)
    {
        OnEndCleaning?.Invoke(moneyEarned);
    }

    public static void DestroyObject()
    {
        OnDestroyObject?.Invoke();
    }
}