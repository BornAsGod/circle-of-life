using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFood : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Collector"))
        {
            return;
        }

        Debug.Log("Food collision detected!");
        AIController player = other.gameObject.GetComponentInParent<AIController>();
        player.OnFoodCollected(this.gameObject);
        Destroy(this.gameObject);
    }
}
