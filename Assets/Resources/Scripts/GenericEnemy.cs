using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections.Generic;
using System.Collections;

public class GenericEnemy : MonoBehaviour
{
    public event Action<GenericEnemy> onDeath;

    private NavMeshAgent Agent;

    private float Health = 5f;
    private float DMG = 1f;
    private float Range = 1f;
    private float CD = 2f;
    private float CDL = 0f;

    private Structure building;
    private bool seek = true;
    private float suspend = 5f;

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
        if (seek)
        {
            if(building == null)
            {
                Structure[] b = GameObject.FindObjectsOfType<Structure>();
                List<Structure> bL = new List<Structure>();
                
                foreach (Structure building in b)
                {
                    if (building.isActivated()) bL.Add(building);
                }

                if (bL.Count > 0)
                {
                    Transform t = bL[0].transform;
                    for (int i = 1; i < bL.Count; i++)
                    {
                        if (bL[i].isActivated())
                        {
                            if ((transform.position - bL[i].gameObject.transform.position).sqrMagnitude < (transform.position - t.position).sqrMagnitude)
                            {
                                t = bL[i].gameObject.transform;
                            }
                        }
                    }
                    building = t.gameObject.GetComponent<Structure>();
                }
                else
                {
                    StartCoroutine(wait(suspend));
                }
            }
        }
    }
    
    private IEnumerator wait(float waitTime)
    {
        float counter = 0;
        while(counter < waitTime)
        {
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    
    public void stopSearch()
    {
        seek = false;
    }
    private void Attack()
    {
        CDL -= Time.deltaTime;
        if (CDL <= 0)
        {
            CDL = CD;
            building.Receive(DMG);
        }
    }

    private void Move()
    {
        Collider[] c = Physics.OverlapSphere(transform.position, Range);
        Structure d = null;
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i].gameObject.TryGetComponent<Structure>(out Structure co))
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


