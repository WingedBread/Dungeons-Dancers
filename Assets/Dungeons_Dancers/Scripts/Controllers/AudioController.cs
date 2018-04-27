﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {
    
    [Header("Game Manager")]
    private GameManager gameManager;
    [Header("Choose Transition between Snapshots")]
    [SerializeField]
    private float transitionTime;
    [Header("Drag AudioMixer Snapshots")]
    [SerializeField]
    private AudioMixerSnapshot[] auMixSnaps;


    [Header("AudioClips")]
    [SerializeField]
    private AudioClip introClip;

    private AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        gameManager = GetComponent<GameManager>();
	}
	
    public void PointsSnapshotCheck()
    {
		/*
        if (gameManager.GetPoints(0, 1, 0) == 0) auMixSnaps[0].TransitionTo(transitionTime);
        else if (gameManager.GetPoints(0, 1, 0) < 15) auMixSnaps[1].TransitionTo(transitionTime);
        else if(gameManager.GetPoints(0, 1, 0) < 30) auMixSnaps[2].TransitionTo(transitionTime);
        else if (gameManager.GetPoints(0, 1, 0) < 45) auMixSnaps[3].TransitionTo(transitionTime);
        else if (gameManager.GetPoints(0, 1, 0) < 60) auMixSnaps[4].TransitionTo(transitionTime);
        */
		// Modificació Curial
        if (gameManager.GetPoints(0, 1, 0) < gameManager.GetSatisfactionTrackPos(1)) auMixSnaps[1].TransitionTo(transitionTime);		// Metronome
        else if (gameManager.GetPoints(0, 1, 0) < gameManager.GetSatisfactionTrackPos(2)) auMixSnaps[2].TransitionTo(transitionTime);	// Satisf 1
        else if(gameManager.GetPoints(0, 1, 0) < gameManager.GetSatisfactionTrackPos(3)) auMixSnaps[3].TransitionTo(transitionTime);	// Satisf 2 
        else if(gameManager.GetPoints(0, 1, 0) < gameManager.GetSatisfactionTrackPos(4)) auMixSnaps[4].TransitionTo(transitionTime);	// Satisf 3
        else if (gameManager.GetPoints(0, 1, 0) >= gameManager.GetSatisfactionTrackPos(4)) auMixSnaps[5].TransitionTo(transitionTime);	// Satisf 4
    }

    public void MuteSound(){
        auMixSnaps[0].TransitionTo(0);
    }
    public void UnmuteSound()
    {
        PointsSnapshotCheck();
    }

    public void PlayIntro()
    {
        audioSource.volume = 1f;
        audioSource.clip = introClip;
        audioSource.Play();
    }

}
