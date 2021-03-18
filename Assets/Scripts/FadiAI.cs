using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class FadiAI : BasePlayer
{
    
    public override IEnumerator RunAI()
    {
        while (health > 0f)
        {
            //Prioritizing enemies over food, that's why I am checking for enemies first
            if (DetectedEnemies.Count > 0) //If there are detected enemies
            {
                yield return Move(GetClosestEnemy()); //Move to closest enemy
            }

            if (DetectedFood.Count > 0)//If food detected
            {
                //Food detected code
                yield return Move(GetClosestFood()); //Go to closest food
            }

            yield return Move(wanderTarget); //If nothing is detected, wander around
        }
        yield return null;
    }
    
}
