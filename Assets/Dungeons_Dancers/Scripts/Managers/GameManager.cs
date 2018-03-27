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
    [Header("Setup Level")]
    public LevelSetup levelSetup;

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

    private LevelStates state;

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
        state = LevelStates.LevelStart;
        levelSetup.EvtIntroStart();
        debugController.GameState((int)state);
        uiController = GetComponent<UIController>();
        rhythmController = GetComponent<RhythmController>();
        auController = GetComponent<AudioController>();
        satisController = GetComponent<SatisfactionController>();
        eventController = GetComponent<EventController>();
        introController = GetComponent<IntroController>();
        dungeonTimer = initDungeonTimer;
        auController.MuteSound();
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
                levelSetup.EvtTimeOver();
                Dead();
                dungeonTimer = initDungeonTimer;
            }

            if (dungeonTimer < 5 && flagTimeNearOver) 
            { 
                levelSetup.EvtTimeNearOver();
                flagTimeNearOver = false;
            }
        }
        else playerManager.SetBlock(true);
    }

    public void IntroBehaviour(int intro){
        uiController.IntroUICheck(intro);
        if(GetIntroCounter() == 5){
            state = LevelStates.LevelPlay;
            levelSetup.EvtIntroEnd();
            levelSetup.EvtStartPlay();
            debugController.GameState((int)state);
            gameStart = true;
            auController.UnmuteSound();
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
                state = LevelStates.LevelPaused;
                debugController.GameState((int)state);
                Time.timeScale = 0;
                gameStart = false;
                auController.MuteSound();
                uiController.PauseUI();
            }
            else
            {
                state = LevelStates.LevelPlay;
                debugController.GameState((int)state);
                Time.timeScale = 1;
                gameStart = true;
                auController.UnmuteSound();
                uiController.ResetUI();
            }
        }
    }

    public void Win()
    {
        state = LevelStates.LevelEnd;
        debugController.GameState((int)state);
        auController.PlayEndLevel();
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
        if (!godMode) { 
            auController.PlayRetry();
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
        state = LevelStates.LevelStart;
        debugController.GameState((int)state);
        satisController.ResetSatisfaction();
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

    public void CoinBehaviour(int coins, bool cons)
    {
        uiController.CoinsUI(coins);
        auController.PlayCoin(cons);
    }
    public void CollectibleBehaviour(int collectible)
    {
        uiController.CollectibleUI(collectible);
        auController.PlayCollectible();
    }
    public void DoorBehaviour(){
        auController.PlayDoor();
    }

    #region Getters & Setters
    public int GetPoints(int min, int current, int max)
    {
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

    public LevelStates GetGameState()
    {
        return state;
    }

    #endregion
}
