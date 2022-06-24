using UnityEngine;

public class GunTurret : Turret
{
    AudioSource a;
    private void OnEnable()
    {
        a = GetComponent<AudioSource>();
    }
    protected override void Shoot()
    {
        GameObject bulletGO = Instantiate(BulletPrefab, transform.position, transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.setRotation(target.transform.position, target.getVelocityVector());
        AudioSource.PlayClipAtPoint(a.clip, transform.position);
    }
}
