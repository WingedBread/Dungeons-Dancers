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
    private float duration;
    private float segmentDuration;
    private float segment1, segment2, segment3, segment4;

    private int accuracy = 0;
    private bool activePlayerInputEvent;
    private bool activePlayerBeatEvent;
    private int lastEndSample;

    [Header("FMOD")]
    [SerializeField]
    FMOD_Manager fMOD_manager;

    [Header("Mute Intro")]
    [SerializeField]
    bool muteIntro = false;

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

        if (gameManager.fmod_enabled)
        {
            for (int i = 0; i < gameManager.levelEventsAudios_FMOD.Count; i++)
            {
                if (gameManager.levelEventsAudios_FMOD == null) Debug.Log("null audios");
                gameManager.levelEventsAudios_FMOD[i].SetLevelState(LevelStates.LevelStart);
                gameManager.levelEventsAudios_FMOD[i].IntroStart();
            }
        }

        for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
        {
            if (gameManager.levelEventsMaterials == null) Debug.Log("null material");
            gameManager.levelEventsMaterials[i].SetLevelState(LevelStates.LevelStart);
            gameManager.levelEventsMaterials[i].IntroStart();
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

        if(gameManager.fmod_enabled) fMOD_manager.koreoWithFmod = true;
        StartIntroRhythm();
		StopCoroutine ("IntroDelayCoroutine");
    }
    private void StartIntroRhythm()
    {
        if (!gameManager.fmod_enabled) multiMusic.Play();
        //else if(gameManager.fmod_enabled && fMOD_manager.restartBool) fMOD_manager.StartFMOD();
        else
        {
            fMOD_manager.StartFMOD();
        }
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
        if (gameManager.fmod_enabled) fMOD_manager.StopFMOD();
        Koreographer.Instance.UnregisterForEvents("PlayerInputEvent", PlayerInputBehaviour);
        Koreographer.Instance.UnregisterForEvents("PlayerBeatEvent", PlayerBeatBehaviour);
    }

    void IntroBehaviour(KoreographyEvent kIntroEvent)
    {
        gameManager.SetIntroCounter(gameManager.GetIntroCounter()+1);
        if(!muteIntro)gameManager.PlayIntroClip();
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
        if(gameManager.fmod_enabled)
        {
            for (int i = 0; i < gameManager.levelEventsAudios_FMOD.Count; i++)
            {
                gameManager.levelEventsAudios_FMOD[i].OnBeat();
            }
        }
		for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
        {
			gameManager.levelEventsMaterials[i].OnBeat();
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
        segmentDuration = duration / 3;
        segment4 = kCalcEvent.EndSample - segmentDuration;
        segment3 = segment4 - segmentDuration;
        segment2 = segment3 - segmentDuration;
        segment1 = segment2 - segmentDuration;

        if (sampleTime < segment1) accuracy = 0; //Good
        else if (sampleTime < segment2) accuracy = 2; //Great
        else if (sampleTime < segment3) accuracy = 1; //Perfect
        else if (sampleTime < segment4) accuracy = 2; //Great
        else if (sampleTime < kCalcEvent.EndSample) accuracy = 0; //Good

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