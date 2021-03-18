using System;
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

    }

    private void OnTriggerStay(Collider other)
    {

    }
}
