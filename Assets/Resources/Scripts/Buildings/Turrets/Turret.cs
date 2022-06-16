using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    [SerializeField] protected GameObject BulletPrefab;
    protected float range = 10f;
    protected float CD = 0.5f;
    protected float CDleft = 0f;
    protected GenericEnemy target;

    protected void Rotate() 
    {
        Vector3 dir = target.transform.position - transform.position;
        Quaternion lR = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Euler(0, lR.eulerAngles.y, 0);
    }
    protected abstract void Shoot();
    public void StartTurretBehaviour()
    {
        CDleft -= Time.deltaTime;
        if (CDleft <= 0)
        {
            CDleft = CD;
            SetEnemy();
            if(target != null)
            {
                Rotate();
                Shoot();
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
        }
    }
}
