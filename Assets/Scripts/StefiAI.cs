using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StefiAI : BasePlayer
{
    public override IEnumerator RunAI()
    {
        while (Player.Health > 0f)
        {
            // Interesting usage of switch cases, perhaps look into duke nukem 3D for switch cases gone wrong (It's C++ code, main game loop switch statement is 3000 lines long)
            // The code won't be scalable is my point, perhaps break it down to functions that you call (can be your own enumerators in your case)
            switch (Player.Health)
            {
                case float _ when (Player.Health >= 75f):
                    
                    if (DetectedEnemies.Count == 1)
                    {
                        if (Player.Mana == 100f)
                        {
                            TurnTowardsPlayer(GetClosestEnemy().Object);
                            SpecialAttack();
                            yield return DoNothing();
                        }
                        else
                        {
                            yield return Move(GetClosestEnemy().Position);
                        }
                    }
                    else if (DetectedEnemies.Count > 1)
                    {
                        if (Player.Mana == 100f)
                        {
                            TurnTowardsPlayer(GetClosestEnemy().Object);
                            SpecialAttack();
                            yield return DoNothing();
                        }
                        else
                        {
                            yield return Move(Player.Home.position);
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
                        if (Player.Mana == 100f)
                        {
                            TurnTowardsPlayer(GetClosestEnemy().Object);
                            SpecialAttack();
                            yield return DoNothing();
                        }
                        else
                        {
                            yield return Move(Player.Home.position);
                        }
                    }

                    break;
                
                case float _ when (Player.Health < 50f):

                    if (DetectedFood.Count > 0)
                    {
                        yield return Move(GetClosestFood().Position);
                    }
                    else
                    {
                        yield return RandomMove(wanderTarget);
                    }

                    break;
            }
            yield return RandomMove(wanderTarget);
        }
    }

    public override IEnumerator EnemyInRangeEvent(AIController enemy)
    {
        BasicAttack(enemy);
        yield return DoNothing();
    }

    public override IEnumerator ScannedFoodEvent(FoodScanned food)
    {
        if (food.Type == Player.FavoriteFood)
        {
            yield return Move(food.Position);
        }

        yield return DoNothing();
    }
}
