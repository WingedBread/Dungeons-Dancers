using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InitializeLevel))]
[RequireComponent (typeof(AudioManager))]
[RequireComponent(typeof(RythmManager))]
[RequireComponent(typeof(UIManager))]
public class GameManager : MonoBehaviour
{

    [Header("Managers")]
    private PlayerManager playerManager;
    private UIManager uiManager;
    private RythmManager rythmManager;
    private InitializeLevel initLevel;
    private AudioManager auManager;

    [Header("God Mode (?)")]
    [SerializeField]
    private bool godMode = false;

    [Header("Time Section")]
    [Header("Dungeon Timer in Seconds")]
    private float initdungeonTimer = 60f;
    private float dungeonTimer;
    [Header("Player Reset Time")]
    [SerializeField]
    private float resetTime = 1f;

    [Header("Points Section")]
    [Header("Initial Satisfaction")]
    [SerializeField]
    private int initPoints = 5;
    [Header("Maximum Satisfaction")]
    [SerializeField]
    private int maxPoints = 20;
    [Header("Fever Satisfaction")]
    [SerializeField]
    private int initFeverPoints = 1;
    [Header("Soon Satisfaction")]
    [SerializeField]
    private int soonPoints = 1;
    [Header("Perfect Satisfaction")]
    [SerializeField]
    private int perfectPoints = 2;
    [Header("Late Satisfaction")]
    [SerializeField]
    private int latePoints = 1;
    [Header("Amount of Satisfaction Removal")]
    [SerializeField]
    private int removePoints = 1;
    private int points = 5;
    private int feverPoints = 1;

    [Header("Lists")]
    private GameObject[] spawnList;
    private GameObject[] exitList;

    private Vector3 playerInitPosition;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        playerManager = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        initLevel = GetComponent<InitializeLevel>();
        uiManager = GetComponent<UIManager>();
        rythmManager = GetComponent<RythmManager>();
        auManager = GetComponent<AudioManager>();
        playerInitPosition = playerManager.gameObject.transform.transform.position;
        points = initPoints;
        feverPoints = initFeverPoints;
        spawnList = GameObject.FindGameObjectsWithTag("Spawn");
        exitList = GameObject.FindGameObjectsWithTag("Exit");
    }

    void Update()
    {
        if (initLevel.GameStart())
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

    public int AddPoint()
    {
        if (points >= maxPoints)
        {
            FeverState();
        }
        else
        {
            switch (rythmManager.GetAccuracy())
            {
                case 0:
                    points = points + soonPoints;
                    break;
                case 1:
                    points = points + perfectPoints;
                    break;
                case 2:
                    points = points + latePoints;
                    break;
            }
            if (points >= maxPoints) points = maxPoints;
            uiManager.AddPointUI();
            auManager.PointsSnapshotCheck();
        }
        return points;
    }

    public int RemovePoint()
    {
        if (points >= maxPoints)
        {
            feverPoints--;
            FeverState();
        }
        else
        {
            points = points - removePoints;
            uiManager.RemovePointUI();
            auManager.PointsSnapshotCheck();
            if (points <= 0) Dead();
        }
        return points;
    }

    private void FeverState()
    {
        if (feverPoints <= 0)
        {
            points--;
            feverPoints = initFeverPoints;
        }
    }

    public void Win()
    {
        uiManager.WinUI();
        StartCoroutine(Reset(resetTime));
    }

    public void Dead()
    {
        if (!godMode)
        {
            playerManager.SetBlock(true);
            uiManager.DeadUI();
            StartCoroutine(Reset(resetTime));
        }
    }

    public void Respawn()
    {
        if (!godMode)
        {
            playerManager.SetBlock(true);
            StartCoroutine(Reset(resetTime));
        }
    }

    private IEnumerator Reset(float time)
    {

        yield return new WaitForSeconds(time);
        //Block PlayerInput(?)
        points = initPoints;
        feverPoints = 0;
        uiManager.ResetUI();
        playerManager.gameObject.transform.transform.position = playerInitPosition;
        playerManager.SetBlock(false);
        StopCoroutine("Reset");
    }

    #region Setters&Getters
    public int GetPoints()
    {
        return points;
    }

    public bool GetPlayerBlock()
    {
        return playerManager.GetBlock();
    }

    public bool GetRythmActiveInput()
    {
        return rythmManager.activePlayerInputEvent;
    }

    public int GetRythmAccuracy(){
        return rythmManager.GetAccuracy();
    }

    public bool GetGameStatus(){
        return initLevel.GameStart();
    }

    public bool GetPlayerInputFlag(){
        return playerManager.inputFlag;
    }

    public float GetDungeonTime(){
        return dungeonTimer;  
    }
    #endregion

}
