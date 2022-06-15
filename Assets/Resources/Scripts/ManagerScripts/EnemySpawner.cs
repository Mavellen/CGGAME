using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Spawn(GameObject Prefab, int amount)
    {
        GameObject[] gOs = new GameObject[amount];
        for(int i = 0; i < amount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-10, 10), 10, Random.Range(-10, 10)) + transform.position;
            if (Physics.Raycast(pos, Vector3.down, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Building"))
                {
                    i--;
                    continue;
                }
                gOs[i] = Instantiate(Prefab, hit.point, hit.transform.rotation);

            }
        }
        return gOs;
    }
}
