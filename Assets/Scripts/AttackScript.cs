using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public bool isAttacking = false;
    public bool inRange = false;
    private BasePlayer AiScript;
    [SerializeField] private float Damage = 10f;

    private void Start()
    {
        AiScript = GetComponent<AIController>()._Ai;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
            AiScript.OnPlayerInRange();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
            AiScript.OnPlayerOutOfRange();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isAttacking)
        {
            return;
        }

        if (!inRange)
        {
            return;
        }

        other.GetComponent<BasePlayer>().TakeDamage(Damage);
        isAttacking = false;
    }
}
