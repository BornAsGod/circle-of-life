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
        Destroy(this.gameObject);
    }
}
