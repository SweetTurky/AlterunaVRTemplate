using UnityEngine;
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

    Vector3 startPointLocalPosition = startPoint.localPosition; // Get the local position of the start point

    for (int row = 0; row < rows; row++)
    {
        float offsetX = -row * cupSpacing * 0.5f;
        for (int cupIndex = 0; cupIndex < cupsInRow; cupIndex++)
        {
            Vector3 cupPosition = startPointLocalPosition + new Vector3(offsetX + cupIndex * cupSpacing, 0f, row * cupSpacing * Mathf.Sqrt(3) * 0.5f + zOffset);

            Quaternion cupRotation = Quaternion.identity; // Default rotation

            // Apply reverse orientation for Player 1's cups
            if (startPoint == startPointPlayer1 && reverseOrientation)
            {
                int reverseRow = rows - row - 1; // Reverse the row index
                cupPosition = startPointLocalPosition + new Vector3(offsetX + cupIndex * cupSpacing, 0f, reverseRow * cupSpacing * Mathf.Sqrt(3) * 0.5f + zOffset);
            }

            GameObject newCup = Instantiate(cupPrefab, cupPosition, cupRotation);
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
