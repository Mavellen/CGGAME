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

    private int numSpawned = 30;
    private int buldingsLeft = 0;

    private void Spawn()
    {
        for(int rings = 10; rings < 60; rings+=10)
        {
            for (int degrees = 0; degrees < 340; degrees += 20)
            {
                Vector3 pos = new Vector3(Mathf.Cos(degrees), 10, Mathf.Sin(degrees));

            }
        }
    }

    private void SpawnSet()
    {
        buldingsLeft += numSpawned;
        for (int i = 0; i < numSpawned; i++)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-65, 65), 10, UnityEngine.Random.Range(-65, 65));
            if(Physics.Raycast(pos, Vector3.down, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Building"))
                {
                    i--;
                    continue;
                }
                var hitt = hit.point;
                hitt.y += 0.5f;
                GameObject go = Instantiate(Prefab[UnityEngine.Random.Range(0, Prefab.Count)], hitt, hit.transform.rotation);
                go.GetComponent<BuildingBase>().destroyedNotice += destructionEvent;
                MainBuilding.notificationExit += go.GetComponent<BuildingBase>().notInRange;
                go.GetComponent<BuildingBase>().Activated = false;
                go.GetComponent<BuildingBase>().ParticleSystem.Pause();
            }

        }
        updateNavMesh?.Invoke();
        buildingDestroyedNotice?.Invoke();
    }

    private void destructionEvent(BuildingBase b)
    {
        b.GetComponent<BuildingBase>().destroyedNotice -= destructionEvent;
        MainBuilding.notificationExit -= b.notInRange;
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
