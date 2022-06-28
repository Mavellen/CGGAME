using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnedComponents
    {
        public EnemyBase Prefab;
        //public int amountSpawned;
    }

    // You could have multiple types of enemies to spawn.
    // Since we're getting amount spawned from EnemyManager, this doesn't really make sense here as we can't control how many "stronger" enemies we would like to spawn
    // but this is just here to remind you of what's possible
    [SerializeField] protected SpawnedComponents[] components;

    public EnemyBase[] Spawn(int amount)
    {
        EnemyBase[] gOs = new EnemyBase[amount];
        for (int k = 0; k < components.Length; k++)
        {
            for (int i = 0; i < amount; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-10, 10), 10, Random.Range(-10, 10)) + transform.position;
                if (Physics.Raycast(pos, Vector3.down, out RaycastHit hit))
                {
                    if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Building"))
                    {
                        i--;
                        continue;
                    }
                    gOs[i] = Instantiate(components[k].Prefab, hit.point, hit.transform.rotation);
                }
            }
        }
        return gOs;
    }
}
