using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected Rigidbody rb;
    protected float speed = 40f;
    protected float DMG = 2f;
    public void setRotation(Vector3 Destination)
    {
        transform.LookAt(Destination);
        rb.freezeRotation = true;
    }
    protected void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }
    protected abstract void OnCollisionEnter(Collision collision);
 
}
