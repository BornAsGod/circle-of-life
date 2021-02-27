using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasePlayer
{
    public float heatlh = 100f;
    public float basicDamage = 10f;
    public float specialDamage = 25f;
    public float basicAttackRange;
    public float specialAttackRange;
    public GameObject favoriteFood;
    public NavMeshAgent agent;

    public IEnumerator move(Vector3 position)
    {
        agent.destination = position;
        yield return new WaitForFixedUpdate();
    }
    public IEnumerator basicAttack(float attackRange)
    {
        yield return new WaitForFixedUpdate();
    }    
    public IEnumerator specialAttack(float attackRange)
    {
        yield return new WaitForFixedUpdate();
    }
}
