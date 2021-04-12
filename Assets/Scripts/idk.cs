using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IDK, I don't know is not a good name for a class
public class idk : BasePlayer
{
    public override IEnumerator RunAI()
    {
        while (Player.Health > 0f)  //while mole is alive
        {

            if (Player.Health > 49f)  //mole has a moderate amount of health, good to go
            {
                if (DetectedEnemies.Count >= 1)  //in this state you can attack thy enemy
                {
                    if (Player.Mana == 100f)
                    {
                        SpecialAttack();
                        yield return null;
                    }
                    else
                    {
                        yield return Move(GetClosestEnemy().Position);
                    }

                }
                else
                {
                    yield return RandomMove(wanderTarget);  //if you cant find an enemy you friggin move around
                }

                if (DetectedFood.Count >= 1)  //if you find a piece of food (while still above 50 hp)
                {
                    if (GetClosestFood().Type == Player.FavoriteFood)  //you check if its your favourite cuz you're a picky eater above 50hp
                    {
                        yield return Move(GetClosestFood().Position);  // you get that bread gamer
                    }
                    else
                    {
                        yield return RandomMove(wanderTarget);  // if it's not your fave you move around idk
                    }
                }
                else
                {
                    yield return RandomMove(wanderTarget);  //if you cant find any food you move around lol
                }

            }

            if (Player.Health < 50f)  //mole is kinda hurt, will not attack if enemy is in range
            {
                if (DetectedFood.Count >= 1)  //if you find any food
                {
                    if (GetClosestFood().Type == Player.FavoriteFood)
                    {
                        yield return Move(GetClosestFood().Position); //you prioritize the fave
                    }

                    if (GetClosestFood().Type != Player.FavoriteFood)
                    {
                        yield return Move(GetClosestFood().Position); //you go get that you hungry af anyways
                    }
                }
                else
                {
                    yield return RandomMove(wanderTarget);  //if not you better walk around and find food
                }

                if (DetectedEnemies.Count >= 1)
                {
                    yield return RandomMove(wanderTarget);
                }

            }

            if (Player.Health < 20f)  //ultimate rodent move, RUNNING AWAY AT ALL COSTS
            {
                if (DetectedFood.Count >= 1 && DetectedEnemies.Count == 0)
                {
                    yield return Move(GetClosestFood().Position);
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
        }

        if (food.Type != Player.FavoriteFood)
        {
            yield return Move(food.Position);
        }

        yield return null;
    }
}
