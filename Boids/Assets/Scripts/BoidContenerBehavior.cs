using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public class BoidContenerBehavior : MonoBehaviour
{
    private Boid _boid;

    public float radius;

    public float boundaryForce;
    // Start is called before the first frame update
    void Start()
    {
        _boid = GetComponent<Boid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_boid.transform.position.magnitude > radius)
        {
            _boid.velocity += transform.position.normalized * ((radius - _boid.transform.position.magnitude) * boundaryForce * Time.deltaTime);
        }
        
    }
}