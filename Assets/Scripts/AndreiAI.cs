using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndreiAI : BasePlayer
{
    public override IEnumerator RunAI()
    {
        while (Player.Health > 0f)
        {
            if (Player.Health > 75f)
            {
                if (DetectedEnemies.Count > 0)
                {
                        yield return Move(GetClosestEnemy().Position);

                }else if (DetectedFood.Count > 0)
                {
                    if (GetClosestFood().Type == Player.FavoriteFood)
                    {
                        yield return Move(GetClosestFood().Position);
                    }
                }else
                {
                    yield return Move(wanderTarget);
                }
            }
            if (Player.Health <= 75 && Player.Health >= 50)
            {
                if (DetectedEnemies.Count > 0)
                {
                    if (Player.specialAttackBar == 100f)
                    {
                        SpecialAttack();
                        yield return null;
                    }else
                    {
                        yield return Move(GetClosestEnemy().Position);
                    }
                }
                if (DetectedFood.Count > 0)
                {
                    yield return Move(GetClosestFood().Position);
                }else
                {
                    yield return Move(wanderTarget);
                }
            }
            if (Player.Health <= 49 && Player.Health >= 20)
            {
                if (DetectedFood.Count > 0)
                {
                    if (GetClosestFood().Type == Player.FavoriteFood)
                    {
                        yield return Move(GetClosestFood().Position);
                    }
                }else
                {
                    yield return RandomMove(wanderTarget);
                }
            }
            if (Player.Health <= 20)
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

                }else if (DetectedEnemies.Count > 0)
                {
                    yield return Move(GetClosestEnemy().Position);
                }
                else
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