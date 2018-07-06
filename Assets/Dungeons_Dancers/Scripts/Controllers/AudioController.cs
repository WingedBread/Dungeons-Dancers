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

    [Header("AudioClips")]
    [SerializeField]
    private AudioClip introClip;

    private int selectedSnap;

    private AudioSource audioSource;

    /*Au Mix Snaps:
     * 0 - Mute
     * 1 - Metronomo
     * 2 - Sat Lvl1
     * 3 - Sat Lvl2
     * 4 - Sat Lvl3
     * 5 - Sat Climax
     * 6 - OnlyFX
     */

	// Use this for initialization
	void Start () {
        selectedSnap = 6;
        audioSource = GetComponent<AudioSource>();
        gameManager = GetComponent<GameManager>(); 
	}

    public void PointsSnapshotCheck(int SelectedSnap)
    {
        selectedSnap = SelectedSnap;
        auMixSnaps[SelectedSnap].TransitionTo(transitionTime); 
    }

    public void MuteSound(){
        auMixSnaps[0].TransitionTo(0);
    }
    public void UnmuteSound()
    {
        PointsSnapshotCheck(selectedSnap);
    }

    public void PlayIntro()
    {
        audioSource.volume = 1f;
        audioSource.clip = introClip;
        audioSource.Play();
    }

}
