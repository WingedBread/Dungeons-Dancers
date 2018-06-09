using UnityEngine;
using FMODUnity;

public class TestFMODScript_Trap : MonoBehaviour {
	
	[SerializeField]
	private StaticTrapBehaviour trapBhv;

	[SerializeField]
	StudioEventEmitter emitter;

    bool justOnce = true;

	// Update is called once per frame
	void Update () 
	{
        if (trapBhv.GetActiveTrap() == 1 && justOnce)
        {
            emitter.Play();
            justOnce = false;
        }
        else
        {
            justOnce = true;
            emitter.SetParameter("ActiveTrap", trapBhv.GetActiveTrap());
            //Stop When 0 Emitter -- Create Instance
        }
	}

    public void UnloadFMOD()
    {
        emitter.Stop();
    }
}
