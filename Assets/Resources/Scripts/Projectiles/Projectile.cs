using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected Rigidbody rb;
    protected float speed = 30f;
    protected float DMG = 3f;
    public abstract void setRotation(Vector3 Destination, Vector3 velocity);

    protected void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }
    protected abstract void OnCollisionEnter(Collision collision);
 
}
