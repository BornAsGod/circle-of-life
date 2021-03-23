using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public BasePlayer AiScript;

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

        AIController enemy = other.GetComponent<AIController>();
        StartCoroutine(AiScript.EnemyInRangeEvent(enemy));
    }
}
