using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;
    public static EnemyManager Instance
    {
        get
        {
            if (_instance == null) _instance = new EnemyManager();
            return _instance;
        }
    }

    private event Action locateEnemy;
    private event Action stopSearch;

    [SerializeField] private List<GameObject> Prefab;
    [SerializeField] List<EnemySpawner> Spawners;

    private int numSpawned = 2;
    private float CD = 5f;
    private float CDL = 0f;

    private void FixedUpdate()
    {
        CDL -= Time.deltaTime;
        if (CDL <= 0)
        {
            CDL = CD;
            SpawnSet();
        }
    }

    private void SpawnSet()
    {
        for (int i = 0; i < Spawners.Count; i++)
        {
            GameObject[] gOs = Spawners[i].Spawn(Prefab[UnityEngine.Random.Range(0, Prefab.Count)], numSpawned);
            for (int j = 0; j < gOs.Length; j++)
            {
                locateEnemy += gOs[j].GetComponent<GenericEnemy>().setEnemy;
                stopSearch += gOs[j].GetComponent<GenericEnemy>().stopSearch;
                gOs[j].GetComponent<GenericEnemy>().onDeath += RemoveEnemy;
            }
        }
    }

    private void RemoveEnemy(GenericEnemy e)
    {
        locateEnemy -= e.setEnemy;
        stopSearch -= e.stopSearch;
        e.GetComponent<GenericEnemy>().onDeath -= RemoveEnemy;
        Destroy(e.gameObject);
    }

    private void notifyEnemies()
    {
        locateEnemy.Invoke();
    }
    private void notifyOfEnd()
    {
        stopSearch?.Invoke();
    }

    private void OnEnable()
    {
        BuildingManager.buildingDestroyedNotice += notifyEnemies;
        BuildingManager.ranOutOfBuildingsNotice += notifyOfEnd;
        SpawnSet();
    }
    private void OnDisable()
    {
        BuildingManager.buildingDestroyedNotice -= notifyEnemies;
        BuildingManager.ranOutOfBuildingsNotice -= notifyOfEnd;
    }
}
