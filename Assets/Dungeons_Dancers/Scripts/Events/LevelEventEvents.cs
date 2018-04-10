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

    public void SetLevelEvents(LevelEvents state){
        levelEvents = state;
    }

}
