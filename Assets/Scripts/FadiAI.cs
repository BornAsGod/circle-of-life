using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class FadiAI : BasePlayer
{
    public override IEnumerator RunAI()
    {
        while (health > 0)
        {
            if (!enemyDetected)
            {
                Vector3 newPos = RandomNavSphere(mTransform.position, wanderRadius, -1);
                yield return Move(newPos);
            }

            if (enemyDetected)
            {
                yield return Move(enemyPosition);
            }

            if (enemyAttackRange)
            {
                yield return BasicAttack();
            }
        }
        yield return null;
    }

    public float wanderRadius = 30f;
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        NavMeshHit navHit;
 
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
        return navHit.position;
    }
}
