using UnityEngine;
using System;

public abstract class BuildingBase : Structure
{
    public event Action<BuildingBase> destroyedNotice;

    //public ParticleSystem ParticleSystem;
    public Generation generation;
    public Consumption consumption;

    protected float energyConsumption = 7f;
    protected float energyGeneration = 4f;

    public bool wasNotified;
    public bool isConnectable;

    protected override void onDestruction()
    {
        if (isActivated())
        {
            removeImpact();
        }
        destroyedNotice?.Invoke(this);
    }

    public void noticeNotifier()
    {
        if (wasNotified)
        {
            wasNotified = false;
        }
        else
        {
            _light.gameObject.SetActive(false);
            isConnectable = false;
            if (isActivated())
            {
                removeImpact();
            }
            setActivated(false);
        }
    }

    public void receivedNotice()
    {
        isConnectable = true;
        wasNotified = true;
        if (!isActivated() && isConnectable)
        {
            colorChange(Color.blue);
        }
    }

    private void OnMouseDown()
    {
        if (isConnectable)
        {
            if (isActivated())
            {
                setActivated(false);
                removeImpact();
            }
            else
            {
                if ((generation.generatedElectricity + energyGeneration) > (consumption.consumedElectricity + energyConsumption))
                {
                    setActivated(true);
                    colorChange(Color.red);
                    generation.generatedElectricity += energyGeneration;
                    consumption.consumedElectricity += energyConsumption;
                }
            }
        }
    }

    private void colorChange(Color color)
    {
        _light.color = color;
        _light.gameObject.SetActive(true);
    }

    private void removeImpact()
    {
        generation.generatedElectricity -= energyGeneration;
        consumption.consumedElectricity -= energyConsumption;
    }
}
