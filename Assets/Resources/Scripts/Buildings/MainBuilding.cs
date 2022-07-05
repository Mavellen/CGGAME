using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

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

    public List<GunTurret> Turrets;
    [SerializeField] private Generation generation;
    [SerializeField] private Consumption consumption;

    private float baseGeneration = 10f;
    private float CDPlode = 5f;
    private float CDplodeleft = 0f;


    private void OnEnable()
    {
        setActivated(true);
        baseHealth *= 5f;
        multiplier = (0.4f * baseHealth);
        consumption.consumedElectricity = 0f;
        generation.generatedElectricity = baseGeneration;
    }


    private void FixedUpdate()
    {
        CDplodeleft += Time.fixedDeltaTime;
        if(CDplodeleft >= CDPlode)
        {
            plode();
            CDplodeleft = 0f;
        }
        foreach(GunTurret t in Turrets)
        {
            t.StartTurretBehaviour();
        }
    }
    private void plode()
    {
        Collider[] c = Physics.OverlapSphere(transform.position, generation.generatedElectricity);
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i].gameObject.TryGetComponent(out BuildingBase co))
            {
                co.receivedNotice();
            }
        }
        notificationExit?.Invoke();
    }

    protected override void onDestruction()
    {
        Debug.Log("Game Over");
        consumption.consumedElectricity = 0f;
        generation.generatedElectricity = baseGeneration;
        LevelGenerator.removeMesh();
        SceneManager.LoadScene("MainMenu");
    }
}
