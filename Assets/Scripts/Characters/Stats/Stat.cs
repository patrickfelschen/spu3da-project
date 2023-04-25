using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    public float baseValue;
    public float currentValue;

    public float GetValue()
    {
        return currentValue;
    }

    public float GetBaseValue()
    {
        return baseValue;
    }
    public void SetValue(float value)
    {
        this.currentValue = value;
    }

    public void IncreaseValue(float value)
    {
        this.currentValue += value;
    }

    public void decreaseValue(float amount)
    {
        this.currentValue -= amount;
    }

    public void RestValue()
    {
        this.currentValue = baseValue;
    }
}
