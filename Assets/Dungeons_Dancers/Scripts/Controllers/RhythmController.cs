using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;

public class RhythmController : MonoBehaviour
{
    [Header("Managers")]
    private GameManager gameManager;
    private EnemyManager enemyManager;

    [Header("Accuracy Calculation")]
    private float duration;
    private float segmentDuration;
    private float segment3, segment2, segment1;

    private int accuracy = 0;
    private bool activePlayerInputEvent;
    private bool activePlayerBeatEvent;

    private MultiMusicPlayer multiMusic;

    private bool flagAccuracy;
    private bool flagIntro;
    // Use this for initialization
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();
        multiMusic = GameObject.FindWithTag("MusicPlayer").GetComponent<MultiMusicPlayer>();
        multiMusic.Play();
    }
    private void StartIntroRhythm()
    {
        Koreographer.Instance.RegisterForEvents("IntroEvent", IntroBehaviour);
    }
    private void StopIntroRhythm()
    {
        Koreographer.Instance.UnregisterForAllEvents("IntroEvent");
    }
    private void StartRhythm()
    {
        Koreographer.Instance.RegisterForEventsWithTime("PlayerInputEvent", PlayerInputBehaviour);
        Koreographer.Instance.RegisterForEvents("PlayerBeatEvent", PlayerBeatBehaviour);
        Koreographer.Instance.RegisterForEvents("Trap1Event", StaticTrap1BeatBehaviour);
        Koreographer.Instance.RegisterForEvents("Trap2Event", StaticTrap2BeatBehaviour);
        Koreographer.Instance.RegisterForEvents("Trap3Event", StaticTrap3BeatBehaviour);
    }

    private void StopRhythm()
    {
        multiMusic.Stop();
        Koreographer.Instance.UnregisterForAllEvents("PlayerInputEvent");
        Koreographer.Instance.UnregisterForAllEvents("PlayerBeatEvent");
        Koreographer.Instance.UnregisterForAllEvents("Trap1Event");
        Koreographer.Instance.UnregisterForAllEvents("Trap2Event");
        Koreographer.Instance.UnregisterForAllEvents("Trap3Event");
    }

    void IntroBehaviour(KoreographyEvent kIntroEvent)
    {
        if (flagIntro)
        {
            Debug.Log("hu");
            gameManager.introCounter++;
            flagIntro = false;
        }
    }

    void PlayerInputBehaviour(KoreographyEvent kInputEvent, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
    {
        if(gameManager.GetPlayerInputFlag()) CalculateTiming(sampleTime, kInputEvent);

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
        }
        else
        {
            activePlayerBeatEvent = false;
        }
    }

    void StaticTrap1BeatBehaviour(KoreographyEvent kTrap1Event)
    {
        enemyManager.TrapEvent1Behaviour();
    }
    void StaticTrap2BeatBehaviour(KoreographyEvent kTrap2Event)
    {
        enemyManager.TrapEvent2Behaviour();
    }
    void StaticTrap3BeatBehaviour(KoreographyEvent kTrap3Event)
    {
        enemyManager.TrapEvent3Behaviour();
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

    public void SetRhythm(bool rhythm)
    {
        if (rhythm) StartRhythm();
        else StopRhythm();
    }

    public void SetIntroRhythm(bool intro)
    {
        if (intro) StartIntroRhythm();
        else StopIntroRhythm();
    }

    public bool GetIntroRhythm(){
        return flagIntro;
    }

    public void SetIntroFlagRhythm(bool boo){
        flagIntro = boo;
    }

    public bool ActivePlayerInputEvent(){
        return activePlayerInputEvent;
    }

    public bool ActivePlayerBeatEvent()
    {
        return activePlayerBeatEvent;
    }
}