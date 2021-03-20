﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public bool isAttacking = false;
    public bool inRange = false;
    public BasePlayer AiScript;
    [SerializeField] private float Damage = 10f;
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        BasePlayer enemy = other.GetComponent<AIController>()._Ai;
        AiScript.EnemyInRangeEvent(enemy);
    }
}
