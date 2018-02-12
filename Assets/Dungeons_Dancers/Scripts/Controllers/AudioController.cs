﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {
    
    [Header("Game Manager")]
    private GameManager gameManager;
    [SerializeField]
    private float transitionTime;
    [SerializeField]
    private AudioMixerSnapshot[] auMixSnaps;
	// Use this for initialization
	void Start () {
        gameManager = GetComponent<GameManager>();
	}
	
    public void PointsSnapshotCheck()
    {
        if(gameManager.GetPoints() < 5) auMixSnaps[1].TransitionTo(transitionTime);
        else if(gameManager.GetPoints() < 10) auMixSnaps[2].TransitionTo(transitionTime);
        else if (gameManager.GetPoints() < 15) auMixSnaps[3].TransitionTo(transitionTime);
        else if (gameManager.GetPoints() < 20) auMixSnaps[4].TransitionTo(transitionTime);
    }

    public void MuteSound(){
        auMixSnaps[0].TransitionTo(0);
    }
    public void UnmuteSound()
    {
        auMixSnaps[1].TransitionTo(0);
    }

}
