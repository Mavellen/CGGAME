using System.Collections;
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

    private int numSpawned = 5;

    private void SpawnSet()
    {
        for (int i = 0; i < numSpawned; i++)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-20, 20), 10, UnityEngine.Random.Range(-20, 20));
            if (Physics.Raycast(pos, Vector3.down, out RaycastHit hit))
            {
                GameObject go = GameObject.Instantiate(Prefab[UnityEngine.Random.Range(0, Prefab.Count)], hit.point, hit.transform.rotation);
                locateEnemy += go.GetComponent<GenericEnemy>().SetupProcedure;
                stopSearch += go.GetComponent<GenericEnemy>().stopSearch;
                go.GetComponent<GenericEnemy>().onDeath += RemoveEnemy;
            }
        }
    }

    private void RemoveEnemy(GenericEnemy e)
    {
        locateEnemy -= e.SetupProcedure;
        stopSearch -= e.stopSearch;
        e.GetComponent<GenericEnemy>().onDeath -= RemoveEnemy;
        Destroy(e.gameObject);
    }

    private void notifyEnemies()
    {
        Delay();
    }
    private void notifyOfEnd()
    {
        stopSearch?.Invoke();
    }

    private IEnumerator Delay()
    {
        yield return new WaitForEndOfFrame();
        locateEnemy.Invoke();
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
