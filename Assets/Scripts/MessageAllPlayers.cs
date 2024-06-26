using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class MessageAllPlayers : AttributesSync
{
    private StoryManager storyManager;

    void Start()
    {
        storyManager = FindObjectOfType<StoryManager>();
        if (storyManager == null)
        {
            Debug.LogError("StoryManager component not found in the scene.");
        }
    }

    // Example method that previously might have checked p1Ready and p2Ready
    public void ExampleMethod()
    {
        if (storyManager != null)
        {
            // Perform actions here based on the new single-player setup
            // For instance, directly calling methods or starting coroutines

            // Example action:
            storyManager.ClimbReadyVO();
        }
        else
        {
            Debug.LogError("StoryManager not found.");
        }
    }

    // Any other methods or logic can be updated similarly
}
