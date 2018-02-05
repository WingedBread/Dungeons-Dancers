using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LevelManager))]
[RequireComponent(typeof(RythmManager))]
[RequireComponent(typeof(UIManager))]
public class GameManager : MonoBehaviour {
    
    [Header("Managers")]
    private PlayerManager playerManager;
    private UIManager uiManager;
    private RythmManager rythmManager;

    [Header("God Mode (?)")]
    [SerializeField]
    private bool godMode = false;

    [Header("Time Section")]
    [Header("Dungeon Timer in Seconds")]
    private float initdungeonTimer = 60f;
    [HideInInspector]
    public float dungeonTimer;
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
	void Start () {
        points = initPoints;
        feverPoints = initFeverPoints;
        DontDestroyOnLoad(this.gameObject);
        playerManager = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        playerInitPosition = playerManager.gameObject.transform.transform.position;
        uiManager = GetComponent<UIManager>();
        rythmManager = GetComponent<RythmManager>();
        spawnList = GameObject.FindGameObjectsWithTag("Spawn");
        exitList = GameObject.FindGameObjectsWithTag("Exit");
	}

    void Update(){
        dungeonTimer -= Time.deltaTime;
        if (dungeonTimer <= 0)
        {
            Dead();
            dungeonTimer = initdungeonTimer;
        }
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
            if (points <= 0) Dead();
        }
        return points;
    }

    private void FeverState(){
        if(feverPoints<= 0)
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
            playerManager.setBlock(true);
            uiManager.DeadUI();
            StartCoroutine(Reset(resetTime));
        }
    }

    public void Respawn()
    {
        if (!godMode)
        {
            playerManager.setBlock(true);
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
        playerManager.setBlock(false);
        StopCoroutine("Reset");
    }

    public int GetPoints()
    {
        return points;
    }

}
