using System.Collections;
using UnityEngine;
using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;

public class RhythmController : MonoBehaviour
{
    [Header("Managers")]
    private GameManager gameManager;
    [SerializeField]
    private DebugController debugController;
    private MultiMusicPlayer multiMusic;

    [Header("Intro Delay Time")]
    [SerializeField]
    private int introTime;

    [Header("Accuracy Calculation")]
    private double duration;
    private double segmentDuration;
    private double segment2, segment3, segment4, segment5;

    private int accuracy = 0;
    private bool activePlayerInputEvent;
    private bool activePlayerBeatEvent;
    private int lastEndSample;

    private double percentPlus;

    bool inputCalcOnce = true;

    [SerializeField]
    private bool debugEnable;
    // Use this for initialization
    void Start()
    {
        multiMusic = GameObject.FindWithTag("MusicPlayer").GetComponent<MultiMusicPlayer>();
        gameManager = GetComponent<GameManager>();
        StartCoroutine(IntroDelayCoroutine(introTime));
    }

    private IEnumerator IntroDelayCoroutine(int time){
        yield return new WaitForSeconds(time);
        for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
        {
            if (gameManager.levelEventsAudios == null) Debug.Log("null audios");
            gameManager.levelEventsAudios[i].SetLevelState(LevelStates.LevelStart);
            gameManager.levelEventsAudios[i].IntroStart();
        }
        for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
        {
            if (gameManager.levelEventsMaterials == null) Debug.Log("null material");
            gameManager.levelEventsMaterials[i].SetLevelState(LevelStates.LevelStart);
            gameManager.levelEventsMaterials[i].IntroStart();
        }
        for (int i = 0; i < gameManager.levelEventsColors.Count; i++)
        {
            if (gameManager.levelEventsColors == null) Debug.Log("null material");
            gameManager.levelEventsColors[i].SetLevelState(LevelStates.LevelStart);
            gameManager.levelEventsColors[i].IntroStart();
        }

        for (int i = 0; i < gameManager.levelEventsEasing1.Count; i++)
        {
            if (gameManager.levelEventsEasing1 == null) Debug.Log("null easing");
            gameManager.levelEventsEasing1[i].SetLevelState(LevelStates.LevelStart);
            gameManager.levelEventsEasing1[i].IntroStart();
        }
        for (int i = 0; i < gameManager.levelEventsEasing2.Count; i++)
        {
            if (gameManager.levelEventsEasing2 == null) Debug.Log("null easing");
            gameManager.levelEventsEasing2[i].SetLevelState(LevelStates.LevelStart);
            gameManager.levelEventsEasing2[i].IntroStart();
        }
        for (int i = 0; i < gameManager.levelEventsEasing3.Count; i++)
        {
            if (gameManager.levelEventsEasing3 == null) Debug.Log("null easing");
            gameManager.levelEventsEasing3[i].SetLevelState(LevelStates.LevelStart);
            gameManager.levelEventsEasing3[i].IntroStart();
        }
        for (int i = 0; i < gameManager.levelEventsEasing4.Count; i++)
        {
            if (gameManager.levelEventsEasing4 == null) Debug.Log("null easing");
            gameManager.levelEventsEasing4[i].SetLevelState(LevelStates.LevelStart);
            gameManager.levelEventsEasing4[i].IntroStart();
        }
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
    }

    private void StopRhythm()
    {
        multiMusic.Stop();
        Koreographer.Instance.UnregisterForEvents("PlayerInputEvent", PlayerInputBehaviour);
        Koreographer.Instance.UnregisterForEvents("PlayerBeatEvent", PlayerBeatBehaviour);
    }

    void IntroBehaviour(KoreographyEvent kIntroEvent)
    {
        gameManager.SetIntroCounter(gameManager.GetIntroCounter()+1);
        gameManager.PlayIntroClip();
    }

    void PlayerInputBehaviour(KoreographyEvent kInputEvent, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
    {
        if (debugEnable) 
        {
            debugController.RhythmStartSpanDebug(kInputEvent.StartSample);
            debugController.RhythmEndSpanDebug(kInputEvent.EndSample);
            debugController.RhythmDurationSpanDebug(kInputEvent.EndSample - kInputEvent.StartSample);
            debugController.SetHitTime(sampleTime);
        }


        if(!gameManager.GetPlayerInputFlag() && inputCalcOnce)
        {
            CalculateTiming(sampleTime, kInputEvent);
            inputCalcOnce = false;
        } 

        if (sampleTime < kInputEvent.EndSample)
        {
            activePlayerInputEvent = true;
        }
        else
        {
            activePlayerInputEvent = false;
            inputCalcOnce = true;
        }

    }

    void PlayerBeatBehaviour(KoreographyEvent kBeatEvent)
    {
        for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
        {
            gameManager.levelEventsAudios[i].OnBeat();
        }
		for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
        {
			gameManager.levelEventsMaterials[i].OnBeat();
        }
        for (int i = 0; i < gameManager.levelEventsColors.Count; i++)
        {
            gameManager.levelEventsColors[i].OnBeat();
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

        if (debugEnable)
        {
            debugController.RhythmBeatPlayerDebug(kBeatEvent.StartSample);
            debugController.RhythmBeatDurationDebug(kBeatEvent.StartSample - lastEndSample);
        }
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

    void CalculateTiming(int sampleTime, KoreographyEvent kCalcEvent)
    {
       
        duration = kCalcEvent.EndSample - kCalcEvent.StartSample;
        segmentDuration = duration / 5;
        //percentPlus = (segmentDuration * 50)/100;
        
        segment5 = kCalcEvent.EndSample - segmentDuration;
        segment4 = segment5 - segmentDuration;
        segment3 = segment4 - segmentDuration;
        segment2 = segment3 - segmentDuration;

        if (sampleTime >= kCalcEvent.StartSample && sampleTime < segment2)
        {
            accuracy = 0; //Good
           //Debug.Log("GOOD 1 -> PushTime: " + sampleTime + "  --Start: " + kCalcEvent.StartSample + "  --End: " + segment2); 
        }
        else if (sampleTime >= segment2 && sampleTime < segment3)
        {
            accuracy = 2; //Great
            //Debug.Log("GREAT 1 -> PushTime: " + sampleTime + "  --Start: " + segment2 + "  --End: " + segment3); 
        }
        else if (sampleTime >= segment3 && sampleTime < segment4)
        {
            accuracy = 1; //Perfect
            //Debug.Log("PERFECT -> PushTime: " + sampleTime + "  --Start: " + segment3 + "  --End: " + segment4); 
        }
        else if (sampleTime >= segment4 && sampleTime < segment5)
        {
            accuracy = 2; //Great
            //Debug.Log("GREAT 2 -> PushTime: " + sampleTime + "  --Start: " + segment4 + "  --End: " + segment5); 
        }
        else if (sampleTime >= segment5 && sampleTime <= kCalcEvent.EndSample)
        {
            accuracy = 0;//Good
            //Debug.Log("GOOD 2 -> PushTime: " + sampleTime + "  --Start: " + segment5 + "  --End: " + kCalcEvent.EndSample); 
        }
        Debug.Log("Accuracy: " + accuracy);
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