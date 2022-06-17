using UnityEngine;
using System;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    public event Action<EnemyBase> onDeath;

    protected NavMeshAgent Agent;

    protected float Health = 5f;
    protected float DMG = 1f;
    protected float Range = 2f;
    protected float VisionRange = 3f;
    protected float CD = 2f;
    protected float CDleft = 0f;

    protected virtual void OnEnable()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = true;
    }
    private void FixedUpdate()
    {
        StartEnemyBehaviour();
    }
    public void Receive(float DMG)
    {
        Health -= DMG;
        if (Health <= 0) onKilled();
    }
    public Vector3 getVelocityVector()
    {
        return Agent.velocity*Agent.speed;
    }
    private void onKilled()
    {
        onDeath?.Invoke(this);
    }
    public void setEnemyNotification()
    {
        setEnemy();
    }
    protected abstract void setEnemy();
    protected abstract void Attack();
    protected abstract void Move();
    protected abstract void StartEnemyBehaviour();

}
