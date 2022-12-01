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
    
    // Start is called before the first frame update
    void Start()
    {
        boid = GetComponent<Boid>();
    }

    // Update is called once per frame
    void Update()
    {
        var boids = FindObjectsOfType<Boid>();
        var averageCohesion = Vector3.zero;
        var averageAlignment = Vector3.zero;
        var averageMagnetism = Vector3.zero;
        var foundCohesion = 0;
        var foundAlignment = 0;
        var foundMagnetism = 0;

        foreach (var boid in boids.Where(b => b != boid))
        {
            var diff = boid.transform.position - this.transform.position;
            
            // Cohesion
            if (diff.magnitude < radiusCohesion)
            {
                averageCohesion += diff;
                foundCohesion += 1;
            }
            
            // Alignment
            if (diff.magnitude < radiusAlignment)
            {
                averageAlignment += boid.velocity;
                foundAlignment += 1;
            }
            
            // Magnetism
            if (diff.magnitude < radiusMagnetism)
            {
                averageMagnetism += diff;
                foundMagnetism += 1;
            }
        }

        if (foundCohesion > 0)
        {
            averageCohesion = averageCohesion / foundCohesion;
            boid.velocity += Vector3.Lerp(Vector3.zero, averageCohesion, averageCohesion.magnitude);
        }

        if (foundAlignment > 0)
        {
            averageAlignment = averageAlignment / foundAlignment;
            boid.velocity += Vector3.Lerp(boid.velocity, averageAlignment, Time.deltaTime);
        }

        if (foundMagnetism > 0)
        {
            averageMagnetism = averageMagnetism / foundMagnetism;
            boid.velocity -=
                Vector3.Lerp(Vector3.zero, averageMagnetism, averageMagnetism.magnitude / radiusMagnetism) *
                repulsionForce;
        }
    }
}
