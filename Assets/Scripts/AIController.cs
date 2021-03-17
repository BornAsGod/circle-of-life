using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public int AiId;
    public BasePlayer _Ai;

    public NavMeshAgent agent;

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
        _Ai.SetPlayer(transform, agent);
        StartCoroutine(_Ai.RunAI());
    }
}
