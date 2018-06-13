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
        if(emitter.IsPlaying() == false)
        {
            emitter.Stop();
            //emitter.SetParameter("ActiveTrap", trapBhv.GetActiveTrap());
            justOnce = true;
        }
	}

    public void UnloadFMOD()
    {
        emitter.Stop();
    }
}
