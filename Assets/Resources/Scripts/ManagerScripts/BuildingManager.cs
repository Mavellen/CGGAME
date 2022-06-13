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
    public static event Action ranOutOfBuildingsNotice;

    [SerializeField] private List<GameObject> Prefab;

    private int numSpawned = 1;
    private int left = 0;

    private void SpawnSet()
    {
        left += numSpawned;
        for (int i = 0; i < numSpawned; i++)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-20, 20), 10, UnityEngine.Random.Range(-20, 20));
            if(Physics.Raycast(pos, Vector3.down, out RaycastHit hit))
            {
                var hitt = hit.point;
                hitt.y += 0.5f;
                GameObject go = GameObject.Instantiate(Prefab[UnityEngine.Random.Range(0, Prefab.Count)], hitt, hit.transform.rotation);
                go.AddComponent<Building>().destroyedNotice += destroyedRam;
            }
            //GameObject go = GameObject.Instantiate(Prefab[UnityEngine.Random.Range(0, Prefab.Count)], pos, Quaternion.identity);
            //go.AddComponent<Building>().destroyedNotice += destroyedRam;

        }
        updateNavMesh?.Invoke();
        buildingDestroyedNotice?.Invoke();
    }

    private void destroyedRam(Building b)
    {
        b.GetComponent<Building>().destroyedNotice -= destroyedRam;
        Destroy(b.gameObject);
        left--;
        if(left > 0)
        {
            buildingDestroyedNotice?.Invoke();
        }
        else
        {
            ranOutOfBuildingsNotice?.Invoke();
        }
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
