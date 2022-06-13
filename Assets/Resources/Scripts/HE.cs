using System.Collections.Generic;
using UnityEngine;

public class HE : MonoBehaviour
{
    private float speed = 15f;
    private float DMG = 5f;
    private float Radius = 5f;
    public void setRotation(Vector3 Destination)
    {
        transform.LookAt(Destination);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Ground"))
        {
            Collider[] c = Physics.OverlapSphere(transform.position, Radius);
            List<GenericEnemy> hitTargets = new List<GenericEnemy>();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i].gameObject.TryGetComponent<GenericEnemy>(out GenericEnemy co))
                {
                    hitTargets.Add(co);
                }
            }
            for (int i = 0; i < hitTargets.Count; i++)
            {
                hitTargets[i].Receive(DMG);
            }
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        transform.Translate(transform.forward * (speed * Time.deltaTime), Space.World);
    }
}
