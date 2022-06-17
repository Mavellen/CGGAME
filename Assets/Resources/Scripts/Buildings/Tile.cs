using UnityEngine;
using System;

public class Tile : MonoBehaviour
{
    public event Action<BuildingBase> constructedTile;
    private void OnMouseDown()
    {
        if (GetComponentInParent<BuildingManagerForTiles>().selectedBuilding != null)
        {
            // If(money == sufficient) ... do the thing
            Vector3 pos = transform.position;
            pos.y += 0.5f;
            constructedTile?.Invoke(Instantiate(GetComponentInParent<BuildingManagerForTiles>().selectedBuilding, pos, transform.rotation));
            Destroy(gameObject);
        }
    }
}
