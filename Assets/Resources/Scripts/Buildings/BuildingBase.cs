using UnityEngine;
using System;

public abstract class BuildingBase : Structure
{
    public event Action<BuildingBase> destroyedNotice;

    public ParticleSystem ParticleSystem;

    protected float energyConsumption = 7f;
    protected float energyGeneration = 4f;

    private bool wasNotified;

    public float getConsumption()
    {
        return energyConsumption;
    }
    public float getGeneration()
    {
        return energyGeneration;
    }

    protected override void onDestruction()
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
