
using UnityEngine;


public class temp : MonoBehaviour
{
    public Generation electricityGeneration;
    private void OnAwake()
    {
        electricityGeneration = FindObjectOfType<Generation>();
    }

    private void Update()
    {
        for (int x = 0; x <= 340; x+=20)
        {
            Debug.DrawRay(transform.position, (new Vector3(Mathf.Cos(x), 0, Mathf.Sin(x)))*electricityGeneration.generatedElectricity, Color.blue);
        }
    }

}
