using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CheckpointBehaviour : MonoBehaviour {

	[SerializeField]
	private ParticleSystem[] emojiParticles;
	[SerializeField]
	private Image flashUI;
	[SerializeField]
	private GameObject rainbowMouth;
	[SerializeField]
	private GameObject mobilePhone;

	[SerializeField]
	private Sprite checkpointPass;

	[Header("Checkpoint Stuff Duration:")]
	[SerializeField]
	float duration = 1f;

	[Header("Choose Flash UI Easing:")]
	[SerializeField]
	private Ease flashEasing = Ease.Flash;

    
	//Use this for initialization
	void Start () 
	{
		for (int i = 0; i < emojiParticles.Length; i++){
			emojiParticles[i].Stop();
		}
		flashUI.gameObject.SetActive(false);
		rainbowMouth.SetActive(false);
		mobilePhone.SetActive(false);
	}
	
	public IEnumerator OnCheckpoint(GameObject currectcheckpoint)
	{        
		flashUI.gameObject.SetActive(true);
        rainbowMouth.SetActive(true);
        mobilePhone.SetActive(true);
		//playAnim
		flashUI.DOFade(0, duration);
		currectcheckpoint.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = checkpointPass;

		for (int i = 0; i < emojiParticles.Length; i++)
        {
			emojiParticles[i].Play();
        }
		yield return new WaitForSeconds(duration);
		for (int i = 0; i < emojiParticles.Length; i++)
		{
			emojiParticles[i].Stop();
		}
        flashUI.gameObject.SetActive(false);
		flashUI.color = Color.white;
        rainbowMouth.SetActive(false);
        mobilePhone.SetActive(false);
		StopCoroutine("OnCheckpoint");
	}
}
