﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
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
    public GameObject Object;
    public int Type;
    public float Distance;
    public Vector3 Position;
}
public class BasePlayer
{
    public AIController Player = null;
    //Detection and Navigation
    public List<ScannedEnemy> DetectedEnemies = new List<ScannedEnemy>(); //List of detected enemies
    public List<FoodScanned> DetectedFood = new List<FoodScanned>(); //List of detected foods
    //Wandering
    public Vector3 wanderTarget = Vector3.zero;

    
    //DO NOT CALL
    public void SetPlayer(AIController controller) //Gets set in AIController on start
    {
        /*
         * Gets reference to the AIController
         */
        Player = controller;
    }

    public IEnumerator Move(Vector3 position) //Moves to defined position
    {
        /*
         * Moves player to entered position
         */
        yield return Player._Move(position);
    }

    public IEnumerator RandomMove(Vector3 position)
    {
        yield return Player._RandomMove(position);
    }
    public void BasicAttack(AIController enemy) //Attacks in range player
    {
        /*
         * Calls TakeDamage on the in-range enemy
         */
        Player.BasicAttack(enemy);
    }  
    
    public void SpecialAttack() //Special attack projectile
    {
        /*
         * Launches projectile that damages enemy on collision
         */
        if (Player.Mana == 100f)
        {
            Player.SpecialAttack(Player.specialDamage);
            Player.Mana = 0f;
        }
    }
    
    
    public IEnumerator DoNothing()
    {
        yield return Player._DoNothing();
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

    protected Vector3 GetFavoriteFood()
    {
         Vector3 _food = Vector3.zero;
        foreach (var food in DetectedFood)
        {
            if (food.Type == Player.FavoriteFood)
            {
                _food = food.Position;
            }
        }
        return _food;
    }

    protected bool CheckFavoriteFood()
    {
        bool isFav = false;

        foreach (var food in DetectedFood)
        {
            if (food.Type == Player.FavoriteFood)
            {
                isFav = true;
            }
        }

        return isFav;
    }

    protected ScannedEnemy GetClosestEnemy() //Checks for closest enemy in DetectedEnemies list and returns its position
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
            return DetectedEnemies[0];
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

        return DetectedEnemies[enemyID];
    }

    protected FoodScanned GetClosestFood() //Checks for closest food in DetectedFood list and returns its position
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
            return DetectedFood[0];
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

        return DetectedFood[foodID];
    }

    public void TurnTowardsPlayer(GameObject player)
    {
        Player.transform.LookAt(player.transform);
    }
    
    //Detection events

    public virtual IEnumerator ScannedEnemyEvent(ScannedEnemy enemy) //Triggers when an enemy is detected
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
        yield return new WaitForFixedUpdate();
    }

    public virtual IEnumerator ScannedFoodEvent(FoodScanned food) //Triggers when food is detected
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
        yield return new WaitForFixedUpdate();
    }

    public virtual IEnumerator EnemyInRangeEvent(AIController enemy)
    {
        /*
         * Override in your AI script
         * to attack call:
         * BasicAttack(enemy);
         */
        yield return new WaitForFixedUpdate();
    }
    
}
