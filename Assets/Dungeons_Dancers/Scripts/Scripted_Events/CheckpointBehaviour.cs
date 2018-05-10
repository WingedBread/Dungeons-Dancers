﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CheckpointBehaviour : MonoBehaviour {

	[SerializeField]
	private GameObject emojiParticles;
	[SerializeField]
	private Image flashUI;
	[SerializeField]
	private GameObject rainbowMouth;
	[SerializeField]
	private GameObject mobilePhone;

	[SerializeField]
	private Sprite checkpointPass;

	private List<GameObject> instantiatedObj = new List<GameObject>();

	[Header("Checkpoint Stuff Duration:")]
	[SerializeField]
	float duration = 0.5f; //TODO: Beat Duration

    
	//Use this for initialization
	void Start () 
	{
		flashUI.gameObject.SetActive(false);
		rainbowMouth.SetActive(false);
		mobilePhone.SetActive(false);
	}
	
	public IEnumerator OnCheckpoint(GameObject currectcheckpoint)
	{
		instantiatedObj.Add((GameObject)Instantiate(emojiParticles, currectcheckpoint.transform));
		flashUI.gameObject.SetActive(true);
        rainbowMouth.SetActive(true);
        mobilePhone.SetActive(true);
		//playAnim
		flashUI.DOFade(0, duration);
		Sequence s = DOTween.Sequence();
		s.Append(mobilePhone.transform.GetChild(0).DOScale((mobilePhone.transform.GetChild(0).localScale * 3), duration/4));
		s.Append(mobilePhone.transform.GetChild(0).DOScale(mobilePhone.transform.GetChild(0).localScale * 0, duration/3));
		currectcheckpoint.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = checkpointPass;

		for (int i = 0; i < emojiParticles.transform.childCount; i++)
        {
            emojiParticles.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
        }

		yield return new WaitForSeconds(duration);

		for (int i = 0; i < emojiParticles.transform.childCount; i++)
        {
			emojiParticles.transform.GetChild(i).GetComponent<ParticleSystem>().Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

		for (int w = 0; w < instantiatedObj.Count; w++)
		{
			Destroy(instantiatedObj[w]);
		}
       
        flashUI.gameObject.SetActive(false);
		mobilePhone.transform.GetChild(0).localScale = new Vector3(0.1f, 0.1f, 0.1f);
		flashUI.color = Color.white;
        rainbowMouth.SetActive(false);
        mobilePhone.SetActive(false);
		StopCoroutine("OnCheckpoint");
	}
}
