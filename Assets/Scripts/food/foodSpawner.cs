using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodSpawner : MonoBehaviour
{

    

    public GameManager _GameManager;

    public Transform Mid;
    public Transform grass;
    public Transform sand;
    public Transform water;
    public Transform dirt;
    public Transform sandstone;

    private float offset = 8.5f;
    public int Maxfood;
    public int FoodSpawned = 0;
    
    int RandomFood;
    int RandomBiome;

    public GameObject[] spawnFood;
    GameObject currentFood;

    public GameObject[] spawnBiome;
    GameObject currentBiome;


    


    // Update is called once per frame
    void Update()
    {
        if (_GameManager.isGameStarted)
        {
            SpawningFoodMid();
        }
    }
    public void SpawningFoodMid()
    {
        PickFood();
        int randomNum = Random.Range(0, Mid.childCount);
        PickFood();
        if (Maxfood>FoodSpawned)
        {
            Instantiate(currentFood, Mid.transform.GetChild(randomNum).position + (transform.up * offset), Quaternion.identity);
            FoodSpawned++;
        }


    }
    public void SpawningFoodGrass()
    {
        
        PickFood();
        PickBiome();
        int randomChild = Random.Range(0, currentBiome.transform.childCount);
        if (Maxfood > FoodSpawned)
        {
            Instantiate(currentFood, currentBiome.transform.GetChild(randomChild).position + (transform.up * 8.5f), Quaternion.identity);
            FoodSpawned++;
        }


    }
    public void PickFood()
    {
        // spawnFood = GameObject.FindGameObjectsWithTag("Food");
        RandomFood = Random.Range(0, spawnFood.Length);
        currentFood = spawnFood[RandomFood];
    }
    public void PickBiome()
    {

        RandomBiome = Random.Range(0, spawnBiome.Length);
        currentBiome = spawnBiome[RandomBiome];
    }

}



