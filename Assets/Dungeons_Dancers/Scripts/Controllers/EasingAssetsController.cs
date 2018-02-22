using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class EasingAssetsController : MonoBehaviour {

    [EventID]
    public string eventID;

    [Header("Move or Scale (or Both)")]
    [SerializeField]
    private bool moveEasing;
    [SerializeField]
    private bool scaleEasing;

    [Space(20)]
    [Header("-OFF BEAT EASING SETTINGS-")]
    [Header("Choose Off Beat Easing")]
    [SerializeField]
    private iTween.EaseType easingOffBeatList;
    [Header("Choose Scale Off Beat (if needed)")]
    [SerializeField]
    private Vector3 offBeatScaleVector3 = new Vector3(1, 1, 1);
    [Header("Choose Position Off Beat (if needed)")]
    [SerializeField]
    private Vector3 offBeatPositionVector3 = new Vector3(0, 0, 0);
    [Header("Easing Duration Off Beat")]
    [SerializeField]
    private float easingOffDuration;

    [Space(20)]
    [Header("-ON BEAT EASING SETTINGS-")]
    [Header("Choose On Beat Easing")]
    [SerializeField]
    private iTween.EaseType easingOnBeatList;
    [Header("Choose Scale On Beat (if needed)")]
    [SerializeField]
    private Vector3 onBeatScaleVector3 = new Vector3(2, 2, 2);
    [Header("Choose Position On Beat (if needed)")]
    [SerializeField]
    private Vector3 onBeatPositionVector3 = new Vector3(0, 0, 0);
    [Header("Easing Duration On Beat")]
    [SerializeField]
    private float easingOnDuration;

    private bool beatflag;

    // Use this for initialization
	void Start () {
        Koreographer.Instance.RegisterForEvents(eventID, BeatEvent);
	}

    private void BeatEvent(KoreographyEvent kevent)
    {

        if (!beatflag)
        {
            iTween.StopByName("onbeat");
            //this.gameObject.transform.localScale = offBeatScaleVector3;
            OnBeatEvent();
            beatflag = true;

        }
        else
        {
            OffBeatEvent();
            beatflag = false;
        }

    }

    void OnBeatEvent()
    {
        if (scaleEasing)
        {
            iTween.ScaleTo(gameObject, iTween.Hash("name", "onbeat", "scale", onBeatScaleVector3, "time", easingOnDuration, "easetype", easingOnBeatList, "oncomplete", "OffBeatEvent"));
        }
        if (moveEasing)
        {
            iTween.ScaleTo(gameObject, iTween.Hash("name", "onbeat", "scale", onBeatScaleVector3, "time", easingOnDuration, "easetype", easingOnBeatList, "oncomplete", "OffBeatEvent"));        
        }
    }

    void OffBeatEvent()
    {
        if (scaleEasing)
        {
            iTween.ScaleTo(gameObject, iTween.Hash("name", "OffBeatScale", "scale", offBeatScaleVector3, "time", easingOffDuration, "easetype", easingOffBeatList));
        }
        if (moveEasing) 
        {
            iTween.MoveTo(gameObject, iTween.Hash("name", "OffBeatMovement", "position", offBeatPositionVector3, "time", easingOffDuration, "easetype", easingOffBeatList));
        }
    }
}
