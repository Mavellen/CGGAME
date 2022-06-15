using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainBuilding : Structure
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

    private GenericEnemy target;
    public List<GunTurret> Turrets;

    private float CDPlode = 1f;
    private float CDR = 0f;

    private float range = 10f;
    private float CDAttacks = 0.5f;
    private float CDL = 0f;

    private float baseGeneration = 10f;
    public Generation electricityGeneration;
    private float Consumption = 0f;
    private void onAwake()
    {
        electricityGeneration = FindObjectOfType<Generation>();
    }
    private void OnEnable()
    {
        baseHealth *= 5f;
        multiplier = (0.4f * baseHealth);
    }


    private void FixedUpdate()
    {
        CDR += Time.fixedDeltaTime;
        if(CDR >= CDPlode)
        {
            plode();
            CDR = 0f;
        }
        if (target != null)
        {
            Attack();
        }
        else
        {
            SetEnemy();
        }
    }

    private void SetEnemy()
    {
        Collider[] c = Physics.OverlapSphere(transform.position, range);
        GenericEnemy d = null;
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i].gameObject.TryGetComponent(out GenericEnemy co))
            {
                d = co;
                break;
            }
        }
        if (d != null)
        {
            target = d;
            Attack();
        }
    }

    private void rotateTurret()
    {
        foreach(GunTurret t in Turrets)
        {
            t.Rotate(target);
        }
    }

    private void Attack()
    {
        rotateTurret();
        CDL -= Time.deltaTime;
        if (CDL <= 0)
        {
            CDL = CDAttacks;
            MakeAttack();
        }
    }
    private void MakeAttack()
    {
        foreach (GunTurret t in Turrets)
        {
            t.Shoot(target);
        }
    }

    private void plode()
    {
        Collider[] c = Physics.OverlapSphere(transform.position, electricityGeneration.generatedElectricity);
        electricityGeneration.generatedElectricity = baseGeneration;
        Consumption = 0f;
        List<BuildingBase> buildings = new List<BuildingBase>();
        for (int i = 0; i < c.Length; i++)
        {

            if (c[i].gameObject.TryGetComponent(out BuildingBase co))
            {
                electricityGeneration.generatedElectricity += co.getGeneration();
                Consumption += co.getConsumption();
                buildings.Add(co);
            }
        }
        while(Consumption > electricityGeneration.generatedElectricity)
        {
            BuildingBase b = buildings[UnityEngine.Random.Range(0, buildings.Count)];
            electricityGeneration.generatedElectricity -= b.getGeneration();
            Consumption -= b.getConsumption();
            buildings.Remove(b);
        }
        foreach (BuildingBase b in buildings) { b.receivedNotice(); }

        notificationExit?.Invoke();
    }
    
    

}
