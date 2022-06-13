using UnityEngine;
using System;
using System.Collections;

public class Building : MonoBehaviour
{
    public event Action<Building> destroyedNotice;

    private event Action enemyInRange;
    private GameObject target;
    private Transform myTurret;
    public GameObject BulletPrefab;

    private float health = 10f;
    private float range = 10f;
    private float CD = 1.5f;
    private float CDL = 0f;

    private void OnEnable()
    {
        myTurret = transform.Find("Turret");
        enemyInRange += Attack;
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
            enemyInRange?.Invoke();
        }
        else
        {
            SetEnemy();
        }
    }

    private void SetEnemy()
    {
        Collider[] c = Physics.OverlapSphere(transform.position, range);
        GameObject d = null;
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i].gameObject.TryGetComponent<GenericEnemy>(out GenericEnemy co))
            {
                d = c[i].gameObject;
                break;
            }
        }
        if (d != null)
        {
            target = d;
            enemyInRange?.Invoke();
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
        GameObject bulletGO = (GameObject)Instantiate(BulletPrefab, this.myTurret.position, this.myTurret.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.setRotation(target.transform.position);
    }

    public void onDestruction()
    {
        enemyInRange -= Attack;
        destroyedNotice?.Invoke(this);
    }
}
