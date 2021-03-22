using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour
{
    [Header("Player Properties")]
    public AttackScript attack;
    public DetectionScript detect;
    public int AiId;
    public BasePlayer _Ai;
    public NavMeshAgent agent;

    [SerializeField] private Transform _transform;
    //Food
    [Header("Food")]
    public GameObject favoriteFood = null;
    public static float foodHealing = 15f;
    public static float favoriteFoodMana = 25f;

    //Wandering
    [Header("Wandering")]
    [SerializeField] private float wanderRadius;
    [SerializeField] private float wanderTimer;
    private float timer;
    [SerializeField] private Transform Home;
    
    [Header("Attack")]
    public GameObject ProjectilePrefab = null;
    public Transform ProjectileSpawn = null;

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
        _Ai.SetPlayer(this ,agent, favoriteFood, _transform, Home);
        StartCoroutine(_Ai.RunAI());
    }

    private void Update()
    {
        timer += Time.deltaTime; //Wandering timer
        //Assign new wandering position when timer hits 0
        if (timer >= wanderTimer) {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            timer = 0;
            _Ai.SetWanderTarget(newPos);
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

    public void OnFoodCollected(GameObject type)
    {
        
        Debug.Log("Food collected!");

        if (type == favoriteFood)
        {
            _Ai.GetFood(foodHealing, favoriteFoodMana);
            UpdateFoodList(type);
            return;
        }

        _Ai.GetFood(foodHealing, 0f);
        UpdateFoodList(type);
    }

    private void UpdateFoodList(GameObject _food)
    {
        foreach (var food in _Ai.DetectedFood.ToList())
        {
            if (food.Type == _food)
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
}
