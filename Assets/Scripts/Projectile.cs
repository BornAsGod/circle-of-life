using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;

    public float AliveTime = 3f;
    public float colider = 0.2f;
    public float speed = 100f;
    private float damage;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Invoke(nameof(EnableCollider), colider);
        Invoke(nameof(DestoryAfterTime), AliveTime);
    }
    
    public void Initialize(Vector3 face, float _damage)
    {
        damage = _damage;
        rb.AddForce(face * speed, ForceMode.Impulse);
    }

    public void DestoryAfterTime()
    {
        Destroy(this.gameObject);
    }

    void EnableCollider()
    {
        GetComponent<Collider>().enabled = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        other.gameObject.GetComponent<AIController>().Health -= damage;
        Destroy(this.gameObject);
    }
}
