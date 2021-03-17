using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{
    [SerializeField] private Transform mPlayer;
    [SerializeField] private BasePlayer AiScript;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) {return;}
        var distance = Vector3.Distance(mPlayer.position, other.transform.position);
        AiScript.OnPlayerDetected(distance, other.transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        var distance = Vector3.Distance(mPlayer.position, other.transform.position);
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
