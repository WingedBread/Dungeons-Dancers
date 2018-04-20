using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SatisfactionController))]
[RequireComponent(typeof(IntroController))]
[RequireComponent (typeof(AudioController))]
[RequireComponent(typeof(RhythmController))]
[RequireComponent(typeof(UIController))]
[RequireComponent(typeof(EventController))]
public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public List<LevelEventsAudio> levelEventsAudios;
    [HideInInspector]
    public List<LevelEventsEasing> levelEventsEasing;

    [Header("Player Manager")][SerializeField]
    private PlayerManager playerManager;

    [Header("Controllers")]
    private UIController uiController;
    private RhythmController rhythmController;
    private AudioController auController;
    private SatisfactionController satisController;
    private EventController eventController;
    private IntroController introController;

    [Header("God Mode (?)")]
    [SerializeField]
    private bool godMode = false;

    private bool gameStart = false;

    [Header("Time Section")]
    [Header("Dungeon Timer in Seconds")]
    [SerializeField]
    private float initDungeonTimer = 60f;
    private float dungeonTimer;

    private int currentLevelState = 0;

    [SerializeField]
    private DebugController debugController;


    bool flagTimeNearOver = true;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < levelEventsAudios.Count; i++)
        {
            if (levelEventsAudios == null) Debug.Log("null audios");
            levelEventsAudios[i].SetLevelState(LevelStates.LevelStart);
            levelEventsAudios[i].IntroStart();
        }

        for (int i = 0; i < levelEventsEasing.Count; i++)
        {
            if (levelEventsEasing == null) Debug.Log("null easing");
            levelEventsEasing[i].SetLevelState(LevelStates.LevelStart);
            levelEventsEasing[i].IntroStart();
        }

        uiController = GetComponent<UIController>();
        rhythmController = GetComponent<RhythmController>();
        auController = GetComponent<AudioController>();
        satisController = GetComponent<SatisfactionController>();
        eventController = GetComponent<EventController>();
        introController = GetComponent<IntroController>();
        dungeonTimer = initDungeonTimer;
        SetIntroCounter(0);
    }

    void Update()
    {
        Pause();

        if (gameStart)
        {
            if (playerManager.GetBlock() == true) playerManager.SetBlock(false);
            dungeonTimer -= Time.deltaTime;
            if (dungeonTimer <= 0)
            {
                for (int i = 0; i < levelEventsAudios.Count; i++)
                {
                    levelEventsAudios[i].TimeOver();
                }
                Dead();
                dungeonTimer = initDungeonTimer;
            }

            if (dungeonTimer < 5 && flagTimeNearOver) 
            { 
                for (int i = 0; i < levelEventsAudios.Count; i++)
                {
                    levelEventsAudios[i].TimeNearOver();
                }
                flagTimeNearOver = false;
            }
        }
        else playerManager.SetBlock(true);
    }

    public void IntroBehaviour(int intro){
        uiController.IntroUICheck(intro);
        if(GetIntroCounter() == 6){
            for (int i = 0; i < levelEventsAudios.Count; i++)
            {
                levelEventsAudios[i].SetLevelState(LevelStates.LevelPlay);
                levelEventsAudios[i].IntroEnd();
                levelEventsAudios[i].StartPlay();
            }
            gameStart = true;
            rhythmController.SetIntroRhythm(false);
            rhythmController.SetRhythm(true);
        }
    }

    private void Pause()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (Time.timeScale > 0)
            {
                for (int i = 0; i < levelEventsAudios.Count; i++)
                {
                    levelEventsAudios[i].SetLevelState(LevelStates.LevelPaused);
                }
                Time.timeScale = 0;
                gameStart = false;
                auController.MuteSound();
                uiController.PauseUI();
            }
            else
            {
                for (int i = 0; i < levelEventsAudios.Count; i++)
                {
                    levelEventsAudios[i].SetLevelState(LevelStates.LevelPlay);
                }
                Time.timeScale = 1;
                gameStart = true;
                auController.UnmuteSound();
                uiController.ResetUI();
            }
        }
    }

    public void Win()
    {
        for (int i = 0; i < levelEventsAudios.Count; i++)
        {
            levelEventsAudios[i].SetLevelState(LevelStates.LevelEnd);
            levelEventsAudios[i].WinLevel();
        }
        gameStart = false;
        rhythmController.SetRhythm(false);
        uiController.WinUI();
        auController.MuteSound();
        StartCoroutine(Reset());
    }

    public void Dead()
    {
        if (!godMode)
        {
            gameStart = false;
            rhythmController.SetRhythm(false);
            auController.MuteSound();
            uiController.DeadUI();
            StartCoroutine(Reset());
        }
    }

    public void Respawn()
    {
        if (!godMode) 
        {
            
        }
    }

    IEnumerator WaitForKeyDown(KeyCode keyCode)
    {
        while (!Input.GetKeyDown(keyCode))
            yield return null;
        StopCoroutine("WaitForKeyDown");
    }

    private IEnumerator Reset()
    {
        yield return StartCoroutine(WaitForKeyDown(KeyCode.Space));
        StartCoroutine(playerManager.ResetPlayer(true));
        dungeonTimer = initDungeonTimer;
        flagTimeNearOver = true;
        for (int i = 0; i < levelEventsAudios.Count; i++)
        {
            levelEventsAudios[i].SetLevelState(LevelStates.LevelStart);
        }
        satisController.ResetSatisfaction();
        auController.UnmuteSound();
        uiController.ResetUI();
        uiController.CoinsUI(0);
        uiController.CollectibleUI(0);
        SetIntroCounter(0);
        rhythmController.SetIntroRhythm(true);
        StopCoroutine("Reset");
    }

    public void AddPoint()
    {
        auController.PointsSnapshotCheck();
        satisController.AddPoint();
        uiController.AddPointUI();
    }

    public void RemovePoint()
    {
        auController.PointsSnapshotCheck();
        satisController.RemovePoint();
        uiController.RemovePointUI();
    }

    public void CoinBehaviour(int coins)
    {
        uiController.CoinsUI(coins);
    }
    public void CollectibleBehaviour(int collectible)
    {
        uiController.CollectibleUI(collectible);
    }
    public void DoorBehaviour(){
        for (int i = 0; i < levelEventsAudios.Count; i++)
        {
            levelEventsAudios[i].Door();
        }
    }

    #region Getters & Setters
    public int GetPoints(int min, int current, int max)
    {
        if(satisController == null) satisController = GetComponent<SatisfactionController>();
        if (min == 1 && current == 0 && max == 0) return satisController.GetSatisfactionPoints(1, 0, 0);
        else if (min == 0 && current == 1 && max == 0) return satisController.GetSatisfactionPoints(0, 1, 0);
        else if (min == 0 && current == 0 && max == 1) return satisController.GetSatisfactionPoints(0, 0, 1);

        else return 0;
    }
    public bool GetPlayerBlock()
    {
        return playerManager.GetBlock();
    }

    public bool GetSatisfactionFever(){
        return satisController.GetFeverState();
    }

    public int GetIntroCounter(){
        return introController.GetCounter();
    }

    public bool GetRhythmActiveInput()
    {
        return rhythmController.ActivePlayerInputEvent();
    }
    public bool GetRhythmActiveBeat()
    {
        return rhythmController.ActivePlayerBeatEvent();
    }

    public int GetRhythmAccuracy(){
        return rhythmController.GetAccuracy();
    }

    public bool GetGameStatus(){
        return gameStart;
    }

    public bool GetPlayerInputFlag(){
        return playerManager.GetPlayerInputFlag();
    }

    public float GetDungeonTime(){
        return dungeonTimer;  
    }

    public void SetIntroCounter(int setintro)
    {
        introController.SetCounter(setintro);
    }

    public void SetPlayerBlock(bool block){
        playerManager.SetBlock(block);
    }

    public void PlayIntroClip(){
        auController.PlayIntro();
    }

    #endregion
}
