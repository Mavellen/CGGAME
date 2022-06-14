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
    }

    private void FixedUpdate()
    {
        Debug.Log(Activated);
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
        //Vector3 dir = target.transform.position - transform.position;
        //Quaternion lR = Quaternion.LookRotation(dir);
        //myTurret.transform.rotation = Quaternion.Euler(0, lR.eulerAngles.y, 0);
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
        //GameObject bulletGO = Instantiate(BulletPrefab, myTurret.position, myTurret.rotation);
        //Bullet bullet = bulletGO.GetComponent<Bullet>();
        //bullet.setRotation(target.transform.position);
    }
}
