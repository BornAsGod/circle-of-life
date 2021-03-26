using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFood : MonoBehaviour
{
    public int FoodID;
    private foodSpawner spawner;

    private void Start()
    {
        spawner = FindObjectOfType<foodSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Collector"))
        {
            return;
        }
        
        AIController player = other.gameObject.GetComponentInParent<AIController>();
        player.OnFoodCollected(this.gameObject, FoodID);
        spawner.FoodSpawned--;
        transform.position = transform.position + new Vector3(0f, 1000f, 0f); 
        Destroy(this.gameObject, 3f);
    }
}
