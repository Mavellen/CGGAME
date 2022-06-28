using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI generationText;
    public TextMeshProUGUI consumptionText;
    public TextMeshProUGUI currencyText;
    public Consumption Consumption;
    public Generation Generation;
    public Currency currency;

    private void FixedUpdate()
    {
        generationText.text = "Generation: " + Generation.generatedElectricity;
        consumptionText.text = "Consumption: " + Consumption.consumedElectricity;
        currencyText.text = "Funds: " + currency.funds;
    }
}
