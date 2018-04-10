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

    public void SetPlayerState(PlayerStates state)
    {
        playerStates = state;
    }
}
