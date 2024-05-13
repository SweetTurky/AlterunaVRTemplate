using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTag : MonoBehaviour
{
    private int playerCount = 0; // Static variable to count the number of players

    // Start is called before the first frame update
    void Start()
    {
        if (playerCount == 0)
        {
            gameObject.tag = "Player1"; // First player gets "Player1" tag
        }
        else if (playerCount == 1)
        {
            gameObject.tag = "Player2"; // Second player gets "Player2" tag
        }
        else
        {
            Debug.LogWarning("More than 2 players connected!");
            // You may want to handle this situation differently based on your game's requirements
        }

        playerCount++; // Increment player count for the next player
    }
}
