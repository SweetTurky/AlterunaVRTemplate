using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class MessageAllPlayers : AttributesSync
{
    private StoryManager storyManager;
    //private TimerEarpong timerEarpong;
    //private string sceneToLoad = "Koncert";

    private void Start() 
    {
        storyManager = GetComponent<StoryManager>();
        //timerEarpong = GetComponent<TimerEarpong>();
    }
    public void SendLoadSceneRPC(int sceneId)
    {
        // Invoke method by name for all players. alternatively, we can call by index.
        BroadcastRemoteMethod(nameof(ReceiveLoadSceneRPC), sceneId);
    }

    // the SynchronizableMethod attribute marks methods available for remote invocation.
    [SynchronizableMethod]
    private void ReceiveLoadSceneRPC(int sceneId)
    {
        Multiplayer.LoadScene("EarPong");
    }

    public void SendBoolTrueFalseRPC(string boolName)
    {
        BroadcastRemoteMethod(nameof(ReceiveBoolTrueFalseRPC), boolName);
    }

    [SynchronizableMethod]
    private void ReceiveBoolTrueFalseRPC(string boolName)
    {
        switch (boolName)
        {
            case "p1Ready":
                storyManager.p1Ready = true;
                Debug.Log("P1 ready!");
                break;
            case "p2Ready":
                storyManager.p2Ready = true;
                Debug.Log("P2 ready!");
                break;
            // Add cases for other boolean variables as needed
            default:
                Debug.LogWarning("Unknown boolean variable name: " + boolName);
                break;
        }
    }

    
    /*public void LoadNextScene()
    {
        Multiplayer.Instance.LoadScene(sceneId);
    }*/

}
