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
    [Header("Player Manager")]
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

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        playerManager = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
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
                Dead();
                dungeonTimer = initDungeonTimer;
            }
        }
        else playerManager.SetBlock(true);
    }

    public void IntroBehaviour(int intro){
        uiController.IntroUICheck(intro);
        if(GetIntroCounter() == 5){
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
                Time.timeScale = 0;
                gameStart = false;
                auController.MuteSound();
                uiController.PauseUI();
            }
            else
            {
                Time.timeScale = 1;
                gameStart = true;
                auController.UnmuteSound();
                uiController.ResetUI();
            }
        }
    }

    public void Win()
    {
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
            uiController.DeadUI();
            auController.MuteSound();
            StartCoroutine(Reset());
        }
    }

    public void Respawn()
    {
        if (!godMode) { 
            playerManager.SetPlayerStartDirection(1);
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
        playerManager.ResetPlayer();
        dungeonTimer = initDungeonTimer;
        satisController.ResetSatisfaction();
        uiController.ResetUI();
        uiController.CoinsUI(0);
        uiController.CollectibleUI(0);
        playerManager.SetPlayerStartDirection(1);
        SetIntroCounter(0);
        rhythmController.SetIntroRhythm(true);
        StopCoroutine("Reset");
    }

    public void AddPoint()
    {
        satisController.AddPoint();
        uiController.AddPointUI();
        auController.PointsSnapshotCheck();
    }

    public void RemovePoint()
    {
        satisController.RemovePoint();
        uiController.RemovePointUI();
        auController.PointsSnapshotCheck();
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
    #endregion
}
