using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnerfier : EnemyBase
{
    protected override void OnEnable()
    {
        Health *= 10;
    }

    protected override void setEnemy()
    {
    }

    protected override void Attack()
    {
    }

    protected override void Move()
    {
    }

    protected override void StartEnemyBehaviour()
    {
    }
    protected override void FixedUpdate()
    {
    }

    public override Vector3 getVelocityVector()
    {
        return Vector3.zero;
    }

    public override void setEnemyNotification()
    {
    }
}
