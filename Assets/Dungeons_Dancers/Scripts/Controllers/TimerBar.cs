using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBar : MonoBehaviour {

	private GameManager gameManager;
	private float initTimerWidth;
	private float initTimer;
	private float timerBarWidth;

	[SerializeField]
	private GameObject TimerBarObject;

	// Use this for initialization
	void Start () {
		initTimerWidth = TimerBarObject.transform.localScale.x;
		initTimer = 60f; //gameManager.GetDungeonTime ();
		gameManager = GetComponent<GameManager>();
		timerBarWidth = (gameManager.GetDungeonTime()*initTimerWidth)/initTimer;
	}
	
	// Update is called once per frame
	void Update () {
		timerBarWidth = (gameManager.GetDungeonTime()*initTimerWidth)/initTimer;
		TimerBarObject.transform.localScale = new Vector3(timerBarWidth,0.3f,1f);
	}
}
