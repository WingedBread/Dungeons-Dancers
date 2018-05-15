using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoseBehaviour : MonoBehaviour {
   
	private GameObject player;

    [Header("Player Easing")]
    [SerializeField]
    private Ease playerEasing;

    [Header("Player Easing Duration:")]
    [SerializeField]
    float playerDuration = 1.5f;
   

    [Header("Lose Stuff Duration:")]
    [SerializeField]
    float duration = 3f;

    [SerializeField]
    Vector3 playerEndPos;
	// Use this for initialization
	void Start () {
		
	}
	
	public IEnumerator OnLose()
	{
		yield return new WaitForSeconds(duration);
	}
}
