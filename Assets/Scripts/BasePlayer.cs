using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class BasePlayer
{
    [Header("Player Attributes")]
    [SerializeField] private float health = 100f;
    [SerializeField] private float basicDamage = 10f;
    [SerializeField] private float specialDamage = 25f;
    public GameObject favoriteFood;
    [SerializeField] private float specialAttackBar = 0f;
    [Header("Detection")] 
    [SerializeField] private AttackScript basicRange;
    [SerializeField] private DetectionScript detectionRange;
    [Header("NavMesh")]
    [SerializeField] private NavMeshAgent agent;


    public IEnumerator Move(Vector3 position)
    {
        agent.destination = position;
        yield return new WaitForFixedUpdate();
    }
    public IEnumerator BasicAttack()
    {
        basicRange.isAttacking = true;
        basicRange.Damage = basicDamage;
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

    public IEnumerator GetFood(float healthAmount, float specialAttackMana)
    {
        health += healthAmount;
        specialAttackBar += specialAttackMana;
        yield return new WaitForFixedUpdate();
    }
    
}
