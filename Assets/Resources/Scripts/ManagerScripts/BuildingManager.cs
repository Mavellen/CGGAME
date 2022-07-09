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
        GetComponent<DropshipManager>().onDropShip += SpawnFromTile;
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
                    Collider[] c = Physics.OverlapSphere(hit.point, 3.5f);
                    bool canSpawn = true;
                    for (int k = 0; k < c.Length; k++)
                    {
                        if (c[k].gameObject.TryGetComponent(out Structure co))
                        {
                            canSpawn = false;
                            break;
                        }
                    }
                    if (!canSpawn)
                    {
                        i++;
                        continue;
                    }
                    Quaternion n = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    Vector3 hitPoint = hit.point;
                    hitPoint.y += 0.5f;
                    GameObject go = Instantiate(Prefab[UnityEngine.Random.Range(0, Prefab.Length)], hitPoint, n);
                    go.GetComponent<BuildingBase>().destroyedNotice += destructionEvent;
                    MainBuilding.notificationExit += go.GetComponent<BuildingBase>().noticeNotifier;
                    MainBuilding.notificationGameExit += go.GetComponent<BuildingBase>().OnGameEnd;
                    go.GetComponent<BuildingBase>().gameEndNotice += gameEndEvent;
                }
            }
            firstRing += 3;
            add = 360 / firstRing;
            degrees = 0;
        }
        updateNavMesh?.Invoke();
    }

    private void SpawnFromTile(BuildingBase building)
    {
        building.destroyedNotice += destructionEvent;
        MainBuilding.notificationExit += building.noticeNotifier;
    }
    private void destructionEvent(BuildingBase b)
    {
        b.GetComponent<BuildingBase>().destroyedNotice -= destructionEvent;
        MainBuilding.notificationExit -= b.noticeNotifier;
        Destroy(b.gameObject);
    }

    private void gameEndEvent(BuildingBase b)
    {
        if (b != null)
        {
            b.GetComponent<BuildingBase>().destroyedNotice -= destructionEvent;
            MainBuilding.notificationExit -= b.noticeNotifier;
            Destroy(b.gameObject);
        }
    }
}
