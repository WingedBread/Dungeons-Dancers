using UnityEngine;

public class TestFMODScript_Skeleton : MonoBehaviour {
	
	[SerializeField]
    private MovingEnemyBehaviour enemyBehaviour;

	[SerializeField]
	FMODUnity.StudioEventEmitter emitter;

    bool justOnce = true;

	// Update is called once per frame
	void Update () 
	{
        if (enemyBehaviour.GetActiveTrap() == 1 && justOnce)
        {
            emitter.Play();
            justOnce = false;
        }
        else
        {
            justOnce = true;
            emitter.SetParameter("ActiveTrap", enemyBehaviour.GetActiveTrap());
            //Stop When 0 Emitter -- Create Instance
        }
	}
}
