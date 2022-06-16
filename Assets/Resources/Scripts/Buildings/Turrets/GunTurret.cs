using UnityEngine;

public class GunTurret : Turret
{
    protected override void Shoot()
    {
        GameObject bulletGO = Instantiate(BulletPrefab, transform.position, transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.setRotation(target.transform.position);
    }
}
