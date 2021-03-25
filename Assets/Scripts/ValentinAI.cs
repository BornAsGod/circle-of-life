using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
public class ValentinAI : BasePlayer
{
    /*
     * Duck AI
     * 
     * vector to return Home - done
     * vector for all the "enemy" biomes and mid - in proggress
     * 
     * description:
     * 
     * 0.constantly checks: 
     * -if there is an enemy in range while walking or idling
     * -CurrentHealth changes(attacked by other player)
     *  if attacked will fight back to death
     * 
     * 1.Go Home(spawnPoint)   +
     * 2.checks for food/fav food +
     * 3.coin flip(50/50) to go mid or enemy biome/home
     * 
     * 3.1 goes mid (maybe in the middle ???)
     * 3.1.1 wanders(goes around in a raduis) for "X" amount of seconds or special attack "bar"(100???) is full
     * 3.1.1.1 "x" seconds passed no full bar goes Home and starts at 1.
     * 
     * 3.1.1.2 bar is full - go on a hunt(goes to enemy biome and tries to kill another player/enemy)
     * 3.1.1.2.1 if there is on one on that Biome goes to the next untill it finds an playe/enemy
     * 
     * 3.2 goes to another player/enemy Home and looks for a fight 
     * 3.2.1 goes back Home/ start at 1. 
     * 
     * possible improvments:
     * if health =/= max hide,run, look for food
     * camping on key blocks
     * 
     * questions:
     * 1.how to get the player position.
     * 2. wander for x amount of seconds
     * 3.timer 
     * 4.speed during wander
     * 
     * 
     * roll a dice??
     * health food ???
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     */
    Vector3 Home = new Vector3(278.6f, -105.7384f, 331.5f);

    private int Phase = 0;
    static Vector3 Duck = GameObject.FindGameObjectWithTag("Player").transform.position;

    
    public override IEnumerator RunAI()
    {
        while (Player.Health > 0)
        {
            
            if (Player.Health >= 50f || Player.specialAttackBar == 100)
            {


                if (DetectedEnemies.Count >= 1)
                {
                    yield return Move(GetClosestEnemy().Position);
                }
                else
                {
                    yield return Move(wanderTarget);
                }
            }
            else if (Player.Health<50f && Player.specialAttackBar == 100)
            {
                if (DetectedEnemies.Count >= 1)
                {
                    yield return Move(GetClosestEnemy().Position);
                }
                else
                {
                    yield return Move(wanderTarget);
                }
            }
            else if (Player.Health < 50f && Player.specialAttackBar != 100)
            {
                if (DetectedFood.Count >= 1)
                {
                    
                    yield return Move(GetClosestFood().Position);
                }
                else
                {
                    yield return Move(wanderTarget); 
                }
            }
            
            
        }
        yield return null;
    }
    public override IEnumerator EnemyInRangeEvent(AIController enemy)
    {
        
        if (Player.specialAttackBar == 100)
        {
            SpecialAttack();
        }
        else
        {
            BasicAttack(enemy);
        }
        return null;
    }
    public override IEnumerator ScannedFoodEvent(FoodScanned food)
    {
        
        if (Player.Health >= 50f)
        {
            if (food.Type == Player.FavoriteFood)
            {
                yield return Move(food.Position);
            }
            else
            {
                yield return Move(wanderTarget);
            }
        }
        else
        {
            yield return Move(food.Position);
        }
    }

    public static Vector3 newPosition()
    {
        float radius = 20f;
        Vector3 newPosition = Vector3.zero;
        for (int i = 0; i < 3; i++)
        {

            newPosition[i] = Duck[i] + Random.Range(-radius, radius);
        }
        return newPosition;
    }

}
