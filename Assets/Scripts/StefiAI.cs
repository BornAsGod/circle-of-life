using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StefiAI : BasePlayer
{
    public override IEnumerator RunAI()
    {
        while (Player.Health > 0f)
        {
            switch (Player.Health)
            {
                case float _ when (Player.Health > 75f):
                    
                    if (DetectedEnemies.Count == 1)
                    {
                        if (Player.specialAttackBar == 100f)
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
                    else if (DetectedEnemies.Count > 1)
                    {
                        if (Player.specialAttackBar == 100f)
                        {
                            TurnTowardsPlayer(GetClosestEnemy().Object);
                            SpecialAttack();
                            yield return null;
                        }
                        else
                        {
                            yield return Move(GetClosestEnemy().Position + new Vector3(20f, 0f, 20f));
                        }
                    }

                    if (DetectedFood.Count > 0f)
                    {
                        yield return Move(GetClosestFood().Position);
                    }

                    break;
                
                case float _ when (Player.Health >= 50f):

                    if (DetectedFood.Count > 0)
                    {
                        yield return Move(GetClosestFood().Position);
                    }

                    if (DetectedEnemies.Count > 0)
                    {
                        if (Player.specialAttackBar == 100f)
                        {
                            TurnTowardsPlayer(GetClosestEnemy().Object);
                            SpecialAttack();
                            yield return null;
                        }
                        else
                        {
                            yield return Move(GetClosestEnemy().Position + new Vector3(20f, 0f, 20f));
                        }
                    }

                    break;
                
                case float _ when (Player.Health < 50f):

                    if (DetectedFood.Count > 0)
                    {
                        foreach (var food in DetectedFood)
                        {
                            yield return Move(food.Position);
                        }
                    }
                    else
                    {
                        yield return Move(wanderTarget);
                    }
                    
                    yield return Move(Player.Home.position);
                    
                    break;
                
                default:

                    break;
            }
            yield return RandomMove(wanderTarget);
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
        }

        yield return null;
    }
}
