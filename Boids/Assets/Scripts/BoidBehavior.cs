using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public class BoidBehavior : MonoBehaviour
{
    private Boid boid;

    public float radiusCohesion;
    public float radiusAlignment;
    public float radiusMagnetism;
    public float repulsionForce;

    private Boid[] boids;
    
    // Start is called before the first frame update
    void Start()
    {
        boid = GetComponent<Boid>();
        boids = FindObjectsOfType<Boid>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 averageCohesion = Vector3.zero;
        Vector3 averageAlignment = Vector3.zero;
        Vector3 averageMagnetism = Vector3.zero;
        int foundCohesion = 0;
        int foundAlignment = 0;
        int foundMagnetism = 0;
        
        // Rules detection
        foreach (var boid in boids.Where(b => b != boid))
        {
            var diff = boid.transform.position - this.transform.position;
            
            // Cohesion detection
            if (diff.magnitude < radiusCohesion)
            {
                averageCohesion += diff;
                foundCohesion += 1;
            }
            
            // Alignment detection
            if (diff.magnitude < radiusAlignment)
            {
                averageAlignment += boid.velocity;
                foundAlignment += 1;
            }
            
            // Magnetism detection
            if (diff.magnitude < radiusMagnetism)
            {
                averageMagnetism += diff;
                foundMagnetism += 1;
            }
        }
        
        // Cohesion interaction compute
        if (foundCohesion > 0)
        {
            averageCohesion = averageCohesion / foundCohesion;
            boid.velocity += Vector3.Lerp(Vector3.zero, averageCohesion, averageCohesion.magnitude);
        }
        
        // Alignment interaction compute
        if (foundAlignment > 0)
        {
            averageAlignment = averageAlignment / foundAlignment;
            boid.velocity += Vector3.Lerp(boid.velocity, averageAlignment, Time.deltaTime);
        }
        
        // Magnetism interaction compute
        if (foundMagnetism > 0)
        {
            averageMagnetism = averageMagnetism / foundMagnetism;
            boid.velocity -=
                Vector3.Lerp(Vector3.zero, averageMagnetism, averageMagnetism.magnitude / radiusMagnetism) *
                repulsionForce;
        }
    }
}
