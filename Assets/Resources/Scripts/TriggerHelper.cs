using UnityEngine;
using System;

public class TriggerHelper : MonoBehaviour
{
    public event Action<Structure> getStructure;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("got hit");
        if (other.TryGetComponent(out Structure co))
        {
            if (co.isActivated())
            {
                getStructure?.Invoke(co);
            }
        }
    }
    public void setPosition(Vector3 pos)
    {
        Debug.Log(pos);
        transform.position = pos;
    }
}
