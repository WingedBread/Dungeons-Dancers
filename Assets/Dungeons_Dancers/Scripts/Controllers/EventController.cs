using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour {

    [Header("Lists")]
    private GameObject[] spawnList;
    private GameObject[] exitList;

	// Use this for initialization
	void Start () {
        spawnList = GameObject.FindGameObjectsWithTag("Spawn");
        exitList = GameObject.FindGameObjectsWithTag("Exit");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
