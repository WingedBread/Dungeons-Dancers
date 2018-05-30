using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFMODScript_Trap : MonoBehaviour {
	
	[SerializeField]
	private StaticTrapBehaviour trapBhv;

	[SerializeField]
	FMODUnity.StudioEventEmitter emitter;


	// Update is called once per frame
	void Update () 
	{
        if (trapBhv.GetActiveTrap() == 1) emitter.Play();
		emitter.SetParameter ("ActiveTrap", trapBhv.GetActiveTrap());

	}
}
