using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EarPongGameManager : MonoBehaviour
{
    public static EarPongGameManager Instance { get; private set; }

    public int player1CupsDestroyed = 0;
    public int player2CupsDestroyed = 0;

    public AudioClip victorySound;
    public GameObject winParticle;

    //public Transform[] player1CupOriginalPositions; // Assign in Inspector
    //public Transform[] player2CupOriginalPositions; // Assign in Inspector

    private Transform[] player1Cups;
    private Transform[] player2Cups;

    public GameObject p1Wins;
    public GameObject p2Wins;

    public GameObject resetObject;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize player1Cups and player2Cups arrays with cup transforms
        player1Cups = GameObject.FindGameObjectsWithTag("Player1Cup").Select(cup => cup.transform).ToArray();
        player2Cups = GameObject.FindGameObjectsWithTag("Player2Cup").Select(cup => cup.transform).ToArray();

    }

    public void CheckWinCondition(CupOwner owner)
    {
        if (owner == CupOwner.Player1)
        {
            player1CupsDestroyed++;
        }
        else if (owner == CupOwner.Player2)
        {
            player2CupsDestroyed++;
        }

        if (player1CupsDestroyed == 6)
        {
            PlayVictoryEffects();
            Debug.Log("Player 2 wins!");
        }
        else if (player2CupsDestroyed == 6)
        {
            PlayVictoryEffects();
            Debug.Log("Player 1 wins!");
        }
    }

    /*public void ResetPlayerCupsOnClick()
    {
        ResetPlayerCups(player1Cups); // Assuming player1Cups is an array of Transform
        ResetPlayerCups(player2Cups); // Assuming player2Cups is an array of Transform
    }*/

    /*public void ResetPlayerCups(Transform[] cups)
    {
        foreach (Transform cup in cups)
        {
            cup.gameObject.SetActive(true);
            ResetGame();
        }
    }*/

    private void PlayVictoryEffects()
    {
        AudioSource.PlayClipAtPoint(victorySound, Camera.main.transform.position);

        // Instantiate the particle effect
        if (winParticle != null)
        {
            winParticle.SetActive(true);
        }

        if(player1CupsDestroyed >= 6)
        {
            p2Wins.SetActive(true);
        }

        if(player2CupsDestroyed >= 6)
        { 
            p1Wins.SetActive(true);       
        }

        //resetObject.SetActive(true);
    }

    /*private void ResetGame()
    {
        player1CupsDestroyed = 0;
        player2CupsDestroyed = 0;
        // Add any additional reset logic here
    }*/
}
