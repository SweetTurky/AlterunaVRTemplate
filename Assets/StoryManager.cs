using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Avatar = Alteruna.Avatar;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance { get;}

    public UnityEvent OnSceneStart;
    // Start is called before the first frame update
    void Start()
    {
        OnSceneStart.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
