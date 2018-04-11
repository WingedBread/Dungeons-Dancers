using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerStatesEvents : MonoBehaviour {
    
    private AudioSource audioSource;
    private Animator animator;
    private GameObject particles;

    Ease easingListIn;
    Ease easingListOut;

    PlayerStates playerStates;

    public bool[] activeEvents = new bool[2];

    public void SetPlayerState(PlayerStates state)
    {
        playerStates = state;
    }

    public void EventContainer()
    {
        if (audioSource != null) audioSource.Play();
    }

    public int GetRuntimeActiveEvents()
    {
        return (int)playerStates;
    }

    public void CheckActiveEvents()
    {
        for (int i = 0; i < activeEvents.Length; i++) activeEvents[i] = false;
        activeEvents[GetRuntimeActiveEvents()] = true;
    }
}
