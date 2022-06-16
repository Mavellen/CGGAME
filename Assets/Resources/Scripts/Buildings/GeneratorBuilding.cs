public class GeneratorBuilding : BuildingBase
{
    private void OnEnable()
    {
        baseHealth *= 1f;
        multiplier = (2f * baseHealth);
        energyGeneration *= 2.5f;
    }
}
