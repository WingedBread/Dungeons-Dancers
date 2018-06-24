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
    [SerializeField]
    private Ease easingList;
    [Header("Easing Duration On Beat")]
    [SerializeField]
    private float easingOnDuration;
    [Header("Easing Duration Off Beat")]
    [SerializeField]
    private float easingOffDuration;

	[Header("Collision Time Division")]
	[SerializeField]
	private float colTime = 2f;

    [Header("Particle System")]
    private ParticleSystem trapParticleSys;

    private bool activeTrapEvent;

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
            if (!activeTrapEvent) ActiveTrap();
        }
    }
	
    public void ActiveTrap()
    {
        activeTrapEvent = true;
        if (trapParticleSys != null) trapParticleSys.Play();
        StartCoroutine(ColliderCoroutine());
        childSpikes.transform.DOLocalMove(trapMaxHeight, easingOnDuration, false).OnComplete(DisableTrap).SetEase(easingList);
    }

    public void DisableTrap()
    {
        activeTrapEvent = false;
        childSpikes.transform.DOLocalMove(trapMinHeight, easingOffDuration, false).SetEase(easingList);
    }   
   
    private IEnumerator ColliderCoroutine()
    {
		yield return new WaitForSeconds(easingOnDuration / colTime);
        gameObject.GetComponent<Collider>().enabled = true;
		yield return new WaitForSeconds(easingOffDuration / colTime);
        gameObject.GetComponent<Collider>().enabled = false;
        StopCoroutine("ColliderCoroutine");

    }

	public int GetActiveTrap()
	{
		if (activeTrapEvent)
			return 1;
		else return 0;
	}
}
