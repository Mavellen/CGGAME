public class DefendingBuilding : BuildingBase
{
    public Turret myTurret;

    private void OnEnable()
    {
        baseHealth *= 1.5f;
        multiplier = (1.3f * baseHealth);
    }

    private void FixedUpdate()
    {
        if(isActivated()) myTurret.StartTurretBehaviour();
    }
}
