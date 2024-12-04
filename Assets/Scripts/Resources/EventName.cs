using System;
using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

[Serializable]
public class StateEvent
{
    public string nameEvent = default;
    public UltEvent EventTrigger = default;
}
public class EventName : MonoBehaviour
{
    [SerializeField] private StateEvent[] statesEvent = default;
    public void CallEvent(string nameEvent)
    {
        for (int i = 0; i < statesEvent.Length; i++)
        {
            if (statesEvent[i]?.nameEvent == nameEvent)
            {
                statesEvent[i]?.EventTrigger?.Invoke();
            }
        }
    }
}
