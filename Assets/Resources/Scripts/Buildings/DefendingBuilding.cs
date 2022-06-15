using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendingBuilding : BuildingBase
{
    private GenericEnemy target;
    public Turret myTurret;

    private float range = 10f;
    private float CD = 1f;
    private float CDL = 0f;

    private void OnEnable()
    {
        baseHealth *= 1.5f;
        multiplier = (1.3f * baseHealth);
    }

    private void FixedUpdate()
    {
        if (Activated)
        {
            if (target != null)
            {
                Attack();
            }
            else
            {
                SetEnemy();
            }
        }
    }

    private void SetEnemy()
    {
        Collider[] c = Physics.OverlapSphere(transform.position, range);
        GenericEnemy d = null;
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i].gameObject.TryGetComponent(out GenericEnemy co))
            {
                d = co;
                break;
            }
        }
        if (d != null)
        {
            target = d;
            Attack();
        }
    }

    private void rotateTurret()
    {
        myTurret.Rotate(target);
    }

    private void Attack()
    {
        rotateTurret();
        CDL -= Time.deltaTime;
        if (CDL <= 0)
        {
            CDL = CD;
            MakeAttack();
        }
    }
    private void MakeAttack()
    {
        myTurret.Shoot(target);
    }
}
