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
            if (Player.Health > 60f) //High health state
            {
                if (DetectedFood.Count >= 1) //If food detected
                {
                    //Only go for favorite food
                    if (CheckFavoriteFood())
                    {
                        yield return Move(GetFavoriteFood());
                    }
                }

                if (Player.Mana == 100f) //Check for special attack
                {
                    if (DetectedEnemies.Count >= 1) //If there are detected enemies
                    {
                        //Use special attack
                        TurnTowardsPlayer(GetClosestEnemy().Object);
                        SpecialAttack();
                    }
                    else
                    {
                        yield return RandomMove(wanderTarget);
                    }
                }
                else
                {
                    if (DetectedEnemies.Count >= 1)
                    {
                        if (GetClosestEnemy().Health < 60f && Player.canAttack)
                        {
                            yield return Move(GetClosestEnemy().Position);
                        }
                    }
                    else
                    {
                        yield return RandomMove(wanderTarget);
                    }
                }
            }
            else //Low health state
            {
                
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
        if (Player.Mana == 100f)
        {
            TurnTowardsPlayer(enemy.Object);
            SpecialAttack();
            yield return RandomMove(wanderTarget);
        }
        else
        {
            if (Player.Health > 60f && enemy.Health < 60f)
            {
                yield return Move(enemy.Position);
            }
            else
            {
                yield return RandomMove(wanderTarget);
            }
        }

        yield return RandomMove(wanderTarget);
    }

    public override IEnumerator EnemyInRangeEvent(AIController enemy)
    {
        if (Player.Health > 60f)
        {
            BasicAttack(enemy);
            yield return DoNothing();
        }
        else
        {
            yield return RandomMove(wanderTarget);
        }

        yield return RandomMove(wanderTarget);
    }

    public override IEnumerator ScannedFoodEvent(FoodScanned food)
    {
        if (Player.Health <= 60f)
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

        yield return RandomMove(wanderTarget);
    }
}
