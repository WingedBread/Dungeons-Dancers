using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFMODScript : MonoBehaviour {
	
	[SerializeField]
	private SatisfactionController satisController;

	[SerializeField]
	FMODUnity.StudioEventEmitter emitter;

	// Update is called once per frame
	void Update () 
	{
        emitter.SetParameter ("Satisfaction", satisController.GetSatisfactionPoints(0,1,0));
	}
}
