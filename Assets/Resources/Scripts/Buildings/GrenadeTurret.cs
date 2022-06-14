using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeTurret : Turret
{
    public override void Shoot(GenericEnemy target)
    {
        GameObject bulletGO = Instantiate(BulletPrefab, transform.position, transform.rotation);
        HE bullet = bulletGO.GetComponent<HE>();
        bullet.setRotation(target.transform.position);
    }
}
