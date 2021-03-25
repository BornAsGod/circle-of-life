using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class FadiAI : BasePlayer
{
    
    public override IEnumerator RunAI()
    {
        while (Player.Health > 0f)
        {
            if (Player.Health > 60f)
            {
                if (DetectedFood.Count >= 1) //If food detected
                {
                    if (CheckFavoriteFood())
                    {
                        yield return Move(GetFavoriteFood());
                    }
                    else
                    {
                        yield return RandomMove(wanderTarget);
                    }
                }

                if (DetectedEnemies.Count >= 1) //If there are detected enemies
                {
                    yield return Move(GetClosestEnemy().Position); //Move to closest enemy
                }
                else
                {
                    yield return RandomMove(wanderTarget); //If nothing is detected, wander around
                }
            }
            else
            {
                //If health is lower than 75 only look for food
                if (DetectedFood.Count >= 1)
                {
                    yield return Move(GetClosestFood().Position);
                }
                else
                {
                    yield return RandomMove(wanderTarget); //If nothing is detected, wander around
                }
            }

            yield return RandomMove(wanderTarget);
        }
        yield return null;
    }

    public override IEnumerator ScannedEnemyEvent(ScannedEnemy enemy)
    {
        if (Player.Mana < 100f)
        {
            if (Player.Health > 70f)
            {
                yield return Move(enemy.Position);
            }
            else
            {
                yield return RandomMove(wanderTarget);
            }
        }
        else
        {
            TurnTowardsPlayer(enemy.Object);
            SpecialAttack();
        }
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
            yield return RandomMove(wanderTarget);
        }
    }

    public override IEnumerator ScannedFoodEvent(FoodScanned food)
    {
        if (Player.Health <= 50f)
        {
            yield return Move(food.Position);
        }
        else
        {
            if (CheckFavoriteFood())
            {
                yield return Move(GetFavoriteFood());
            }
            else
            {
                yield return RandomMove(wanderTarget);
            }
        }
    }
}
