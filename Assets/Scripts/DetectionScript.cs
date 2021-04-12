using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{
    public BasePlayer AiScript;

    private GameManager _gameManager = null;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other) 
    {
      // it seems like only a couple variables change, perhaps a function with parameters could've been written? You can send a list as reference (standard behaviour)
        if (!other.CompareTag("Player"))
        {
            if (!other.CompareTag("Food"))
            {
                return;
            }
            //Add food data to list
            FoodScanned scannedFood = new FoodScanned();
            scannedFood.Distance = Vector3.Distance(transform.root.position, other.transform.position);
            scannedFood.Type = other.GetComponent<CollectFood>().FoodID;
            scannedFood.Object = other.gameObject;
            scannedFood.Position = other.transform.position;
            AiScript.DetectedFood.Add(scannedFood);
            if (_gameManager.isGameStarted)
            {
                StartCoroutine(AiScript.ScannedFoodEvent(scannedFood)); //Call scanned food event
            }
            return;
        }
        //Add enemy data to list
        ScannedEnemy scannedEnemy = new ScannedEnemy();
        scannedEnemy.Distance = Vector3.Distance(transform.root.position, other.transform.position);
        scannedEnemy.Position = other.transform.position;
        scannedEnemy.Object = other.gameObject;
        scannedEnemy.Health = other.GetComponent<AIController>().Health;
        AiScript.DetectedEnemies.Add(scannedEnemy);
        if (_gameManager.isGameStarted)
        {
            StartCoroutine(AiScript.ScannedEnemyEvent(scannedEnemy)); //Call scanned enemy event
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Seems like this data can be cached in enter and removed in exit for optimization
        if (!other.CompareTag("Player"))
        {
            return;
        }
        //Update in-range enemy data
        foreach (var enemy in AiScript.DetectedEnemies.ToList())
        {
            Vector3 _position = other.transform.position;
            if(other.gameObject != enemy.Object )
                return;
            enemy.Position = _position;
            enemy.Distance = Vector3.Distance(transform.root.position, _position);
            enemy.Health = other.GetComponent<AIController>().Health;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Think these two could've been placed in one function with the list as parameter (maybe with a bool for which case)
        if (!other.CompareTag("Player"))
        {
            if (!other.CompareTag("Food"))
            {
                return;
            }

            // Does the same thing as the lines below:
            //AiScript.DetectedFood.RemoveAll(x => x.Object == other);

            //Remove out-of-range food from list
            foreach (var food in AiScript.DetectedFood.ToList())
            {
                if (food.Object == other.gameObject)
                {
                    AiScript.DetectedFood.Remove(food);
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
            }
        }
    }
}
