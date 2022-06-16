using UnityEngine;
using System;

public abstract class BuildingBase : Structure
{
    public event Action<BuildingBase> destroyedNotice;

    public ParticleSystem ParticleSystem;
    public Generation generation;
    public Consumption consumption;

    protected float energyConsumption = 7f;
    protected float energyGeneration = 4f;

    private bool wasNotified;
    public bool isInRange = false;
    public bool isConnectable = false;

    protected override void onDestruction()
    {
        if (Activated)
        {
            generation.generatedElectricity -= energyGeneration;
            consumption.consumedElectricity -= energyConsumption;
        }
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
            isConnectable = false;
            if (Activated)
            {
                generation.generatedElectricity -= energyGeneration;
                consumption.consumedElectricity -= energyConsumption;
            }
            Activated = false;
        }
    }
    public void receivedNotice()
    {
        isConnectable = true;
        wasNotified = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    private void Update()
    {
        if (!Activated && isConnectable)
        {
            ParticleSystem.Pause();
            var main = ParticleSystem.main;
            main.startColor = Color.red;
            ParticleSystem.Play();
        }
        if (isInRange && isConnectable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Activated)
                {
                    Activated = false;
                    generation.generatedElectricity -= energyGeneration;
                    consumption.consumedElectricity -= energyConsumption;
                }
                else
                {
                    if((generation.generatedElectricity + energyGeneration) > (consumption.consumedElectricity + energyConsumption))
                    {
                        Activated = true;
                        ParticleSystem.Pause();
                        var main = ParticleSystem.main;
                        main.startColor = Color.yellow;
                        ParticleSystem.Play();
                        generation.generatedElectricity += energyGeneration;
                        consumption.consumedElectricity += energyConsumption;
                    }
                }
            }
        }
    }
}
