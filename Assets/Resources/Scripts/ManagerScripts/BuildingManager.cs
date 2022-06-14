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
    private int buldingsLeft = 0;

    private void SpawnSet()
    {
        buldingsLeft += numSpawned;
        for (int i = 0; i < numSpawned; i++)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-20, 20), 10, UnityEngine.Random.Range(-20, 20));
            if(Physics.Raycast(pos, Vector3.down, out RaycastHit hit))
            {
                var hitt = hit.point;
                hitt.y += 0.5f;
                GameObject go = GameObject.Instantiate(Prefab[UnityEngine.Random.Range(0, Prefab.Count)], hitt, hit.transform.rotation);
                go.GetComponent<BuildingBase>().destroyedNotice += destructionEvent;
            }

        }
        updateNavMesh?.Invoke();
        buildingDestroyedNotice?.Invoke();
    }

    private void destructionEvent(BuildingBase b)
    {
        b.GetComponent<BuildingBase>().destroyedNotice -= destructionEvent;
        Destroy(b.gameObject);
        buldingsLeft--;
        if(buldingsLeft > 0)
        {
            buildingDestroyedNotice?.Invoke();
        }
        else
        {
            ranOutOfBuildingsNotice?.Invoke();
        }
    }

    private void OnEnable()
    {
        SpawnSet();
    }

}
