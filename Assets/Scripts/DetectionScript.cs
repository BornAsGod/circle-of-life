﻿using System;
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
            Debug.Log("Food Detected!");
            FoodScanned scannedFood = new FoodScanned();
            scannedFood.Distance = Vector3.Distance(transform.root.position, other.transform.position);
            scannedFood.Type = other.gameObject;
            scannedFood.Position = other.transform.position;
            AiScript.DetectedFood.Add(scannedFood);
            AiScript.ScannedFoodEvent(scannedFood); //Call scanned food event
            return;
        }
        //Add enemy data to list
        Debug.Log("Enemy Detected!");
        ScannedEnemy scannedEnemy = new ScannedEnemy();
        scannedEnemy.Distance = Vector3.Distance(transform.root.position, other.transform.position);
        scannedEnemy.Position = other.transform.position;
        scannedEnemy.Object = other.gameObject;
        AiScript.DetectedEnemies.Add(scannedEnemy);
        AiScript.ScannedEnemyEvent(scannedEnemy); //Call scanned enemy event
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
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
                    Debug.Log("Food Lost!");
                }
            }

            return;
        }
        //Remove out-of-range enemies from list
        foreach (var enemy in AiScript.DetectedEnemies.ToList())
        {
            if (enemy.Object == other.gameObject)
            {
                AiScript.DetectedEnemies.Remove(enemy);
                Debug.Log("Enemy Lost!");
            }
        }
    }
}
