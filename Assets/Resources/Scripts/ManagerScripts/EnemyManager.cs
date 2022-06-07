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

    [SerializeField] private List<GameObject> Prefab;

    private void SpawnSet()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-20, 20), 0.5f, UnityEngine.Random.Range(-20, 20));
            GameObject go = GameObject.Instantiate(Prefab[UnityEngine.Random.Range(0, Prefab.Count)], pos, Quaternion.identity);
            locateEnemy += go.GetComponent<GenericEnemy>().SetupProcedure;
            go.GetComponent<GenericEnemy>().onDeath += RemoveEnemy;
        }
    }

    private void RemoveEnemy(GenericEnemy e)
    {
        locateEnemy -= e.SetupProcedure;
        e.GetComponent<GenericEnemy>().onDeath -= RemoveEnemy;
        Destroy(e.gameObject);
    }

    private void notifyEnemies()
    {
        Delay();
    }

    private IEnumerator Delay()
    {
        yield return new WaitForEndOfFrame();
        locateEnemy.Invoke();
    }

    private void OnEnable()
    {
        SpawnSet();
        BuildingManager.buildingDestroyedNotice += notifyEnemies;
    }
    private void OnDisable()
    {
        BuildingManager.buildingDestroyedNotice -= notifyEnemies;
    }
}
