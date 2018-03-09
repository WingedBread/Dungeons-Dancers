﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StaticTrapBehaviour : MonoBehaviour {
    
    [Header("Choose Trap Behaviour from 1-3")]
    [SerializeField]
    private int trapBehaviour;
    [Header("Choose Trap Max Height")]
    [SerializeField]
    private Vector3 trapMaxHeight;
    [Header("Choose Trap MinHeight")]
    [SerializeField]
    private Vector3 trapMinHeight;
    [Header("Choose Easing")]
    [SerializeField]
    private Ease easingList;
    [Header("Easing Duration On Beat")]
    [SerializeField]
    private float easingOnDuration;
    [Header("Easing Duration Off Beat")]
    [SerializeField]
    private float easingOffDuration;

    private bool activeTrapEvent;

    private GameObject childSpikes;

	// Use this for initialization
	void Start () 
    {
        childSpikes = transform.GetChild(0).GetChild(0).gameObject;
	}
	
    public void ActiveTrap()
    {
        activeTrapEvent = true;
        StartCoroutine(ColliderCoroutine());
        childSpikes.transform.DOLocalMove(trapMaxHeight, easingOnDuration, false).OnComplete(DisableTrap);
    }

    public void DisableTrap()
    {
        activeTrapEvent = false;
        childSpikes.transform.DOLocalMove(trapMinHeight, easingOffDuration, false);
    }

    public int GetTrapBehaviour(){
        return trapBehaviour;
    }

    public bool GetActiveTrapEvent(){
        return activeTrapEvent;
    }

    private IEnumerator ColliderCoroutine()
    {
        yield return new WaitForSeconds(easingOnDuration / 2);
        gameObject.GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(easingOffDuration / 2);
        gameObject.GetComponent<Collider>().enabled = false;
        StopCoroutine("ColliderCoroutine");

    }
}
