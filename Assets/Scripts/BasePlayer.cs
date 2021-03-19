using System.Collections;
using System.Collections.Generic;
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
    //Detection and Navigation
    public List<ScannedEnemy> DetectedEnemies = new List<ScannedEnemy>(); //List of detected enemies
    public List<FoodScanned> DetectedFood = new List<FoodScanned>(); //List of detected foods
    private NavMeshAgent agent; //Player nav mesh agent
    //Wandering
    public Vector3 wanderTarget = Vector3.zero;

    public void SetPlayer(NavMeshAgent _agent) //Gets set in AIController on start
    {
        agent = _agent;
    }

    public IEnumerator Move(Vector3 position) //Moves to defined position
    {
        agent.destination = position;
        yield return new WaitForFixedUpdate();
    }
    public IEnumerator BasicAttack() //Attacks in range player
    {
        
        yield return new WaitForFixedUpdate();
    }    
    public IEnumerator SpecialAttack(float attackRange) //Special attack projectile
    {
        yield return new WaitForFixedUpdate();
    }

    public IEnumerator TakeDamage(float damage) //Decrease health when attacked
    {
        health -= damage;
        yield return new WaitForFixedUpdate();
    }

    public IEnumerator GetFood(float healthAmount, float specialAttackMana) //Adjusts health/mana on food collection
    {
        health += healthAmount;
        specialAttackBar += specialAttackMana;
        yield return new WaitForFixedUpdate();
    }

    
    public virtual IEnumerator RunAI() //Runs AI
    {
        yield return null;
    }
    
    public void SetWanderTarget(Vector3 target) //Ran by AIController to get a random position for wandering
    {
        wanderTarget = target;
    }

    public Vector3 GetClosestEnemy() //Checks for closest enemy in DetectedEnemies list and returns its position
    {
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
        //Override in your AI script
    }

    public virtual void ScannedFoodEvent(FoodScanned food) //Triggers when food is detected
    {
        //Override in your AI script
    }
    
}
