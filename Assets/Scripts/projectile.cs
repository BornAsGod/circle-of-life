using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    Rigidbody rb;

    public float AliveTime = 3f;
    public float Radius = 2f;
    public float speed = 100f;
    private float damage;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("EnableCollider", 0.2f);
        Invoke("Destroy", AliveTime);
    }
    
    public void Initialize(Vector3 face, float _damage)
    {
        damage = _damage;
        rb.AddForce(face * speed, ForceMode.Impulse);
    }
    void EnableCollider()
    {
        GetComponent<Collider>().enabled = true;
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        
        other.gameObject.GetComponent<AIController>().TakeDamage(damage);
        Destroy(this);
    }
}
