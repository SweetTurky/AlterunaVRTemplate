using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class SceneStarter : AttributesSync
{
    private LastSceneManager lastSceneManager;
    public TimerEarpong timerEarpong;
    // Start is called before the first frame update
    void Start()
    {
        lastSceneManager = GetComponent<LastSceneManager>();
        if (lastSceneManager == null)
        {
            Debug.LogError("lastSceneManager component not found on the same GameObject as SceneStarter.");
            return;
        }
        timerEarpong = GetComponent<TimerEarpong>();
        lastSceneManager.p1Ready = true;
        lastSceneManager.p2Ready = true;
        BroadcastRemoteMethod(nameof(StartScene));
    }

    [SynchronizableMethod]
    private void StartScene()
    {
        Debug.Log("Starting Scene...");
        lastSceneManager.ReadyToPlayVO();
    }
    // Update is called once per frame
}