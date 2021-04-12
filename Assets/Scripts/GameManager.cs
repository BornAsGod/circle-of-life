using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameStarted = false;

    public bool isGamePaused = false;

    public GameObject menuOverlay;
    public GameObject gameInstructions;
    
    // Maybe look into encapsulation with as little amount of public objects as possible. Use getters and (private) setters if you want more control
    // If you need it to be serializable:
    //[SerializeField]
    //private GameObject myName;

    [Header("Prefabs")] 
    public GameObject[] playerPrefabs = new GameObject[5];
    
    [Header("Spawnpoints")] 
    public Transform[] spawnPoints = new Transform[5];

    [Header("UI")] 
    public Healthbar[] healthBars = new Healthbar[4];

    public Manabar[] manaBars = new Manabar[4];

    private List<AIController> players = new List<AIController>();

    private void Start()
    {
        for (int i = 0; i < playerPrefabs.Length; i++)
        {
            GameObject player = Instantiate(playerPrefabs[i], spawnPoints[i].position, spawnPoints[i].rotation);
            AIController _ai = player.GetComponent<AIController>();
            _ai.healthbar = healthBars[i];
            _ai.manabar = manaBars[i];
            healthBars[i].SetMaxHealth(_ai.Health);
            players.Add(_ai);
            _ai.Home = spawnPoints[i];
        }
    }

    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space))
       {
           gameInstructions.SetActive(false);
           foreach (var player in players)
           {
               isGameStarted = true;
               player.RunGame();
           } 
       }

       if (Input.GetKeyDown(KeyCode.Escape))
       {
           if (!isGamePaused)
           {
               PauseGame();
           }
           else
           {
               ResumeGame();
           }
       }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        menuOverlay.SetActive(false);
        isGamePaused = false;
    }

    public void PauseGame()
    {
        // Setting the timescale to 0 prevents some devices from running properly (controllers don't get input as analogue stick requires it)
        // It's better to stop everything based on one static variable (GameManager.isPaused) or accessible in a static instance (gameManager.isPaused)

        Time.timeScale = 0;
        menuOverlay.SetActive(true);
        isGamePaused = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
