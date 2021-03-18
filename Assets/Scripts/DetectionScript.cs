using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{
    private BasePlayer AiScript;

    private void Start()
    {
        AiScript = GetComponentInParent<AIController>()._Ai;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) {return;}
        float distance = Vector3.Distance(transform.root.position, other.transform.position);
        AiScript.OnPlayerDetected(distance, other.transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        float distance = Vector3.Distance(transform.root.position, other.transform.position);
        AiScript.OnPlayerDetected(distance, other.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        AiScript.OnPlayerLost();
    }
}
