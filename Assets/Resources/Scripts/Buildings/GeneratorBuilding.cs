using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBuilding : BuildingBase
{
    private void OnEnable()
    {
        baseHealth *= 1f;
        multiplier = (2f * baseHealth);
        energyGeneration *= 2.5f;
    }
    private void FixedUpdate()
    {
        ParticleSystem.Pause();
        var main = ParticleSystem.main;
        main.startColor = Color.red;
        if (!Activated)
        {
            main.startColor = Color.black;
        }
        ParticleSystem.Play();
    }
}
