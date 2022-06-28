using UnityEngine;

public class GrenadeTurret : Turret
{
    private void OnEnable()
    {
        range *= 0.8f;
        CD *= 3f;
    }
    protected override void Shoot()
    {
        GameObject bulletGO = Instantiate(BulletPrefab, transform.position, transform.rotation);
        HE bullet = bulletGO.GetComponent<HE>();
        bullet.setRotation(target.transform.position, target.getVelocityVector());
    }
}
