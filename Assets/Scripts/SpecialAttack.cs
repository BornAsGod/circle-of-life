using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public GameObject projectalPrebab;
    public Transform shootPos; // eyes level positio/shooting laszer????

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           


            GameObject insProj = Instantiate(projectalPrebab, shootPos.transform.position, Quaternion.identity);
            insProj.GetComponent<projectile>().Initialize(shootPos.transform.forward);
        }
    }
    
    public void SpecialAttackk(Transform eyes)//position where the projectile will spawn
    {
        GameObject insProj = Instantiate(projectalPrebab, eyes.position, Quaternion.identity);
        insProj.GetComponent<projectile>().Initialize(eyes.forward);

    }

}
