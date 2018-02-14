using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMovementController : MonoBehaviour {
    [Header("Rhythm Controller")]
    private RhythmController rhythmController;

    [Header("Choose Scale Multiplier")]
    [SerializeField]
    private float multiplier = 2f;
    private Vector3 ogScale;
    private bool beatflag = false;

	// Use this for initialization
	void Start () {
        rhythmController = GameObject.FindWithTag("GameManager").GetComponent<RhythmController>();
        ogScale = this.gameObject.transform.localScale;
		
	}

    // Update is called once per frame
    void Update()
    {
        if (rhythmController.ActivePlayerBeatEvent() && !beatflag) OnBeatEvent();
        else if(!rhythmController.ActivePlayerBeatEvent() && beatflag) NoBeatEvent();
    }

    void OnBeatEvent(){
        //rewrite from the desrired go?
        this.gameObject.transform.localScale = this.gameObject.transform.localScale*multiplier;
        beatflag = true;
    }

    void NoBeatEvent(){
        //rewrite from the desrired go?
        this.gameObject.transform.localScale = ogScale;
        beatflag = false;
    }       
}
