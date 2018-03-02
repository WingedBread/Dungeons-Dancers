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

    [Header("FX AudioSources")]
    [SerializeField]
    private GameObject FXSource;
    private AudioSource[] FXaudioSource;
	// Use this for initialization
	void Start () {
        FXaudioSource = FXSource.GetComponents<AudioSource>();
        gameManager = GetComponent<GameManager>();
	}
	
    public void PointsSnapshotCheck()
    {
        if(gameManager.GetPoints(0, 1, 0) < 20) auMixSnaps[1].TransitionTo(transitionTime);
        else if(gameManager.GetPoints(0, 1, 0) < 30) auMixSnaps[2].TransitionTo(transitionTime);
        else if (gameManager.GetPoints(0, 1, 0) < 40) auMixSnaps[3].TransitionTo(transitionTime);
        else if (gameManager.GetPoints(0, 1, 0) < 50) auMixSnaps[4].TransitionTo(transitionTime);
    }

    public void MuteSound(){
        auMixSnaps[0].TransitionTo(0);
    }
    public void UnmuteSound()
    {
        PointsSnapshotCheck();
    }

    public void PlayCoin(bool consecutive){
        if (consecutive)
        {
            FXaudioSource[0].pitch = FXaudioSource[0].pitch + 0.1f;
            FXaudioSource[0].Play();
        }
        else
        {
            FXaudioSource[0].pitch = 1;
            FXaudioSource[0].Play();
        } 
    }

    public void PlayCollectible(){
        FXaudioSource[1].Play();
    }

}
