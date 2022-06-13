
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;

public class GenericEnemy : MonoBehaviour
{
    public event Action<GenericEnemy> onDeath;

    private NavMeshAgent Agent;
    private event Action buildingInRange;

    private float health = 5f;
    private float DMG = 1f;

    private Building building;
    private bool seek = true;

    private void OnEnable()
    {
        buildingInRange += Attack;
        Agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (building != null)
        {
            StartCoroutine(Move());
        }
        else
        {
            StartCoroutine(setEnemy());
        }
    }
    private IEnumerator setEnemy()
    {
        while (seek && building == null)
        {
            Building[] b = GameObject.FindObjectsOfType<Building>();
            if (b.Length > 0)
            {
                Transform t = b[0].gameObject.transform;
                for (int i = 1; i < b.Length - 1; i++)
                {
                    if ((transform.position - b[i].gameObject.transform.position).sqrMagnitude < (transform.position - b[0].gameObject.transform.position).sqrMagnitude)
                    {
                        t = b[i].gameObject.transform;
                    }
                }
                building = t.gameObject.GetComponent<Building>();
            }
            yield return new WaitForEndOfFrame();
        }
    }
    public void SetupProcedure()
    {
        StartCoroutine(setEnemy());
    }
    public void stopSearch()
    {
        seek = false;
    }

    private IEnumerator MakeAttack()
    {
        yield return new WaitForEndOfFrame();
        building.Receive(DMG);
        yield return new WaitForEndOfFrame();
    }
    private void Attack()
    {
        StartCoroutine(MakeAttack());
    }

    private IEnumerator Move()
    {
        Collider[] c = Physics.OverlapSphere(transform.position, 1);
        GameObject d = null;
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i].gameObject.TryGetComponent<Building>(out Building co))
            {
                d = c[i].gameObject;
                //building = c[0].gameObject.GetComponent<Building>();
                break;
            }
        }

        if (d == null)
        {
            Vector3 dir = building.transform.position - transform.position;
            Quaternion lR = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Euler(0, lR.eulerAngles.y, 0);
            Agent.destination = building.transform.position;
        }
        else
        {
            building = d.gameObject.GetComponent<Building>();
            Agent.destination = transform.position;
            buildingInRange?.Invoke();
        }
        yield return null;
    }

    public void Receive(float DMG)
    {
        Debug.Log("Taking Damage!");
        health -= DMG;
        if (health <= 0) onKilled();
    }

    private void onKilled()
    {
        buildingInRange -= Attack;
        onDeath?.Invoke(this);
    }
}


