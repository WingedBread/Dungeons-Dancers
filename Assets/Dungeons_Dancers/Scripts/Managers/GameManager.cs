using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SatisfactionController))]
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

    [Header("God Mode (?)")]
    [SerializeField]
    private bool godMode = false;

    private bool gameStart = false;
    [Header("Intro Time")]
    [SerializeField]
    private float introTime = 4f;

    [Header("Time Section")]
    [Header("Dungeon Timer in Seconds")]
    private float initdungeonTimer = 60f;
    private float dungeonTimer;
    [Header("Player Reset Time")]
    [SerializeField]
    private float resetTime = 1f;

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
        dungeonTimer = initdungeonTimer;
        StartCoroutine(IntroCoroutine());
    }

    void Update()
    {
        if (gameStart)
        {
            if (playerManager.GetBlock() == true) playerManager.SetBlock(false);
            dungeonTimer -= Time.deltaTime;
            if (dungeonTimer <= 0)
            {
                Dead();
                dungeonTimer = initdungeonTimer;
            }
        }
        else playerManager.SetBlock(true);
    }

    private IEnumerator IntroCoroutine()
    {
        yield return new WaitForSeconds(introTime / 4);
        uiController.IntroUICheck(3);
        yield return new WaitForSeconds(introTime / 4);
        uiController.IntroUICheck(2);
        yield return new WaitForSeconds(introTime / 4);
        uiController.IntroUICheck(1);
        yield return new WaitForSeconds(introTime / 4);
        uiController.IntroUICheck(0);
        gameStart = true;
        rhythmController.SetRhythm(true);
        StopCoroutine("IntroCoroutine");
    }


    public void Win()
    {
        gameStart = false;
        rhythmController.SetRhythm(false);
        uiController.WinUI();
        StartCoroutine(Reset(resetTime));
    }

    public void Dead()
    {
        if (!godMode)
        {
            gameStart = false;
            rhythmController.SetRhythm(false);
            uiController.DeadUI();
            StartCoroutine(Reset(resetTime));
        }
    }

    public void Respawn()
    {
        if (!godMode) playerManager.SetPlayerStartDirection(1);
    }

    private IEnumerator Reset(float time)
    {

        yield return new WaitForSeconds(time);
        dungeonTimer = initdungeonTimer;
        satisController.ResetSatisfaction();
        uiController.ResetUI();
        playerManager.SetPlayerStartDirection(1);
        StartCoroutine(IntroCoroutine());
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

    #region Getters & Setters
    public int GetPoints()
    {
        return satisController.GetSatisfactionPoints();
    }
    public bool GetPlayerBlock()
    {
        return playerManager.GetBlock();
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

    public void SetPlayerBlock(bool block){
        playerManager.SetBlock(block);
    }
    #endregion
}
