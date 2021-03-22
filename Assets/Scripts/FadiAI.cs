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
        while (Player.Health > 0f)
        {
            if (Player.Health > 75f)
            {
                //Prioritizing enemies over food, while health is over 75
                if (DetectedEnemies.Count > 0) //If there are detected enemies
                {
                    Debug.Log("Attacking!");
                    yield return Move(GetClosestEnemy().Position); //Move to closest enemy
                }

                if (DetectedFood.Count > 0) //If food detected
                {
                    //Food detected code
                    if (GetClosestFood().Type == favoriteFood)
                    {
                        yield return Move(GetClosestFood().Position); //Go to closest food
                    }
                }
                else
                {
                    yield return Move(wanderTarget); //If nothing is detected, wander around
                }
            }
            else
            {
                //If health is lower than 75 only look for food
                if (DetectedFood.Count > 0)
                {
                    yield return Move(GetClosestFood().Position);
                }
                else
                {
                    yield return Move(wanderTarget); //If nothing is detected, wander around
                }
            }

            yield return null;

        }
        yield return null;
    }

    public override IEnumerator ScannedEnemyEvent(ScannedEnemy enemy)
    {
        if (specialAttackBar < 100f)
        {
            if (Player.Health > 70f)
            {
                yield return Move(enemy.Position);
            }
            else
            {
                yield return Move(wanderTarget);
            }
        }
        else
        {
            TurnTowardsPlayer(enemy.Object);
            SpecialAttack();
        }

        yield return null;
    }

    public override IEnumerator EnemyInRangeEvent(AIController enemy)
    {
        if (Player.Health > 50f)
        {
            BasicAttack(enemy);
            yield return DoNothing();
        }
        else
        {
            yield return Move(wanderTarget);
        }
    }

    public override IEnumerator ScannedFoodEvent(FoodScanned food)
    {
        if (food.Type == favoriteFood)
        {
            yield return Move(food.Position);
        }

        yield return DoNothing();
    }
}
