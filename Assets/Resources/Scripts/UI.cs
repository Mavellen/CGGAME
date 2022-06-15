using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI generationText;
    public TextMeshProUGUI consumptionText;
    public Consumption Consumption;
    public Generation Generation;

    private void FixedUpdate()
    {
        generationText.text = "Generation: " + Generation.generatedElectricity;
        consumptionText.text = "Consumption: " + Consumption.consumedElectricity;
    }
}
