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
    private AudioClip coinClip;
    [SerializeField]
    private AudioClip keyClip;
    [SerializeField]
    private AudioClip endLeveClip;
    [SerializeField]
    private AudioClip retryClip;

    private AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        gameManager = GetComponent<GameManager>();
	}
	
    public void PointsSnapshotCheck()
    {
<<<<<<< HEAD
        if (gameManager.GetPoints(0, 1, 0) == 0) auMixSnaps[0].TransitionTo(transitionTime);
        else if (gameManager.GetPoints(0, 1, 0) < 10) auMixSnaps[1].TransitionTo(transitionTime);
        else if(gameManager.GetPoints(0, 1, 0) < 20) auMixSnaps[2].TransitionTo(transitionTime);
        else if (gameManager.GetPoints(0, 1, 0) < 30) auMixSnaps[3].TransitionTo(transitionTime);
        else if (gameManager.GetPoints(0, 1, 0) < 40) auMixSnaps[4].TransitionTo(transitionTime);
=======
        if(gameManager.GetPoints(0, 1, 0) < 20) auMixSnaps[1].TransitionTo(transitionTime);
        else if(gameManager.GetPoints(0, 1, 0) < 30) auMixSnaps[2].TransitionTo(transitionTime);
        else if (gameManager.GetPoints(0, 1, 0) < 40) auMixSnaps[3].TransitionTo(transitionTime);
        else if (gameManager.GetPoints(0, 1, 0) < 50) auMixSnaps[4].TransitionTo(transitionTime);
>>>>>>> 23b2777b0074dbc495bdb555da356e84bc9d81f3
    }

    public void MuteSound(){
        auMixSnaps[0].TransitionTo(0);
    }
    public void UnmuteSound()
    {
        PointsSnapshotCheck();
    }

    public void PlayCoin(bool consecutive){
        audioSource.volume = 0.1f;
        audioSource.clip = coinClip;
        if (consecutive)
        {
            audioSource.pitch = audioSource.pitch + 0.1f;
            audioSource.Play();
        }
        else
        {
            audioSource.pitch = 1;
            audioSource.Play();
        } 
    }

    public void PlayCollectible(){
        audioSource.volume = 1f;
        audioSource.clip = keyClip;
        audioSource.Play();
    }

    public void PlayRetry()
    {
        audioSource.volume = 1f;
        audioSource.clip = retryClip;
        audioSource.Play();
    }

    public void PlayEndLevel()
    {
        audioSource.volume = 1f;
        audioSource.clip = endLeveClip;
        audioSource.Play();
    }

}
