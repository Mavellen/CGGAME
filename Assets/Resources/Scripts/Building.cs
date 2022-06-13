using UnityEngine;
using System;

public class Building : MonoBehaviour
{
    public event Action<Building> destroyedNotice;

    private GenericEnemy target;
    private Transform myTurret;
    public GameObject BulletPrefab;

    private float health = 10f;
    private float range = 10f;
    private float CD = 1.5f;
    private float CDL = 0f;

    private void OnEnable()
    {
        myTurret = transform.Find("Turret");
    }

    public void Receive(float DMG)
    {
        health -= DMG;
        if (health <= 0) onDestruction();
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            Attack();
        }
        else
        {
            SetEnemy();
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
        Vector3 dir = target.transform.position - transform.position;
        Quaternion lR = Quaternion.LookRotation(dir);
        myTurret.rotation = Quaternion.Euler(0, lR.eulerAngles.y, 0);
    }

    public void Attack()
    {
        rotateTurret();
        CDL -= Time.deltaTime;
        if(CDL <= 0)
        {
            CDL = CD;
            MakeAttack();
        }
    }
    private void MakeAttack()
    {
        GameObject bulletGO = Instantiate(BulletPrefab, myTurret.position, myTurret.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.setRotation(target.transform.position);
    }

    public void onDestruction()
    {
        destroyedNotice?.Invoke(this);
    }
}
