using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodSpawner : MonoBehaviour
{

    public GameObject Food;
    public Transform Mid;
    public int Maxfood;
    public int FoodSpawned = 0;


    // Start is called before the first frame update
    void Start()
    {
        
        
    }


    // Update is called once per frame
    void Update()
    {
        SpawningFood();
    }
    public void SpawningFood()
    {
        int randomNum = Random.Range(0, Mid.childCount);
        if (Maxfood>FoodSpawned)
        {
            Instantiate(Food, Mid.transform.GetChild(randomNum).position + (transform.up * 10), Quaternion.identity);
            FoodSpawned++;
        }


    }


}



