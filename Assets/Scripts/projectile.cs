using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    Rigidbody rb;

    public float AliveTime = 3;
    public float Radius = 2;
    public float speed = 100f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("EnableColider", 0.2f);
        Invoke("Destroy", 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Initialize(Vector3 face)
    {
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
}
