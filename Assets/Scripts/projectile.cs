using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    Rigidbody rb;

    public float AliveTime = 3;
    public float Radius = 2;
    public float speed = 100f;
    private float damage;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("EnableColider", 0.2f);
        Invoke("Destroy", 2f);
    }
    
    public void Initialize(Vector3 face, float _damage)
    {
        damage = _damage;
        rb.AddForce(face * speed, ForceMode.Impulse);
    }
    void EnableColider()
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
        
        other.gameObject.GetComponent<AIController>()._Ai.TakeDamage(damage);
    }
}
