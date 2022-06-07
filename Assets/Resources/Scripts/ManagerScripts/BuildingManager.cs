using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingManager : MonoBehaviour
{
    private static BuildingManager _instance;
    public static BuildingManager Instance
    {
        get
        {
            if (_instance == null) _instance = new BuildingManager();
            return _instance;
        }
    }

    public static event Action updateNavMesh;
    public static event Action buildingDestroyedNotice;

    [SerializeField] private List<GameObject> Prefab;

    private void SpawnSet()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-20, 20), 0.5f, UnityEngine.Random.Range(-20, 20));
            GameObject go = GameObject.Instantiate(Prefab[UnityEngine.Random.Range(0, Prefab.Count)], pos, Quaternion.identity);
            go.AddComponent<Building>().destroyedNotice += destroyedRam;

        }
        updateNavMesh?.Invoke();
        buildingDestroyedNotice?.Invoke();
    }

    private void destroyedRam(Building b)
    {
        b.GetComponent<Building>().destroyedNotice -= destroyedRam;
        Destroy(b.gameObject);
        buildingDestroyedNotice?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SpawnSet();
        }
    }

    private void OnEnable()
    {
        SpawnSet();
    }

}
