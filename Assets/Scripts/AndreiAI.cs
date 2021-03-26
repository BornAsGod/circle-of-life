using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndreiAI : BasePlayer
{
    public override IEnumerator RunAI()
    {
        while (Player.Health > 0f)
        {

            if (Player.Health > 35f)
            {
                if (DetectedEnemies.Count > 0)
                {
                    if (Player.Mana == 100f)
                    {
                        TurnTowardsPlayer(GetClosestEnemy().Object);
                        SpecialAttack();
                        yield return null;
                    }
                    else
                    {
                        yield return Move(GetClosestEnemy().Position);
                    }
                }
                else if (DetectedFood.Count > 0)
                {
                     if (GetClosestFood().Type == Player.FavoriteFood)
                     {
                        yield return Move(GetClosestFood().Position);
                     }
                     else
                     {
                         yield return Move(GetClosestFood().Position);
                     }
                }else
                {
                    yield return RandomMove(wanderTarget);
                }
            }
            if (Player.Health < 35f)
            {
                if (DetectedFood.Count > 0)
                {
                    if (GetClosestFood().Type == Player.FavoriteFood)
                    {
                        yield return Move(GetClosestFood().Position);
                    }else
                     {
                        yield return Move(GetClosestFood().Position);
                     }
                }else
                {
                    yield return RandomMove(wanderTarget);
                }
            }
            yield return null;
        }
    }

    public override IEnumerator EnemyInRangeEvent(AIController enemy)
    {
        BasicAttack(enemy);
        yield return null;
    }

    public override IEnumerator ScannedFoodEvent(FoodScanned food)
    {
        if (food.Type == Player.FavoriteFood)
        {
            yield return Move(food.Position);
        }else
        {
            yield return Move(food.Position);
        }
        yield return null;
    }
}
