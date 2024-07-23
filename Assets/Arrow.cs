using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 10;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        // rb.velocity += transform.forward * speed;
        rb.AddForce(transform.forward * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
 
}
