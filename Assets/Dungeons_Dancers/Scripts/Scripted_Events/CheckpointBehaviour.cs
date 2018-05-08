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
	private Sprite checkpointCurrent;

	[SerializeField]
    private Sprite checkpointOld;

	[Header("Checkpoint Stuff Duration:")]
	[SerializeField]
	float duration = 1f;

	[Header("Choose Flash UI Easing:")]
	[SerializeField]
	private Ease flashEasing = Ease.Flash;
    
	private List<GameObject> checkpoints = new List<GameObject>();

    
	//Use this for initialization
	void Start () 
	{
		for (int i = 0; i < emojiParticles.Length; i++)
		{
			emojiParticles[i].Stop();
		}
        
		flashUI.gameObject.SetActive(false);
		rainbowMouth.SetActive(false);
		mobilePhone.SetActive(false);
	}
	
	public void OnCheckpoint(GameObject currectcheckpoint)
	{        
		flashUI.gameObject.SetActive(true);
        rainbowMouth.SetActive(true);
        mobilePhone.SetActive(true);
		//playAnim
		flashUI.DOFade(0, duration);
		currectcheckpoint.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = checkpointCurrent;
		checkpoints.Add(currectcheckpoint);
		for (int w = 0; w < checkpoints.Count; w++)
		{
			if(checkpoints[w].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite == checkpointCurrent && checkpoints[w]!= currectcheckpoint)
			{
				checkpoints[w].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = checkpointOld;
			}
		}

		for (int i = 0; i < emojiParticles.Length; i++)
        {
			emojiParticles[i].Play();
        }
	}

	public void DeactivateCheckpoint()
	{
        for (int i = 0; i < emojiParticles.Length; i++)
        {
            emojiParticles[i].Stop();
        }
        flashUI.gameObject.SetActive(false);
        flashUI.color = Color.white;
        rainbowMouth.SetActive(false);
        mobilePhone.SetActive(false);
    }
}
