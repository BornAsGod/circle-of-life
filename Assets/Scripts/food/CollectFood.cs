using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFood : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        BasePlayer player = other.gameObject.GetComponent<BasePlayer>();
        if (player.favoriteFood == this.gameObject)
        {
            player.GetFood(15f, 25f);
            Destroy(this.gameObject);
        }
        else
        {
            player.GetFood(15f, 0f);
        }
    }
}
