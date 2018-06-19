using System.Collections;
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

    private int pointsflag = 0;

    [Header("AudioClips")]
    [SerializeField]
    private AudioClip introClip;

    private AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        gameManager = GetComponent<GameManager>();
        auMixSnaps[1].TransitionTo(transitionTime); 
	}
	
    //public void PointsSnapshotCheck()
    //{
    //    if (gameManager.GetPoints(0, 1, 0) < gameManager.GetSatisfactionTrackPos(1) && gameManager.GetPoints(0, 1, 0) > gameManager.GetSatisfactionTrackPos(0) && pointsflag != 1)
    //    {
    //        auMixSnaps[1].TransitionTo(transitionTime);
    //        pointsflag = 1;
    //    }
    //    else if (gameManager.GetPoints(0, 1, 0) < gameManager.GetSatisfactionTrackPos(2) && gameManager.GetPoints(0, 1, 0) > gameManager.GetSatisfactionTrackPos(1) && pointsflag != 2)
    //    {
    //        auMixSnaps[2].TransitionTo(transitionTime);
    //        pointsflag = 2;
    //    }
    //    else if (gameManager.GetPoints(0, 1, 0) < gameManager.GetSatisfactionTrackPos(3) && gameManager.GetPoints(0, 1, 0) > gameManager.GetSatisfactionTrackPos(2) && pointsflag != 3)
    //    {
    //        auMixSnaps[3].TransitionTo(transitionTime);
    //        pointsflag = 3;
    //    }
    //    else if (gameManager.GetPoints(0, 1, 0) < gameManager.GetSatisfactionTrackPos(4) && gameManager.GetPoints(0, 1, 0) > gameManager.GetSatisfactionTrackPos(3) && pointsflag != 4)
    //    {
    //        auMixSnaps[4].TransitionTo(transitionTime);
    //        pointsflag = 4;
    //    }
    //    else if (gameManager.GetPoints(0, 1, 0) >= gameManager.GetSatisfactionTrackPos(4) && pointsflag != 5)
    //    {
    //        auMixSnaps[5].TransitionTo(transitionTime);
    //        pointsflag = 5;
    //    }
    //}

    public void PointsSnapshotCheck()
    {
        if (gameManager.GetPoints(0, 1, 0) < gameManager.GetSatisfactionTrackPos(1) && gameManager.GetPoints(0, 1, 0) > gameManager.GetSatisfactionTrackPos(0) && pointsflag != 1)
        {
            auMixSnaps[1].TransitionTo(transitionTime);
            pointsflag = 1;
        }
        else if (gameManager.GetPoints(0, 1, 0) < gameManager.GetSatisfactionTrackPos(2) && gameManager.GetPoints(0, 1, 0) > gameManager.GetSatisfactionTrackPos(1) && pointsflag != 2)
        {
            auMixSnaps[2].TransitionTo(transitionTime);
            pointsflag = 2;
        }
        else if (gameManager.GetPoints(0, 1, 0) < gameManager.GetSatisfactionTrackPos(3) && gameManager.GetPoints(0, 1, 0) > gameManager.GetSatisfactionTrackPos(2) && pointsflag != 3)
        {
            auMixSnaps[3].TransitionTo(transitionTime);
            pointsflag = 3;
        }
        else if (gameManager.GetPoints(0, 1, 0) < gameManager.GetSatisfactionTrackPos(4) && gameManager.GetPoints(0, 1, 0) > gameManager.GetSatisfactionTrackPos(3) && pointsflag != 4)
        {
            auMixSnaps[4].TransitionTo(transitionTime);
            pointsflag = 4;
        }
        else if (gameManager.GetPoints(0, 1, 0) >= gameManager.GetSatisfactionTrackPos(4) && pointsflag != 5)
        {
            auMixSnaps[5].TransitionTo(transitionTime);
            pointsflag = 5;
        }
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
