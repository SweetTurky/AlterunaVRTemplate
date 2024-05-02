using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public Transform player1Spawn;
    public Transform player2Spawn;

    private GameObject player1;
    private GameObject player2;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("MovePlayersToSpawn", 0.1f);
    }

    // Update is called once per frame

    private void MovePlayersToSpawn()
    {
        // Find the player GameObjects with the specified tags
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");

        // Check if both players are found
        if (player1 != null)
        {
            // Move the players to their respective spawn positions
            player1.transform.position = player1Spawn.position;
        }
        else 
        {
            Debug.Log("Player 1 was not found");
        }

        if (player2 != null)
        {
            player2.transform.position = player2Spawn.position;
        }
        else
        {
            Debug.Log("Player 2 was not found");
        }
    }
}
