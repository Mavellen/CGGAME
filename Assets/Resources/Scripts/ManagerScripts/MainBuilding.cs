using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainBuilding : MonoBehaviour
{
    private static MainBuilding _instance;
    public static MainBuilding Instance
    {
        get
        {
            if (_instance == null) _instance = new MainBuilding();
            return _instance;
        }
    }

    public static event Action notificationExit;

    private float CD = 1f;
    private float CDR = 0f;

    private float baseGeneration = 10f;
    private float Generation = 0f;
    private float Consumption = 0f;

    private void FixedUpdate()
    {
        CDR += Time.fixedDeltaTime;
        if(CDR >= CD)
        {
            plode();
            CDR = 0f;
        }
    }

    private void plode()
    {
        Collider[] c = Physics.OverlapSphere(transform.position, Generation);
        Generation = baseGeneration;
        Consumption = 0f;
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i].gameObject.TryGetComponent(out BuildingBase co))
            {
                Generation += co.getGeneration();
                Consumption += co.getConsumption();
                co.receivedNotice();
            }
        }
        notificationExit?.Invoke();
    }
    
    

}
