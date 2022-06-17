public class EliteEnemy : GenericEnemy
{
    protected override void OnEnable()
    {
        Health *= 2f;
        DMG *= 2f;
        base.OnEnable();
    }
}
