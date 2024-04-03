using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public UnityEvent OnEventTriggered;

    public void TriggerEvent()
    {
        OnEventTriggered.Invoke();
    }

    // Add more event management functions as needed
}
