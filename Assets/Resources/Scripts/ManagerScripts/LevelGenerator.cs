using Unity.AI.Navigation;
using UnityEngine;
using System;

public class LevelGenerator : MonoBehaviour
{
    private static NavMeshSurface surface;

    private void Awake()
    {
        surface = GetComponent<NavMeshSurface>();
        BuildingManager.updateNavMesh += Build;
        TreeManager.updateNavMesh += Build;
        Build();
    }

    private void Build()
    {
        surface.BuildNavMesh();  
    }

    public static void removeMesh()
    {
        Destroy(surface);
    }
}
