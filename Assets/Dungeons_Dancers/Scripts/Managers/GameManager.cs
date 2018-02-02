using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Choose Initial Points")]
    [SerializeField]
    private int initPoints = 5;
    private int points = 5;

    [Header("Lists")]
    private GameObject[] spawnList;
    private GameObject[] exitList;

    [Header("Choose Player Reset Time")]
    [SerializeField]
    private float resetTime = 1f;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        playerManager = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        uiManager = GetComponent<UIManager>();
        rythmManager = GetComponent<RythmManager>();
        spawnList = GameObject.FindGameObjectsWithTag("Spawn");
        exitList = GameObject.FindGameObjectsWithTag("Exit");
	}

    public int AddPoint()
    {
        switch (rythmManager.GetAccuracy())
        {
            case 0:
                points++;
                break;
            case 1:
                points = points + 2;
                break;
            case 2:
                points++;
                break;
        }
        uiManager.AddPointUI();
        return points;
    }

    public int AddMultiplePoints(int Points){
        points = points + Points;
        return points;
    }

    public int RemovePoint()
    {
        points--;
        uiManager.RemovePointUI();
        Dead();
        return points;
    }


    public void Win()
    {
        uiManager.WinUI();
        StartCoroutine(Reset(resetTime));
    }

    public void Dead()
    {
        if (points <= 0 && !godMode)
        {
            this.transform.position = new Vector3(spawnList[0].transform.position.x, spawnList[0].transform.position.y + 0.85f, spawnList[0].transform.position.z);
            uiManager.DeadUI();
            StartCoroutine(Reset(resetTime));
        }
    }

    public void Respawn()
    {
        if(!godMode) playerManager.gameObject.transform.position = new Vector3(spawnList[0].transform.position.x, spawnList[0].transform.position.y + 0.85f, spawnList[0].transform.position.z);
    }

    private IEnumerator Reset(float time)
    {
        yield return new WaitForSeconds(time);

        //Block PlayerInput(?)
        points = initPoints;
        uiManager.ResetUI();
        playerManager.gameObject.transform.position = new Vector3(spawnList[0].transform.position.x, spawnList[0].transform.position.y + 0.85f, spawnList[0].transform.position.z);
        StopCoroutine("Reset");
    }

    public int GetPoints()
    {
        return points;
    }

}
