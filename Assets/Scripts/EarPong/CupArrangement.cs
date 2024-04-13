using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CupArrangement : MonoBehaviour
{
    public GameObject cupPrefabPlayer1; // The cup prefab for player 1
    public GameObject cupPrefabPlayer2; // The cup prefab for player 2
    public Transform startPointPlayer1; // Starting point for player 1's cups
    public Transform startPointPlayer2; // Starting point for player 2's cups
    public float cupSpacing = 1.0f; // Spacing between cups

    public bool isSixCupGame = true; // Toggle between 6-cup and 10-cup game

    void Start()
    {
        if (isSixCupGame)
        {
            CreateTriangleArrangement(startPointPlayer1, cupPrefabPlayer1, 6, true); // Passing true for Player 1
            CreateTriangleArrangement(startPointPlayer2, cupPrefabPlayer2, 6, false); // Passing false for Player 2

            startPointPlayer2.transform.localEulerAngles = new Vector3(0, -180, 0);

        }
        else
        {
            CreateTriangleArrangement(startPointPlayer1, cupPrefabPlayer1, 10, true); // Passing true for Player 1
            CreateTriangleArrangement(startPointPlayer2, cupPrefabPlayer2, 10, false); // Passing false for Player 2
        }
    }

void CreateTriangleArrangement(Transform startPoint, GameObject cupPrefab, int totalCups, bool reverseOrientation)
{
    List<GameObject> cups = new List<GameObject>();

    int rows = Mathf.CeilToInt(Mathf.Sqrt(totalCups * 2)); // Calculate rows based on total cups
    int cupsInRow = 1;
    int createdCups = 0;

    float zOffset = 0f;
    if (!isSixCupGame && startPoint == startPointPlayer1)
    {
        zOffset = 0f; // Adjusted offset for Player 1's cups in a 10-cup game
    }
    else if (!isSixCupGame && startPoint == startPointPlayer2)
    {
        zOffset = -0.1f; // Offset for Player 2's cups in a 10-cup game
    }

    for (int row = 0; row < rows; row++)
    {
        float rowOffset = -row * cupSpacing * Mathf.Sqrt(3) * 0.5f;
        float xOffset = -((cupsInRow - 1) * cupSpacing * 0.5f);

        for (int cupIndex = 0; cupIndex < cupsInRow; cupIndex++)
        {
            Vector3 cupPosition = startPoint.position + new Vector3(rowOffset + zOffset, 0f, xOffset + cupIndex * cupSpacing);

            GameObject newCup = Instantiate(cupPrefab, cupPosition, Quaternion.identity);
            newCup.transform.parent = startPoint; // Set the parent to the starting point
            cups.Add(newCup);

            createdCups++;
            if (createdCups >= totalCups)
            {
                break; // Exit loop if all cups are created
            }
        }
        cupsInRow++;
        if (createdCups >= totalCups)
        {
            break; // Exit loop if all cups are created
        }
    }
}

}
