using UnityEngine;
using System;

public abstract class BuildingBase : MonoBehaviour
{
    public event Action<BuildingBase> destroyedNotice;

    protected float baseHealth = 10f;

    public void Receive(float DMG)
    {
        baseHealth -= DMG;
        if (baseHealth <= 0) onDestruction();
    }
    private void onDestruction()
    {
        destroyedNotice?.Invoke(this);
    }
}
