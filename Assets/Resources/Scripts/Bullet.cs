using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 40f;
    private float DMG = 2f;
    public Rigidbody rb;
    public void setRotation(Vector3 Destination)
    {
        transform.LookAt(Destination);
        rb.freezeRotation = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("My Collision: " + collision.collider.tag);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<GenericEnemy>().Receive(DMG);
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }
}
