using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SonicBloom.Koreo;

public class StaticTrapBehaviour : MonoBehaviour {
	private AudioSource audioSource;

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
	//[SerializeField]
	//private float easingOffLong = 0.46f;
	[Header("Sounds")]
	[SerializeField]
	private AudioClip ActiveSound;

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
		if (this.gameObject.GetComponent<AudioSource>() != null) audioSource = GetComponent<AudioSource>();
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

	// ActiveTrap gestiona l'activació/desactivació de la trampa a cada Beat del seu comportament
    public void ActiveTrap()
    {
		if (activeTrapEvent == false) {
			activeTrapEvent = true;

			// Activem el collider
			gameObject.GetComponent<Collider> ().enabled = true;

			// Disparem les partícules de la trampa (Mirar com fer-no a partir de la gestió de la distància)
			if (trapParticleSys != null)
				trapParticleSys.Play ();
			
			// Fem aparèixer la trampa
			childSpikes.transform.DOLocalMove (trapMaxHeight, easingOnDuration, false).SetEase(easingOn);

			// Fem sonar el so de la trampa
			audioSource.clip = ActiveSound;
			audioSource.Play();

		} else if (activeTrapEvent == true) {
			activeTrapEvent = false;

			// Desactivem el collider
			gameObject.GetComponent<Collider>().enabled = false;
			// Fem desaparèixer la trampa
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
