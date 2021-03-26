using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
public class ValentinAI : BasePlayer
{
    

    
    public override IEnumerator RunAI()
    {
        while (Player.Health > 0)
        {
            
            if (Player.Health >= 50f || Player.Mana == 100)
            {


                if (DetectedEnemies.Count >= 1)
                {
                    yield return RandomMove(GetClosestEnemy().Position);
                }
                else
                {
                    yield return RandomMove(wanderTarget);
                }
            }
            else if (Player.Health<50f && Player.Mana == 100)
            {
                if (DetectedEnemies.Count >= 1)
                {
                    yield return RandomMove(GetClosestEnemy().Position);
                }
                else
                {
                    yield return RandomMove(wanderTarget);
                }
            }
            else if (Player.Health < 50f && Player.Mana != 100)
            {
                if (DetectedFood.Count >= 1)
                {
                    
                    yield return Move(GetClosestFood().Position);
                }
                else
                {
                    yield return RandomMove(wanderTarget);
                }
            }
            
            
        }
        yield return null;
    }
    public override IEnumerator EnemyInRangeEvent(AIController enemy)
    {
        
        if (Player.Mana == 100)
        {
            SpecialAttack();
        }
        else
        {
            BasicAttack(enemy);
            yield return DoNothing();
        }
        
    }
    public override IEnumerator ScannedFoodEvent(FoodScanned food)
    {
        
        if (Player.Health >= 50f)
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
        else
        {
            yield return Move(food.Position);
            
        }
        yield return RandomMove(wanderTarget);
    }

   

}
