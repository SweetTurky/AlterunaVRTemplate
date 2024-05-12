using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    // Definer variabel til at holde referencer til anchor points
    public Transform[] anchorPoints;

    // Metode til at teleportere NPC'en til en given destination
    public void TeleportTo(Transform destination)
    {
        // Teleportér NPC'en til den givne destination
        transform.position = destination.position;
        transform.rotation = destination.rotation;
        Debug.Log("Har teleporteret");
    }

    // Eksempel på, hvordan du kan kalde teleportationen
    void TeleportToRandomAnchorPoint()
    {
        // Vælg tilfældigt et anchor point
        int randomIndex = Random.Range(0, anchorPoints.Length);
        Transform randomAnchorPoint = anchorPoints[randomIndex];

        // Teleportér NPC'en til det tilfældigt valgte anchor point
        TeleportTo(randomAnchorPoint);
    }

    // Eksempel på, hvordan du kan kalde teleportationen fra en anden klasse eller metode
    void ExampleUsage()
    {
         if (Input.GetKeyDown("space"))
        {
            Debug.Log("space key was pressed");
        // Antag at dette kaldes på et tidspunkt, hvor du ønsker at teleportere NPC'en
        TeleportToRandomAnchorPoint();
        }
    }
    /*void Start()
    {
        TeleportToRandomAnchorPoint();
    }*/
    void Update()
    {
         if (Input.GetKeyDown("space"))
        {
            Debug.Log("space key was pressed");
        // Antag at dette kaldes på et tidspunkt, hvor du ønsker at teleportere NPC'en
        TeleportToRandomAnchorPoint();
        }
    }
    void TeleportToWall()
    {
        TeleportTo(anchorPoints[1]);
    }
    void TeleportToBeerpong()
    {
        TeleportTo(anchorPoints[2]);
    }
}

