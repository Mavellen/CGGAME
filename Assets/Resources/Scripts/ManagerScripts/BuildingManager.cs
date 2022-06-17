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

    [SerializeField] private GameObject[] Prefab;

    private float numSpawnedInnerRing = 2;

    private void OnEnable()
    {
        SpawnSet();
    }
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
                    if (hit.collider.CompareTag("Building") || hit.collider.CompareTag("Player"))
                    {
                        i++;
                        continue;
                    }
                    Vector3 hitPoint = hit.point;
                    hitPoint.y += 0.5f;
                    GameObject go = Instantiate(Prefab[UnityEngine.Random.Range(0, Prefab.Length-1)], hitPoint, hit.transform.rotation);
                    go.GetComponent<BuildingBase>().destroyedNotice += destructionEvent;
                    MainBuilding.notificationExit += go.GetComponent<BuildingBase>().noticeNotifier;
                    go.GetComponent<BuildingBase>().ParticleSystem.Pause();
                }
            }
            firstRing += 3;
            add = 360 / firstRing;
            degrees = 0;
        }
        SpawnTiles();
        updateNavMesh?.Invoke();
    }
    private void SpawnTiles()
    {
        for(int i = 0; i < 10; i++)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-50, 50), 10, UnityEngine.Random.Range(-50, 50));
            if (Physics.Raycast(pos, Vector3.down, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Building") || hit.collider.CompareTag("Player"))
                {
                    i--;
                    continue;
                }
                Vector3 hitPoint = hit.point;
                hitPoint.y += 0.1f;
                GameObject go = Instantiate(Prefab[3], hitPoint, hit.transform.rotation);
                go.transform.SetParent(transform);
                go.GetComponent<Tile>().constructedTile += SpawnFromTile;
            }
        }
    }

    private void SpawnFromTile(BuildingBase building)
    {
        building.destroyedNotice += destructionEvent;
        MainBuilding.notificationExit += building.noticeNotifier;
        building.ParticleSystem.Pause();
    }
    private void destructionEvent(BuildingBase b)
    {
        b.GetComponent<BuildingBase>().destroyedNotice -= destructionEvent;
        MainBuilding.notificationExit -= b.noticeNotifier;
        Destroy(b.gameObject);
        updateNavMesh?.Invoke();
    }
}
