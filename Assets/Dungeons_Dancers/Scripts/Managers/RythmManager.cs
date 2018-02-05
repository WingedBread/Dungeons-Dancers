using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class RythmManager : MonoBehaviour
{
    private PlayerManager playerManager;

    [Header("Accuracy Calculation")]
    private float duration;
    private float segmentDuration;
    private float segment3, segment2, segment1;

    private int accuracy = 0;
    [HideInInspector]
    public bool activePlayerInputEvent;
    [HideInInspector]
    public bool activePlayerBeatEvent;

    private bool flagAccuracy;
    // Use this for initialization
    void Start()
    {
        playerManager = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        Koreographer.Instance.RegisterForEventsWithTime("PlayerInputEvent", PlayerInputBehaviour);
        Koreographer.Instance.RegisterForEvents("PlayerBeatEvent", PlayerBeatBehaviour);
    }

    void PlayerInputBehaviour(KoreographyEvent kInputEvent, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
    {
        if(playerManager.inputFlag) CalculateTiming(sampleTime, kInputEvent);

        if (sampleTime < kInputEvent.EndSample)
        {
            activePlayerInputEvent = true;
        }
        else
        {
            activePlayerInputEvent = false;
            flagAccuracy = true;
        }

    }

    void PlayerBeatBehaviour(KoreographyEvent kBeatEvent)
    {
        if (!activePlayerBeatEvent)
        {
            activePlayerBeatEvent = true;
            //this.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
        }
        else
        {
            activePlayerBeatEvent = false;
            //this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    void CalculateTiming(int sampleTime, KoreographyEvent kCalcEvent)
    {

        duration = kCalcEvent.EndSample - kCalcEvent.StartSample;
        segmentDuration = duration / 3;
        segment3 = kCalcEvent.EndSample - segmentDuration;
        segment2 = segment3 - segmentDuration;
        segment1 = segment2 - segmentDuration;

        if (sampleTime < segment2) accuracy = 0;
        else if (sampleTime < segment3) accuracy = 1;
        else if (sampleTime < kCalcEvent.EndSample) accuracy = 2;
    }

    public int GetAccuracy()
    {
        return accuracy;
    }
}