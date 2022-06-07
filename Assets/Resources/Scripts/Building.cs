using UnityEngine;
using System;
using System.Collections;

public class Building : MonoBehaviour
{
    public event Action<Building> destroyedNotice;

    private event Action enemyInRange;
    private GenericEnemy target;

    private float DMG = 1f;
    private float health = 10f;

    private void OnEnable()
    {
        enemyInRange += Attack;
    }

    public void Receive(float DMG)
    {
        Debug.Log("Structure under attack!");
        health -= DMG;
        if (health <= 0) onDestruction();
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            StartCoroutine(MakeAttack());
        }
        else
        {
            StartCoroutine(SetEnemy());
        }
    }

    private IEnumerator SetEnemy()
    {
        Collider[] c = Physics.OverlapSphere(transform.position, 3);
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
            target = d.gameObject.GetComponent<GenericEnemy>();
            enemyInRange?.Invoke();
        }
        yield return null;
    }

    public void Attack()
    {
        StartCoroutine(MakeAttack());
    }
    private IEnumerator MakeAttack()
    {
        yield return new WaitForEndOfFrame();
        try
        {
            target.Receive(DMG);
        }
        catch (NullReferenceException e) { }
        yield return new WaitForEndOfFrame();
    }

    public void onDestruction()
    {
        enemyInRange -= Attack;
        destroyedNotice?.Invoke(this);
    }
}
