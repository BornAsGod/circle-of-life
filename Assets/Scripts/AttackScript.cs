using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public bool inRange = false;
    public bool isAttacking = false;
    [HideInInspector] public float Damage = 0;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (inRange && isAttacking)
        {
            StartCoroutine(Attack(other, Damage));
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
            isAttacking = false;
        }
    }

    public IEnumerator Attack(Collision other, float damage)
    {
        other.gameObject.GetComponent<BasePlayer>().TakeDamage(damage);
        yield return new WaitForSeconds(2f);
    }
}
