using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasingAssetsController : MonoBehaviour {
    [Header("Rhythm Controller")]
    private RhythmController rhythmController;

    [Header("Move or Scale (or Both)")]
    [SerializeField]
    private bool moveEasing;
    [SerializeField]
    private bool scaleEasing;

    [Header("Choose Easing")]
    [SerializeField]
    private iTween.EaseType easingList;

    [Space(20)]
    [Header("-OFF BEAT EASING SETTINGS-")]
    [Header("Choose Scale Off Beat (if needed)")]
    [SerializeField]
    private Vector3 offBeatScaleVector3 = new Vector3(1, 1, 1);
    [Header("Choose Position Off Beat (if needed)")]
    [SerializeField]
    private Vector3 offBeatPositionVector3 = new Vector3(0, 0, 0);
    [Header("Easing Speed Off Beat")]
    [SerializeField]
    private float easingOffSpeed;
    [Header("Easing Duration Off Beat")]
    [SerializeField]
    private float easingOffDuration;

    [Space(20)]
    [Header("-ON BEAT EASING SETTINGS-")]
    [Header("Choose Scale On Beat (if needed)")]
    [SerializeField]
    private Vector3 onBeatScaleVector3 = new Vector3(2, 2, 2);
    [Header("Choose Position On Beat (if needed)")]
    [SerializeField]
    private Vector3 onBeatPositionVector3 = new Vector3(0, 0, 0);
    [Header("Easing Speed On Beat")]
    [SerializeField]
    private float easingOnSpeed;
    [Header("Easing Duration On Beat")]
    [SerializeField]
    private float easingOnDuration;

    // Use this for initialization
	void Start () {
        rhythmController = GameObject.FindWithTag("GameManager").GetComponent<RhythmController>();
	}
                     
    // Update is called once per frame
    void Update()
    {
        if (rhythmController.ActivePlayerBeatEvent())
        {
            OnBeatEvent();
        }
        else if (!rhythmController.ActivePlayerBeatEvent())
        {
            NoBeatEvent();
        }
    }

    void OnBeatEvent(){
        if(scaleEasing) iTween.ScaleTo(gameObject, iTween.Hash("scale", onBeatScaleVector3, "time", easingOnDuration, "speed", easingOnSpeed, "easetype", easingList));
        if(moveEasing)iTween.MoveTo(gameObject, iTween.Hash("position", onBeatPositionVector3, "time", easingOnDuration, "speed", easingOnSpeed, "easetype", easingList));
    }

    void NoBeatEvent(){
        if(scaleEasing)iTween.ScaleTo(gameObject, iTween.Hash("scale", offBeatScaleVector3, "time", easingOffDuration, "speed", easingOffSpeed, "easetype", easingList));
        if(moveEasing) iTween.MoveTo(gameObject, iTween.Hash("position", offBeatPositionVector3, "time", easingOffDuration, "speed", easingOffSpeed, "easetype", easingList));
    }
}
