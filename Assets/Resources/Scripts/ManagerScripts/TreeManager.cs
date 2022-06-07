using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    private static TreeManager _instance;
    public static TreeManager Instance
    {
        get
        {
            if (_instance == null) _instance = new TreeManager();
            return _instance;
        }
    }

    public static event Action updateNavMesh;

    [SerializeField] private List<GameObject> Prefab;

    private void SpawnSet()
    {
        for (int i = 0; i < 20; i++)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-20, 20), 0, UnityEngine.Random.Range(-20, 20));
            GameObject go = GameObject.Instantiate(Prefab[UnityEngine.Random.Range(0, Prefab.Count)], pos, Quaternion.identity);
        }
        updateNavMesh?.Invoke();
    }

    private void OnEnable()
    {
        SpawnSet();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            SpawnSet();
        }
    }
}
