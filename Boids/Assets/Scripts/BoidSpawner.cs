using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public GameObject prefab; // Boid prefab
    public float radius; // Spawn radius limit
    public int number; // Number of boids to instantiate
    
    void Start()
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(prefab, this.transform.position + Random.insideUnitSphere * radius, Random.rotation);
        }
    }
}
