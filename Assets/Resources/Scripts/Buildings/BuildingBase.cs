using UnityEngine;
using System;

public abstract class BuildingBase : MonoBehaviour
{
    public event Action<BuildingBase> destroyedNotice;

    protected float baseHealth = 10f;
    protected float energyConsumption = 5f;
    protected float energyGeneration = 5f;
    protected bool Activated;
    private bool wasNotified;

    public float getConsumption()
    {
        return energyConsumption;
    }
    public float getGeneration()
    {
        return energyGeneration;
    }
    public bool isActivated()
    {
        return Activated;
    }
    public void Receive(float DMG)
    {
        baseHealth -= DMG;
        if (baseHealth <= 0) onDestruction();
    }
    private void onDestruction()
    {
        destroyedNotice?.Invoke(this);
    }

    public void notInRange()
    {
        if (wasNotified)
        {
            wasNotified = false;
        }
        else
        {
            Activated = false;
        }
    }
    public void receivedNotice()
    {
        Activated = true;
        wasNotified = true;
    }

 
}
