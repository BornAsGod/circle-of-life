using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class BasePlayer
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float basicDamage = 10f;
    [SerializeField] private float specialDamage = 25f;
    [SerializeField] private GameObject basicRange;
    [SerializeField] private GameObject detectionRange;
    [SerializeField] private GameObject favoriteFood;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float specialAttackBar = 0f;

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
