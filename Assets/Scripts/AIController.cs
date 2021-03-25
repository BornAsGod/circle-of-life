using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[DisallowMultipleComponent]
public class AIController : MonoBehaviour
{
    
    [Header("Player Properties")]
    public AttackScript attack;
    public DetectionScript detect;
    public int AiId;
    public BasePlayer _Ai;
    public float Health = 100f;
    public float specialAttackBar = 0f; //Mana bar
    [SerializeField] private NavMeshAgent agent;

    [Header("Food")]

    public int FavoriteFood;

    public static float foodHealing = 15f;
    public static float favoriteFoodMana = 25f;

    [Header("Animation")] 
    public Animator anim = null;
    
    [Header("Wandering")]
    [SerializeField] private float wanderRadius;
    [SerializeField] private float wanderTimer;
    private float timer;
    public Transform Home;
    
    [Header("Attack")]
    public float specialDamage = 25f; //Special attack damage
    public float basicDamage = 10f; 
    public GameObject ProjectilePrefab = null;
    public Transform ProjectileSpawn = null;
    public float attackCooldown = 5f;
    public bool canAttack = true;

    private void Awake()
    {
        switch (AiId)
        {
            case 0:
                _Ai = new FadiAI();
                break;
            case 1:
                _Ai = new ValentinAI();
                break;
            case 2:
                _Ai = new StefiAI();
                break;
                
        }
    }
    
    private void Start()
    {
        timer = wanderTimer;
        detect.AiScript = _Ai;
        attack.AiScript = _Ai;
        _Ai.SetPlayer(this);
    }

    private void Update()
    {
        AnimatePlayer();
        timer += Time.deltaTime; //Wandering timer
        //Assign new wandering position when timer hits 0
        if (timer >= wanderTimer) 
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            timer = 0;
            _Ai.SetWanderTarget(newPos);
        }

        if (!canAttack)
        {
            attackCooldown -= Time.deltaTime;

            if (attackCooldown <= 0)
            {
                canAttack = true;
            }
        }
    }
    
    //Gets random position for wandering
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        NavMeshHit navHit;
 
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
        return navHit.position;
    }

    public IEnumerator _Move(Vector3 position)
    {
        /*
         * Moves player to set destination
         */
        agent.SetDestination(position);
        yield return new WaitForFixedUpdate();
    }

    public IEnumerator _DoNothing()
    {
        yield return new WaitForFixedUpdate();
    }
    
    public void OnFoodCollected(GameObject type, int id)
    {
        
        Debug.Log("Food collected!");

        if (id == FavoriteFood)
        {
            Debug.Log("Favorite food detected!");
            GetFood(foodHealing, favoriteFoodMana);
            UpdateFoodList(type);
            return;
        }

        GetFood(foodHealing, 0f);
        UpdateFoodList(type);
    }

    private void UpdateFoodList(GameObject _food)
    {
        foreach (var food in _Ai.DetectedFood.ToList())
        {
            if (food.Object == _food)
            {
                _Ai.DetectedFood.Remove(food);
                Debug.Log("Removed collected food!");
            }
        }
    }

    public void SpecialAttack(float damage)
    {
        GameObject insProj = Instantiate(ProjectilePrefab, ProjectileSpawn.position, Quaternion.identity);
        insProj.GetComponent<projectile>().Initialize(ProjectileSpawn.forward, damage);
    }

    public void RunGame()
    {
        StartCoroutine(_Ai.RunAI());
    }
    
    //DO NOT CALL
    public void GetFood(float healthAmount, float specialAttackMana) //Adjusts health/mana on food collection
    {
        /*
         * Gets automatically called when colliding with food
         * favorite food check is done in AIController, and the right values get added
         */
        Health += healthAmount;
        specialAttackBar += specialAttackMana;
        if (Health > 100f)
        {
            Health = 100f;
        }

        if (specialAttackBar > 100f)
        {
            specialAttackBar = 100f;
        }
    }

    private void AnimatePlayer()
    {
        if (agent.remainingDistance > 1f)
        {
            anim.SetBool("move", true);
        }
        else
        {
            anim.SetBool("move", false);
        }

        if (anim.GetBool("attack"))
        {
            anim.SetBool("attack", false);
        }
    }
}
