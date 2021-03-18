using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour
{
    public AttackScript attack;
    public DetectionScript detect;
    public int AiId;
    public BasePlayer _Ai;
    public NavMeshAgent agent;
    
    //Wandering
    public float wanderRadius;
    public float wanderTimer;
    private float timer;

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
        }
    }
    

    private void Start()
    {
        timer = wanderTimer;
        detect.AiScript = _Ai;
        attack.AiScript = _Ai;
        _Ai.SetPlayer(agent);
        StartCoroutine(_Ai.RunAI());
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= wanderTimer) {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            timer = 0;
            _Ai.SetWanderTarget(newPos);
        }
    }
    
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        NavMeshHit navHit;
 
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
        return navHit.position;
    }
}
