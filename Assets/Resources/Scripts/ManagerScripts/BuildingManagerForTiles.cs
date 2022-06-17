using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class BuildingManagerForTiles : MonoBehaviour
{
    public BuildingBase selectedBuilding;

    private List<Button> buttons;

    private void OnEnable()
    {
        buttons = new List<Button>();
        Canvas c = GameObject.Find("TowerPicker").GetComponent<Canvas>();
        for (int k = 0; k < c.transform.childCount; k++)
        {
            buttons.Add(c.transform.GetChild(k).GetComponent<Button>());
        }
    }
    public void selectTower(BuildingBase building)
    { 
        selectedBuilding = building;
        setButtonColors(building.name);
    }
    private void setButtonColors(string buildingName)
    {
        foreach (Button button in buttons)
        {
            if (button.name == buildingName)
            {
                button.image.color = Color.red;
            }
            else
            {
                button.image.color = Color.grey;
            }
        }
    }
}
