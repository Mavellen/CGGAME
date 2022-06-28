using System;
using UnityEngine;

public class DropshipManager : MonoBehaviour
{
    public Action<BuildingBase> onDropShip;

    [SerializeField] BuildingManagerForTiles bmft;
    [SerializeField] private Currency funds;
    private Camera cam;
    void OnEnable()
    {
        cam = Camera.main;
        funds.funds = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Collider[] c = Physics.OverlapSphere(hit.point, 3f);
                bool canSpawn = true;
                for (int i = 0; i < c.Length; i++)
                {
                    if (c[i].gameObject.TryGetComponent(out Structure co))
                    {
                        Debug.Log("Another building is too close!");
                        canSpawn = false;
                        break;
                    }
                }
                if (funds.funds < 40) canSpawn = false;
                if (canSpawn)
                {
                    Quaternion n = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    Vector3 hitPoint = hit.point;
                    hitPoint.y += 0.5f;
                    BuildingBase b = Instantiate(bmft.selectedBuilding, hitPoint, n);
                    funds.funds -= 20;
                    onDropShip?.Invoke(b);
                }
            }

        }
    }
}
