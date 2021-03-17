using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class BasePlayer : MonoBehaviour
{
    [Header("Player Attributes")]
    public float health = 100f;
    [SerializeField] private float specialDamage = 25f;
    public GameObject favoriteFood;
    [SerializeField] private float specialAttackBar = 0f;
    [Header("Detection")] public Transform mTransform;
    [SerializeField] private AttackScript basicRange;
    [SerializeField] private DetectionScript detectionRange;
    public bool enemyDetected = false;
    public float enemyDistance;
    public bool enemyAttackRange = false;
    public Vector3 enemyPosition;
    [Header("NavMesh")]
    [SerializeField] private NavMeshAgent agent;

    public void SetPlayer(Transform transform, NavMeshAgent _agent)
    {
        mTransform = transform;
        agent = _agent;
    }

    public IEnumerator Move(Vector3 position)
    {
        agent.destination = position;
        yield return new WaitForFixedUpdate();
    }
    public IEnumerator BasicAttack()
    {
        basicRange.isAttacking = true;
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

    public void OnPlayerDetected(float distance, Vector3 pos)
    {
        enemyDetected = true;
        enemyDistance = distance;
        enemyPosition = pos;
    }

    public void OnPlayerLost()
    {
        enemyDetected = false;
    }

    public void OnPlayerInRange()
    {
        enemyAttackRange = true;
    }

    public void OnPlayerOutOfRange()
    {
        enemyAttackRange = false;
    }

    public virtual IEnumerator RunAI()
    {
        yield return null;
    }
    
}
