using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 20f;
    private float DMG = 1f;
    public void setRotation(Vector3 Destination)
    {
        transform.LookAt(Destination);
    }
    private void OnCollisionEnter(Collision collision)
    {
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
        transform.Translate(transform.forward * (speed * Time.deltaTime), Space.World);
    }
}
