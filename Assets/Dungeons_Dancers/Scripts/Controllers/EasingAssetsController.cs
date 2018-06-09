using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using DG.Tweening;

public class EasingAssetsController : MonoBehaviour {

    [EventID]
    public string eventID;

    private GameManager gameManager;

    [Header("Move or Scale (or Both)")]
    [SerializeField]
    private bool moveEasing;
    [SerializeField]
    private bool scaleEasing;

    [Space(20)]
    [Header("-OFF BEAT EASING SETTINGS-")]
    [Header("Choose Off Beat Easing")]
    [SerializeField]
    private Ease easingOffBeatList;
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
    private Ease easingOnBeatList;
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
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
	}

    private void BeatEvent(KoreographyEvent kevent)
    {
        if(gameManager.GetGameStatus())OnBeatEvent();
    }

    void OnBeatEvent()
    {
        if (scaleEasing)
        {
            Sequence s = DOTween.Sequence();
            s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration)).SetEase(easingOnBeatList);
            s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration)).SetEase(easingOffBeatList);
        }

        if (moveEasing)
        {
            Sequence s = DOTween.Sequence();
            s.Append(transform.DOMove(onBeatScaleVector3, easingOnDuration)).SetEase(easingOnBeatList);
            s.Append(transform.DOMove(offBeatScaleVector3, easingOffDuration)).SetEase(easingOffBeatList);
        }
    }
}
