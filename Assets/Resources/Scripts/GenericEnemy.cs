
using UnityEngine;
using UnityEngine.AI;
using System;

public class GenericEnemy : MonoBehaviour
{
    public event Action<GenericEnemy> onDeath;

    private NavMeshAgent Agent;

    private float Health = 5f;
    private float DMG = 1f;
    private float Range = 1f;

    private Building building;
    private bool seek = true;

    private void OnEnable()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (building != null)
        {
            Move();
        }
        else
        {
            setEnemy();
        }
    }
    public void setEnemy()
    {
        while (seek && building == null)
        {
            Building[] b = GameObject.FindObjectsOfType<Building>();
            if (b.Length > 0)
            {
                Transform t = b[0].gameObject.transform;
                for (int i = 1; i < b.Length - 1; i++)
                {
                    if ((transform.position - b[i].gameObject.transform.position).sqrMagnitude < (transform.position - t.position).sqrMagnitude)
                    {
                        t = b[i].gameObject.transform;
                    }
                }
                building = t.gameObject.GetComponent<Building>();
            }
        }
    }
    public void stopSearch()
    {
        seek = false;
    }
    private void Attack()
    {
        building.Receive(DMG);
    }

    private void Move()
    {
        Collider[] c = Physics.OverlapSphere(transform.position, Range);
        Building d = null;
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i].gameObject.TryGetComponent<Building>(out Building co))
            {
                d = co;
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
            building = d;
            Agent.destination = transform.position;
            Attack();
        }
    }

    public void Receive(float DMG)
    {
        Debug.Log("Taking Damage!");
        Health -= DMG;
        if (Health <= 0) onKilled();
    }

    private void onKilled()
    {
        onDeath?.Invoke(this);
    }
}


