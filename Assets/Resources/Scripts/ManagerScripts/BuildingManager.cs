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

    private float numSpawnedInnerRing = 3;
    private int buldingsLeft = 0;

    private void SpawnSet()
    {
        float firstRing = numSpawnedInnerRing;
        float degrees = 0;
        float add = 360 / firstRing;
        for(int rings = 10; rings < 70; rings += 10)
        {
            for(float i = firstRing; i >= 1; i--)
            {
                degrees += add;
                var x = Mathf.Cos(UnityEngine.Random.Range(degrees - 7, degrees + 7)) * UnityEngine.Random.Range(rings - 3, rings + 3);
                var z = Mathf.Sin(UnityEngine.Random.Range(degrees - 7, degrees + 7)) * UnityEngine.Random.Range(rings - 3, rings + 3);
                Vector3 pos = new Vector3(x, 10, z);
                if (Physics.Raycast(pos, Vector3.down, out RaycastHit hit))
                {
                    if (hit.collider.CompareTag("Building"))
                    {
                        continue;
                    }
                    var hitt = hit.point;
                    hitt.y += 0.5f;
                    GameObject go = Instantiate(Prefab[UnityEngine.Random.Range(0, Prefab.Count)], hitt, hit.transform.rotation);
                    go.GetComponent<BuildingBase>().destroyedNotice += destructionEvent;
                    MainBuilding.notificationExit += go.GetComponent<BuildingBase>().notInRange;
                    go.GetComponent<BuildingBase>().Activated = false;
                    go.GetComponent<BuildingBase>().ParticleSystem.Pause();
                    buldingsLeft++;
                }
            }
            firstRing += 3;
            add = 360 / firstRing;
            degrees = 0;
        }
        updateNavMesh?.Invoke();
        buildingDestroyedNotice?.Invoke();

        /*
        for (int rings = 10; rings < 70; rings+=10)
        {
            for (int degrees = 0; degrees < 360; degrees += (360/numSpawnedInnerRing))
            {
                Vector3 pos = new Vector3(Mathf.Cos(UnityEngine.Random.Range(degrees-10,degrees+10)), 0, Mathf.Sin(UnityEngine.Random.Range(degrees - 10, degrees + 10)));
                pos *= UnityEngine.Random.Range(rings - 3, rings + 3);
                pos.y = 10;
                if (Physics.Raycast(pos, Vector3.down, out RaycastHit hit))
                {
                    if (hit.collider.CompareTag("Building"))
                    {
                        degrees-=10;
                        continue;
                    }
                    var hitt = hit.point;
                    hitt.y += 0.5f;
                    GameObject go = Instantiate(Prefab[UnityEngine.Random.Range(0, Prefab.Count)], hitt, hit.transform.rotation);
                    go.GetComponent<BuildingBase>().destroyedNotice += destructionEvent;
                    MainBuilding.notificationExit += go.GetComponent<BuildingBase>().notInRange;
                    go.GetComponent<BuildingBase>().Activated = false;
                    go.GetComponent<BuildingBase>().ParticleSystem.Pause();
                    buldingsLeft++;
                }
            }
        }
        updateNavMesh?.Invoke();
        buildingDestroyedNotice?.Invoke();
        */
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
