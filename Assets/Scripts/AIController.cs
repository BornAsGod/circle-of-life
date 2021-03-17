using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public BasePlayer _Ai = new FadiAI();

    public NavMeshAgent agent;

    private void Start()
    {
        _Ai.SetPlayer(transform, agent);
        StartCoroutine(_Ai.RunAI());
    }
}
