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

    public void SetLevelState(LevelStates state)
    {
        levelStates = state;
    }
}
