using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBehaviour : MonoBehaviour {

	[SerializeField]
    private GameObject winParticles;

	private Camera mainCamera;

	[SerializeField]
	private Sprite[] lights = new Sprite[3];

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
