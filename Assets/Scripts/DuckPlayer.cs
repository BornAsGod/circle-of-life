using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DuckPlayer : BasePlayer
{
    /*
     * Duck AI
     * 
     * vector to return Home - done
     * vector for all the "enemy" biomes and mid - in proggress
     * 
     * description:
     * 
     * 0.constantly checks: 
     * -if there is an enemy in range while walking or ideling
     * -CurrentHelath changes(attacked by other player)
     *  if attacked will fight back to death
     * 
     * 1.Go Home(spawnPoint)   +
     * 2.checks for food/fav food +
     * 3.coin flip(50/50) to go mid or enemy biome/home
     * 
     * 3.1 goes mid (maybe in the middle ???)
     * 3.1.1 wanders(goes around in a raduis) for "X" amount of seconds or special attack "bar"(100???) is full
     * 3.1.1.1 "x" seconds passed no full bar goes Home and starts at 1.
     * 
     * 3.1.1.2 bar is full - go on a hunt(goes to enemy biome and tries to kill another player/enemy)
     * 3.1.1.2.1 if there is on one on that Biome goes to the next untill it finds an playe/enemy
     * 
     * 3.2 goes to another player/enemy Home and looks for a fight 
     * 3.2.1 goes back Home/ start at 1. 
     * 
     * possible improvments:
     * if health =/= max hide,run, look for food
     * camping on key blocks
     * 
     * 
     */
    
    Vector3 Home;

    // Start is called before the first frame update
    void Start()
    {
        Home = new Vector3(278.6f, -105.7384f, 331.5f);
        StartCoroutine(Move(Home));
    }

    // Update is called once per frame
    void Update()
    {
        wander(5f);
    }
    void wander(float timer)
    {
        if (gameObject.transform.position == Home)
        {
            gameObject.GetComponent<WanderingAI>().enabled = true;
            Invoke("goo", timer);
        }
    }
    void goo()
    {
        gameObject.GetComponent<WanderingAI>().enabled = false;
        StartCoroutine(Move(Home));
    }
}
