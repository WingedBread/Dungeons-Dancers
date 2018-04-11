using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelEventEvents : MonoBehaviour {

    private AudioSource audioSource;
    private Animator animator;
    private GameObject particles;

    Ease easingListIn;
    Ease easingListOut;

    LevelEvents levelEvents;

    public bool[] activeEvents = new bool[19];

    void Start()
    {
        if(this.gameObject.GetComponent<AudioSource>() != null) audioSource = GetComponent<AudioSource>();
        if (this.gameObject.GetComponent<Animator>() != null) animator = GetComponent<Animator>();
    }
    public void SetLevelEvents(LevelEvents state)
    {
        levelEvents = state;
    }

    public void EventContainer()
    {
        audioSource.Play();
    }

    public int GetRuntimeActiveEvents()
    {
        return (int)levelEvents;
    }

    public void CheckActiveEvents()
    {
        for (int i = 0; i < activeEvents.Length; i++) activeEvents[i] = false;
        activeEvents[GetRuntimeActiveEvents()] = true;
    }

}
