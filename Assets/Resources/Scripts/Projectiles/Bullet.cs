using UnityEngine;

public class Bullet : Projectile
{
    public override void setRotation(Vector3 Destination, Vector3 velocity)
    {
        Destination += velocity / speed;
        transform.LookAt(Destination);
        rb.freezeRotation = true;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<GenericEnemy>().Receive(DMG);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
