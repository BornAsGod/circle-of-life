using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameStarted = false;
    
    [Header("Prefabs")] 
    public GameObject[] playerPrefabs = new GameObject[5];
    
    [Header("Spawnpoints")] public Transform[] spawnPoints = new Transform[5];
    
    private List<AIController> players = new List<AIController>();

    private void Start()
    {
        for (int i = 0; i < playerPrefabs.Length; i++)
        {
            GameObject player = Instantiate(playerPrefabs[i], spawnPoints[i].position, spawnPoints[i].rotation);
            AIController _ai = player.GetComponent<AIController>();
            players.Add(_ai);
            _ai.Home = spawnPoints[i];
        }
    }

    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space))
       {
           foreach (var player in players)
           {
               isGameStarted = true;
               player.RunGame();
           } 
       }
    }
}
