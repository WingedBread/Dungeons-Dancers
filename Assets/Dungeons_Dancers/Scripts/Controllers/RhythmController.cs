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
    [Header("Intro Delay Time")]
    [SerializeField]
    private int introTime;

    [Header("Accuracy Calculation")]
    private float duration;
    private float segmentDuration;
    private float segment3, segment2, segment1;

    private int accuracy = 0;
    private bool activePlayerInputEvent;
    private bool activePlayerBeatEvent;

    private MultiMusicPlayer multiMusic;

    private bool flagAccuracy;
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
        multiMusic.Play();
        StartIntroRhythm();
    }
    private void StartIntroRhythm()
    {
        Koreographer.Instance.RegisterForEvents("IntroEvent", IntroBehaviour);
    }
    private void StopIntroRhythm()
    {
        Koreographer.Instance.UnregisterForEvents("IntroEvent", IntroBehaviour);
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
        multiMusic.Play();
    }

    void IntroBehaviour(KoreographyEvent kIntroEvent)
    {
        gameManager.SetIntroCounter(gameManager.GetIntroCounter()+1);
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

    public void SetIntroRhythm(bool introbool)
    {
        if (introbool) StartIntroRhythm();
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