using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

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

    public static event Action updateNavMesh;

    [SerializeField] private Spawnerfier spawnerPrefab;
    private List<EnemySpawner> Spawners;
    [SerializeField] private Consumption consumption;
    [SerializeField] private Currency currency;

    private int numSpawned = 1;
    private float CD = 5f;
    private float CDleft = 0f;

    private void OnEnable()
    {
        Spawners = new List<EnemySpawner>();
        SpawnSpawners();
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

    private void SpawnSpawners()
    {
        Vector3[] positions = new Vector3[]
        {
            new Vector3(-60, 20, -50),
            new Vector3(-60, 20, 100),
            new Vector3(90, 20, -50),
            new Vector3(90, 20, 100),
        };
        for (int x = 0; x < positions.Length; x++)
        {
            positions[x] += new Vector3(Random.Range(-20, 0), 0, Random.Range(-20, 0));
            if (Physics.Raycast(positions[x], Vector3.down, out RaycastHit hit))
            {
                Quaternion n = Quaternion.FromToRotation(Vector3.up, hit.normal);
                Vector3 hitPoint = hit.point;
                Spawnerfier s = Instantiate(spawnerPrefab, hitPoint, n);
                s.onDeath += RemoveEnemy;
                Spawners.Add(s.gameObject.GetComponent<EnemySpawner>());
            }
        }
        updateNavMesh?.Invoke();
    }
    private void SpawnSet()
    {
        for (int i = 0; i < Spawners.Count; i++)
        {
            EnemyBase[] gOs = Spawners[i].Spawn(numSpawned);
            for (int j = 0; j < gOs.Length; j++)
            {
                gOs[j].onDeath += RemoveEnemy;
            }
        }
    }
    private void RemoveEnemy(EnemyBase e, int funds)
    {
        e.onDeath -= RemoveEnemy;
        currency.funds += funds;
        if (e.TryGetComponent(out EnemySpawner es))
        {
            Spawners.Remove(es);
            if (Spawners.Count == 0)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        Destroy(e.gameObject);
    }

}
