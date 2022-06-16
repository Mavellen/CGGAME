using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private List<GameObject> Prefab;
    [SerializeField] private List<EnemySpawner> Spawners;
    [SerializeField] private Consumption consumption;

    private int numSpawned = 1;
    private float CD = 5f;
    private float CDleft = 0f;

    private void OnEnable()
    {
        for (int i = 0; i < Spawners.Count; i++)
        {
            Spawners[i].transform.position += new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        }
        SpawnSet();
    }
    private void FixedUpdate()
    {
        CDleft -= Time.deltaTime;
        if (CDleft <= 0)
        {
            CDleft = CD;
            numSpawned = 1;
            numSpawned *= Mathf.FloorToInt(consumption.consumedElectricity / 10);
            numSpawned = Mathf.Clamp(numSpawned, 1, 5);
            SpawnSet();
        }
    }
    private void SpawnSet()
    {
        for (int i = 0; i < Spawners.Count; i++)
        {
            GameObject[] gOs = Spawners[i].Spawn(Prefab[Random.Range(0, Prefab.Count)], numSpawned);
            for (int j = 0; j < gOs.Length; j++)
            {
                gOs[j].GetComponent<EnemyBase>().onDeath += RemoveEnemy;
            }
        }
    }
    private void RemoveEnemy(EnemyBase e)
    {
        Destroy(e.gameObject);
    }

}
