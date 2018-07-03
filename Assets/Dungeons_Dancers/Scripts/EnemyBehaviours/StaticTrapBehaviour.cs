using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SonicBloom.Koreo;

public class StaticTrapBehaviour : MonoBehaviour {

	[Header("Choose Beat Behaviour")]
    [EventID]
    public string beatBhv;
	private GameManager gameManager;
    
    [Header("Choose Trap Max Height")]
    [SerializeField]
    private Vector3 trapMaxHeight;
    [Header("Choose Trap MinHeight")]
    [SerializeField]
    private Vector3 trapMinHeight;
    [Header("Choose Easing")]
    [Header("Easing Duration On Beat")]
    [SerializeField]
	private Ease easingOn;
	[SerializeField]
    private float easingOnDuration;
    [Header("Easing Duration Off Beat")]
	[SerializeField]
	private Ease easingOff;
    [SerializeField]
    private float easingOffDuration;
	[SerializeField]
	private float easingOffLong = 0.46f;

    [Header("Particle System")]
    private ParticleSystem trapParticleSys;

    private bool activeTrapEvent = false;

	private int beatCounter = 0;	// For debug beats with behaviour

    private GameObject childSpikes;

	// Use this for initialization
	void Start () 
    {
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		Koreographer.Instance.RegisterForEvents(beatBhv, BeatDetection);
        if(GetComponent<ParticleSystem>() != null) trapParticleSys = GetComponent<ParticleSystem>();
        childSpikes = transform.GetChild(0).GetChild(0).gameObject;
	}

	void BeatDetection(KoreographyEvent evt)
    {
		if (gameManager.GetGameStatus())
		{
			ActiveTrap();
			// Debug Things
			//Debug.Log ("Beat Bhv num: " + beatCounter + " | Trap State: " + activeTrapEvent);
			//beatCounter++;
		}
    }
	
    public void ActiveTrap()
    {
		if (activeTrapEvent == false) {
			activeTrapEvent = true;
			gameObject.GetComponent<Collider> ().enabled = true;
			if (trapParticleSys != null)
				trapParticleSys.Play ();
			childSpikes.transform.DOLocalMove (trapMaxHeight, easingOnDuration, false).SetEase(easingOn);
			// Afegir una seqüència de tancament lent
			/*
			Sequence SpikesUpDown = DOTween.Sequence();
			SpikesUpDown.Append(childSpikes.transform.DOLocalMove(trapMaxHeight, easingOnDuration,false).SetEase(easingOn));
			//SpikesUpDown.PrependInterval (0.1);
			SpikesUpDown.Append(childSpikes.transform.DOLocalMove(trapMinHeight, easingOffLong, false).SetEase(easingOff));
			*/
		} else if (activeTrapEvent == true) {
			activeTrapEvent = false;
			gameObject.GetComponent<Collider>().enabled = false;
			childSpikes.transform.DOLocalMove(trapMinHeight, easingOffDuration, false).SetEase(easingOff);
		}
    }
	public int GetActiveTrap()
	{
		if (activeTrapEvent)
			return 1;
		else return 0;
	}
}
