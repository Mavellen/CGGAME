using UnityEngine;
using System;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    public event Action<EnemyBase, int> onDeath;

    protected NavMeshAgent Agent;

    protected float Health = 5f;
    protected float DMG = 1f;
    protected float Range = 4f;
    protected float VisionRange = 3f;
    protected float CD = 2f;
    protected float CDleft = 0f;
    protected int awardedCurrency = 1;

    protected virtual void OnEnable()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = true;
    }
    protected virtual void FixedUpdate()
    {
        StartEnemyBehaviour();
    }
    public void Receive(float DMG)
    {
        Health -= DMG;
        if (Health <= 0) onKilled();
    }
    public virtual Vector3 getVelocityVector()
    {
        return Agent.velocity*Agent.speed;
    }
    private void onKilled()
    {
        onDeath?.Invoke(this,awardedCurrency);
    }
    public virtual void setEnemyNotification()
    {
        setEnemy();
    }
    protected abstract void setEnemy();
    protected abstract void Attack();
    protected abstract void Move();
    protected abstract void StartEnemyBehaviour();

}
