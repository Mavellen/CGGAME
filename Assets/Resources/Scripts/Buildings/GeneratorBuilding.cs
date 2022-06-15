using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBuilding : BuildingBase
{
    private void OnEnable()
    {
        baseHealth *= 1f;
        multiplier = (2f * baseHealth);
        energyGeneration += 5f;
    }
    private void FixedUpdate()
    {
        Powered();
    }
    private void Powered()
    {
        if (Activated) ParticleSystem.Play();
        else ParticleSystem.Pause();
    }
}
