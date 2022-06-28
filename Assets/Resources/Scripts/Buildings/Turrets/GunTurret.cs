using System.Collections;
using UnityEngine;

public class GunTurret : Turret
{
    AudioSource a;
    private bool isCooldown;
    private void OnEnable()
    {
        a = GetComponent<AudioSource>();
    }
    protected override void Shoot()
    {
        GameObject bulletGO = Instantiate(BulletPrefab, transform.position, transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.setRotation(target.transform.position, target.getVelocityVector());
        StartCoroutine(GunshotAudio());
    }

    IEnumerator GunshotAudio(){
        if (isCooldown)
        {
            yield return new WaitForSeconds(3f);
            isCooldown = false;
        }
        else
        {
            AudioSource.PlayClipAtPoint(a.clip, transform.position);
            isCooldown = true;
        }
        
    }
}
