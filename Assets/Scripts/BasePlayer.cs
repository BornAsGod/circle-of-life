using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class BasePlayer
{
    public float health = 100f;
    public float basicDamage = 10f;
    public float specialDamage = 25f;
    public float basicAttackRange;
    public float specialAttackRange;
    public GameObject favoriteFood;
    public NavMeshAgent agent;

    public IEnumerator Move(Vector3 position)
    {
        agent.destination = position;
        yield return new WaitForFixedUpdate();
    }
    public IEnumerator BasicAttack(float attackRange)
    {
        yield return new WaitForFixedUpdate();
    }    
    public IEnumerator SpecialAttack(float attackRange)
    {
        yield return new WaitForFixedUpdate();
    }

    public IEnumerator TakeDamage(float damage)
    {
        health -= damage;
        yield return new WaitForFixedUpdate();
    }

    public IEnumerator GetHealth(float healthAmount)
    {
        health += healthAmount;
        yield return new WaitForFixedUpdate();
    }
}
