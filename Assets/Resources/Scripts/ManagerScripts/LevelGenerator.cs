using Unity.AI.Navigation;
using UnityEngine;
using System;

public class LevelGenerator : MonoBehaviour
{
    private NavMeshSurface surface;

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
}
