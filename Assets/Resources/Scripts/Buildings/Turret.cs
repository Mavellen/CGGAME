using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    public GameObject BulletPrefab;
    public void Rotate(GenericEnemy target)
    {
        Vector3 dir = target.transform.position - transform.position;
        Quaternion lR = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Euler(0, lR.eulerAngles.y, 0);
    }
    public virtual void Shoot(GenericEnemy target)
    {
        GameObject bulletGO = Instantiate(BulletPrefab, transform.position, transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.setRotation(target.transform.position);
    }
    
}
