using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{
    public BasePlayer AiScript;
    
    private void OnTriggerEnter(Collider other) 
    {
        if (!other.CompareTag("Player"))
        {
            if (!other.CompareTag("Food"))
            {
                return;
            }
            //Add food data to list
            FoodScanned scannedFood = new FoodScanned();
            scannedFood.Distance = Vector3.Distance(transform.root.position, other.transform.position);
            scannedFood.Type = other.gameObject;
            scannedFood.Position = other.transform.position;
            AiScript.DetectedFood.Add(scannedFood);
        }
        //Add enemy data to list
        ScannedEnemy scannedEnemy = new ScannedEnemy();
        scannedEnemy.Distance = Vector3.Distance(transform.root.position, other.transform.position);
        scannedEnemy.Position = other.transform.position;
        scannedEnemy.Object = other.gameObject;
        AiScript.DetectedEnemies.Add(scannedEnemy);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            if (!other.CompareTag("Food"))
            {
                return;
            }
            //Update in-range food data
            foreach (var food in AiScript.DetectedFood.ToList())
            {
                food.Position = food.Type.transform.position;
                food.Distance = Vector3.Distance(transform.root.position, food.Type.transform.position);
            }
        }
        //Update in-range enemy data
        foreach (var enemy in AiScript.DetectedEnemies.ToList())
        {
            enemy.Position = enemy.Object.transform.position;
            enemy.Distance = Vector3.Distance(transform.root.position, enemy.Object.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            if (!other.CompareTag("Food"))
            {
                return;
            }
            //Remove out-of-range food from list
            foreach (var food in AiScript.DetectedFood.ToList())
            {
                if (food.Type == other.gameObject)
                {
                    AiScript.DetectedFood.Remove(food);
                }
            }
        }
        //Remove out-of-range enemies from list
        foreach (var enemy in AiScript.DetectedEnemies.ToList())
        {
            if (enemy.Object == other.gameObject)
            {
                AiScript.DetectedEnemies.Remove(enemy);
            }
        }
    }
}
