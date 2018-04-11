using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelStateEvents : MonoBehaviour {
    
    private AudioSource audioSource;
    private Animator animator;
    private GameObject particles;

    Ease easingListIn;
    Ease easingListOut;

    LevelStates levelStates;

    public bool[] activeEvents = new bool[3];

    public void SetLevelState(LevelStates state)
    {
        levelStates = state;
    }

    public void EventContainer()
    {
        if (audioSource != null) audioSource.Play();
    }

    public int GetRuntimeActiveEvents()
    {
        return (int)levelStates;
    }

    public void CheckActiveEvents()
    {
        for (int i = 0; i < activeEvents.Length; i++) activeEvents[i] = false;
        activeEvents[GetRuntimeActiveEvents()] = true;
    }
}
