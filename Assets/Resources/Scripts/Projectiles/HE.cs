using UnityEngine;

public class HE : Projectile
{
    private float Radius = 3f;

    public override void setRotation(Vector3 Destination, Vector3 velocity)
    {
        
        transform.LookAt(Destination);
        rb.freezeRotation = true;
    }
    private void Start()
    {
        DMG *= 2.5f;
        speed *= 0.5f;
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Ground"))
        {
            Collider[] c = Physics.OverlapSphere(transform.position, Radius);
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i].gameObject.TryGetComponent(out EnemyBase co))
                {
                    co.Receive(DMG);
                    AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, collision.transform.position);
                }
            }
            Destroy(gameObject);
        }
    }
}
