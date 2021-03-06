using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GenericEnemy : EnemyBase
{
    private Structure building;
    private AudioSource s;
    private Animator _anim;

    protected override void OnEnable()
    {
        base.OnEnable();
        _anim = GetComponent<Animator>();
        s = GetComponent<AudioSource>();
        StartCoroutine(MovementAudio());
    }

    IEnumerator MovementAudio(){
        while(gameObject){
            yield return new WaitForSeconds(5f);
            s.Play();
        }
        yield return null;
    }

    protected override void StartEnemyBehaviour()
    {
        if (building == null) setEnemy();
        trySetClosest();
        if (Vector3.Distance(transform.position, building.transform.position) > Range) Move();
        else
        {
            Agent.destination = transform.position;
            CDleft -= Time.deltaTime;
            if (CDleft <= 0)
            {
                Attack();
            }
        }
    }
    protected override void setEnemy()
    {
        Structure[] b = FindObjectsOfType<Structure>();
        List<Structure> bL = new List<Structure>();
                
        foreach (Structure building in b)
        {
            if (building.isActivated()) bL.Add(building);
        }

        Structure s = null;
        Vector3 t = Vector3.positiveInfinity;
        foreach (Structure building in bL)
        {
            if ((transform.position - building.transform.position).sqrMagnitude < (transform.position - t).sqrMagnitude)
            {
                t = building.transform.position;
                s = building;
            }
        }
        building = s;
    }
    private void trySetClosest()
    {
        Collider[] c = Physics.OverlapSphere(transform.position, VisionRange);
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i].gameObject.TryGetComponent(out Structure co))
            {
                if (co.isActivated())
                {
                    building = co;
                }
                break;
            }
        }
    }
    protected override void Move()
    {
        Vector3 dir = building.transform.position - transform.position;
        Quaternion lR = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Euler(0, lR.eulerAngles.y, 0);
        Agent.destination = building.transform.position;
        _anim.SetBool("Attack",false);
    }
    protected override void Attack()
    {
        building.Receive(DMG);
        CDleft = CD;
        _anim.SetBool("Attack", true);
    }

}


