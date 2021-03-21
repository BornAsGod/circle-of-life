using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class ScannedEnemy //Stores detected enemy data
{
    public GameObject Object;
    public float Distance;
    public float Health;
    public Vector3 Position;
}

public class FoodScanned //Stores detected food data
{
    public GameObject Type;
    public float Distance;
    public Vector3 Position;
}
public class BasePlayer
{
    //Player Attributes
    public float health = 100f; //Player health
    private float specialDamage = 25f; //Special attack damage
    private float basicDamage = 10f; //Basic attack damage
    private float specialAttackBar = 0f; //Mana bar
    public GameObject favoriteFood = null;

    public Transform mTransform = null;
    //Detection and Navigation
    public List<ScannedEnemy> DetectedEnemies = new List<ScannedEnemy>(); //List of detected enemies
    public List<FoodScanned> DetectedFood = new List<FoodScanned>(); //List of detected foods
    public NavMeshAgent agent; //Player nav mesh agent
    //Wandering
    public Vector3 wanderTarget = Vector3.zero;

    public void SetPlayer(NavMeshAgent _agent, GameObject food, Transform transform) //Gets set in AIController on start
    {
        /*
         * Gets called automatically on start to set some of the players properties
         */
        agent = _agent;
        favoriteFood = food;
        mTransform = transform;
    }

    public IEnumerator Move(Vector3 position) //Moves to defined position
    {
        /*
         * Moves player to entered position
         */
        agent.destination = position;
        yield return new WaitForFixedUpdate();
    }
    public void BasicAttack(BasePlayer enemy) //Attacks in range player
    {
        /*
         * Calls TakeDamage on the in-range enemy
         */
        enemy.TakeDamage(basicDamage);
    }    
    public void SpecialAttack(float attackRange) //Special attack projectile
    {
        /*
         * Launches projectile that damages enemy on collision
         */
    }

    public void TakeDamage(float damage) //Decrease health when attacked
    {
        /*
         * Gets called automatically on the player taking damage
         * as a result of BasicAttack call or collision with a special attack projectile
         */
        health -= damage;
    }

    public void GetFood(float healthAmount, float specialAttackMana) //Adjusts health/mana on food collection
    {
        /*
         * Gets automatically called when colliding with food
         * favorite food check is done in AIController, and the right values get added
         */
        health += healthAmount;
        specialAttackBar += specialAttackMana;
        if (health > 100f)
        {
            health = 100f;
        }

        if (specialAttackBar > 100f)
        {
            specialAttackBar = 100f;
        }
    }

    
    public virtual IEnumerator RunAI() //Runs AI
    {
        /*
         * This is where you call the functions
         * Override this in your AI script take a look at FadiAI
         *
         * Set it to run while health > 0
         */
        yield return null;
    }
    
    public void SetWanderTarget(Vector3 target) //Ran by AIController to get a random position for wandering
    {
        /*
         * To use it call:
         * yield return Move(wanderTarget)
         *
         * a random position on the navmesh within a 150f radius
         * gets changed every 5 seconds
         */
        wanderTarget = target;
    }

    public Vector3 GetClosestEnemy() //Checks for closest enemy in DetectedEnemies list and returns its position
    {
        /*
         * Returns the position of the closest enemy
         *
         * Example:
         *
         * yield return Move(GetClosestEnemy());
         *
         * moves the player towards the closest enemy
         */
        if (DetectedEnemies.Count == 1)
        {
            return DetectedEnemies[0].Position;
        }

        float lowestDistance = 1000f;
        int enemyID = 0;
        for (var i = 0; i < DetectedEnemies.Count; i++)
        {
            if (DetectedEnemies[i].Distance < lowestDistance)
            {
                lowestDistance = DetectedEnemies[i].Distance;
                enemyID = i;
            }
        }
        return DetectedEnemies[enemyID].Position;
    }
    
    public Vector3 GetClosestFood() //Checks for closest food in DetectedFood list and returns its position
    {
        /*
         * Returns the position of the closest food detected
         *
         * Example:
         *
         * yield return Move(GetClosestFood());
         *
         */
        if (DetectedFood.Count == 1)
        {
            return DetectedFood[0].Position;
        }

        float lowestDistance = 1000f;
        int foodID = 0;
        for (var i = 0; i < DetectedFood.Count; i++)
        {
            if (DetectedFood[i].Distance < lowestDistance)
            {
                lowestDistance = DetectedFood[i].Distance;
                foodID = i;
            }
        }
        return DetectedFood[foodID].Position;
    }
    
    //Detection events

    public virtual void ScannedEnemyEvent(ScannedEnemy enemy) //Triggers when an enemy is detected
    {
        /*
         * Override in your AI script
         * ScannedEnemy have attributes like:
         * Object
         * Distance
         * Position
         * Health
         * Check ScannedFoodEvent for examples
         */
    }

    public virtual void ScannedFoodEvent(FoodScanned food) //Triggers when food is detected
    {
        /*
         * Override in your AI script
         * FoodScanned have attributes like:
         * Position (Vector 3) => yield return Move(food.Position)
         * 
         * Distance (Float) => if(food.Distance > 30f)
         * {
         * yield return Move(food.Position);
         * }
         * 
         * Type (GameObject) => if(food.Type == favoriteFood)
         * {
         * yield return Move(food.Position);
         * }
         */
    }

    public virtual void EnemyInRangeEvent(BasePlayer enemy)
    {
        /*
         * Override in your AI script
         * to attack call:
         * yield return BasicAttack(enemy);
         */
    }
    
}
