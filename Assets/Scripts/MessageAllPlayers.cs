using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class MessageAllPlayers : AttributesSync
{
    public void SendLoadSceneRPC(int sceneId)
    {
        // Invoke method by name for all players. alternatively, we can call by index.
        BroadcastRemoteMethod(nameof(ReceiveRPC), sceneId);
    }

    // the SynchronizableMethod attribute marks methods available for remote invocation.
    [SynchronizableMethod]
    private void ReceiveRPC(int sceneId)
    {
        Multiplayer.Instance.LoadScene(sceneId);
    }
}
