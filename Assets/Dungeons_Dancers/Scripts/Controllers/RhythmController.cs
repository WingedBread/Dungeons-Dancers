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
    [SerializeField]
    private DebugController debugController;
    private MultiMusicPlayer multiMusic;

    [Header("Intro Delay Time")]
    [SerializeField]
    private int introTime;

    [Header("Accuracy Calculation")]
    private float duration;
    private float segmentDuration;
    private float segment3, segment2;

    private int accuracy = 0;
    private bool activePlayerInputEvent;
    private bool activePlayerBeatEvent;
    private int lastEndSample;

    // Use this for initialization
    void Start()
    {
        multiMusic = GameObject.FindWithTag("MusicPlayer").GetComponent<MultiMusicPlayer>();
        gameManager = GetComponent<GameManager>();
        enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();
        StartCoroutine(IntroDelayCoroutine(introTime));
    }

    private IEnumerator IntroDelayCoroutine(int time){
        yield return new WaitForSeconds(time);
        StartIntroRhythm();
		StopCoroutine ("IntroDelayCoroutine");
    }
    private void StartIntroRhythm()
    {
        multiMusic.Play();
        Koreographer.Instance.RegisterForEvents("IntroEvent", IntroBehaviour);
    }
    private void StopIntroRhythm()
    {
        Koreographer.Instance.UnregisterForEvents("IntroEvent", IntroBehaviour);
        //multiMusic.Stop();
        //multiMusic.Play();
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
        Koreographer.Instance.UnregisterForEvents("PlayerInputEvent", PlayerInputBehaviour);
        Koreographer.Instance.UnregisterForEvents("PlayerBeatEvent", PlayerBeatBehaviour);
        Koreographer.Instance.UnregisterForEvents("Trap1Event", StaticTrap1BeatBehaviour);
        Koreographer.Instance.UnregisterForEvents("Trap2Event", StaticTrap2BeatBehaviour);
        Koreographer.Instance.UnregisterForEvents("Trap3Event", StaticTrap3BeatBehaviour);
    }

    void IntroBehaviour(KoreographyEvent kIntroEvent)
    {
        gameManager.SetIntroCounter(gameManager.GetIntroCounter()+1);
        gameManager.PlayIntroClip();
    }

    void PlayerInputBehaviour(KoreographyEvent kInputEvent, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
    {
        debugController.RhythmStartSpanDebug(kInputEvent.StartSample);
        debugController.RhythmEndSpanDebug(kInputEvent.EndSample);
        debugController.RhythmDurationSpanDebug(kInputEvent.EndSample - kInputEvent.StartSample);
        debugController.SetHitTime(sampleTime);

        if(gameManager.GetPlayerInputFlag()) CalculateTiming(sampleTime, kInputEvent);

        if (sampleTime < kInputEvent.EndSample)
        {
            activePlayerInputEvent = true;
        }
        else
        {
            activePlayerInputEvent = false;
        }

    }

    void PlayerBeatBehaviour(KoreographyEvent kBeatEvent)
    {
        for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
        {
            gameManager.levelEventsAudios[i].OnBeat();
        }
        for (int i = 0; i < gameManager.levelEventsEasing1.Count; i++)
        {
            gameManager.levelEventsEasing1[i].OnBeat();
        }
        for (int i = 0; i < gameManager.levelEventsEasing2.Count; i++)
        {
            gameManager.levelEventsEasing2[i].OnBeat();
        }
        for (int i = 0; i < gameManager.levelEventsEasing3.Count; i++)
        {
            gameManager.levelEventsEasing3[i].OnBeat();
        }
        for (int i = 0; i < gameManager.levelEventsEasing4.Count; i++)
        {
            gameManager.levelEventsEasing4[i].OnBeat();
        }

        debugController.RhythmBeatPlayerDebug(kBeatEvent.StartSample);
        debugController.RhythmBeatDurationDebug(kBeatEvent.StartSample - lastEndSample);
        lastEndSample = kBeatEvent.EndSample;
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

        if (sampleTime < (segment2 / 3)) accuracy = 0; //Good
        else if (sampleTime < (segment3 - (segment2 / 2))) accuracy = 2; // Great
        else if (sampleTime < (segment3 + (segment2 / 3))) accuracy = 1; //Perfect
        else if (sampleTime < (kCalcEvent.EndSample - (segment3 / 2))) accuracy = 2; //Great
        else if (sampleTime < (kCalcEvent.EndSample - (segment3 / 3))) accuracy = 0; //Great

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

    public void SetIntroRhythm(bool introbool)
    {
        if (introbool) StartCoroutine(IntroDelayCoroutine(introTime));
        else StopIntroRhythm();
    }

    public bool ActivePlayerInputEvent(){
        return activePlayerInputEvent;
    }

    public bool ActivePlayerBeatEvent()
    {
        return activePlayerBeatEvent;
    }
}