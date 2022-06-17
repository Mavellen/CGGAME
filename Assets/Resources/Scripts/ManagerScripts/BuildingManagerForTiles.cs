using UnityEngine;

public class BuildingManagerForTiles : MonoBehaviour
{
    public BuildingBase selectedBuilding;
    public void selectTower(BuildingBase building)
    {
        selectedBuilding = building;
    }
}
