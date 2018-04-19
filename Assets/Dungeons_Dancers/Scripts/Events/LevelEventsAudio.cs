using UnityEngine;
using SonicBloom.Koreo;
using System;

[RequireComponent(typeof(AudioSource))]
public class LevelEventsAudio : MonoBehaviour {
    private AudioSource audioSource;
    private GameManager gameManager;

    [SerializeField]
    LevelStates levelStates;
    [Header("ONLY WORKS ON LEVEL STATE = PLAY!")]
    [SerializeField]
    PlayerStates playerStates;
    [SerializeField]
    SatisfactionStates satisfactionStates;

    [SerializeField]
    LevelEvents[] levelEvents;

    [Space]
    [Header("ONLY WORKS ON LEVEL EVENT = BEAT BEHAVIOUR!")]
    [EventID]
    public string beatBhv;

    [SerializeField]
    private AudioClip[] auClip;

    [HideInInspector]
    public LevelStates managerLS;
    [HideInInspector]
    public PlayerStates managerPS;
    [HideInInspector]
    public SatisfactionStates managerSS;



    bool[][] activeLevelEvents;
    bool[] activeLevelStates = new bool[4];
    bool[] activePlayerStates = new bool[3];
    bool[] activeSatisfactionStates = new bool[4];

	private void Awake()
	{
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        gameManager.levelEventsAudios.Add(this);
	}

	// Use this for initialization
	void Start () 
    {
        if (this.gameObject.GetComponent<AudioSource>() != null) audioSource = GetComponent<AudioSource>();
        //CheckActiveEvents();
	}
	
    void EventContainerLevelStates()
    {
        switch (managerLS)
        {
            case LevelStates.None:
                Debug.Log("Nothing will happen!");
                break;
            case LevelStates.LevelStart:
                if(levelStates == managerLS)GetLevelEvent();
                break;
            case LevelStates.LevelPaused:
                if (levelStates == managerLS)GetLevelEvent();
                break;
            case LevelStates.LevelPlay:
                if (levelStates == managerLS)GetLevelEvent();
                break;
            case LevelStates.LevelEnd:
                if (levelStates == managerLS)GetLevelEvent();
                break;
        }
    }

    void EventContainerPlayerStates(){
        switch (managerPS)
        {
            case PlayerStates.None:
                Debug.Log("No PlayerState Selected!");
                break;
            case PlayerStates.Dancing:
                if(playerStates == managerPS)GetLevelEvent();
                break;
            case PlayerStates.Hit:
                if (playerStates == managerPS)GetLevelEvent();
                break;
            case PlayerStates.Succeed:
                if (playerStates == managerPS)GetLevelEvent();
                break;
        }
    }

    void EventContainerSatisStates(){
        switch (managerSS)
        {
            case SatisfactionStates.None:
                Debug.Log("Satisfaction = 0");
                break;
            case SatisfactionStates.SatisfactionLvl1:
                if(satisfactionStates == managerSS)GetLevelEvent();
                break;
            case SatisfactionStates.SatisfactionLvl2:
                if (satisfactionStates == managerSS)GetLevelEvent();
                break;
            case SatisfactionStates.SatisfactionLvl3:
                if (satisfactionStates == managerSS)GetLevelEvent();
                break;
            case SatisfactionStates.SatisfactionClimax:
                if (satisfactionStates == managerSS)GetLevelEvent();
                break;
        }
    }

    #region Level Events Functions
    public void IntroStart(){
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if(activeLevelEvents[w][1]){
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }

    public void IntroEnd()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][2])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }
    public void StartPlay()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][3])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }
    public void OnBeat()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][4])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }
    public void BeatBehaviour()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][5])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }
    public void OnCheckpoint()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][6])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }
    public void WinLevel()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][7])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }
    public void GetSparkle()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][8])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }
    public void GetKey()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][9])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }
    public void TimeNearOver()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][10])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }
    public void TimeOver()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][11])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }
    public void SatisfactionZero()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][12])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }
    public void SatisfactionLvl1()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][13])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }

    public void SatisfactionLvl2()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][14])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }
    public void SatisfactionLvl3()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][15])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }
    public void SatisfactionClimax()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][16])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }
    public void OnHit()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][17])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }
    public void PerfectMove()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][18])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }
    public void GoodMove()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][19])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }
    public void WrongMove()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][20])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }
    public void OnShoot()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][21])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }
    public void Door()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (activeLevelEvents[w][22])
            {
                audioSource.clip = auClip[w];
                audioSource.Play();
            }
        }
    }

    void GetLevelEvent(){
        //for (int w = 0; w < auClip.Length; w++)
        //{
        //    audioSource.clip = auClip[w];
        //    audioSource.Play();
        //}
        
    }
    #endregion

    public void SetLevelState(LevelStates le)
    {
        managerLS = le;
        EventContainerLevelStates();
    }
    public void SetPlayerState(PlayerStates ps)
    {
        managerPS = ps;
        EventContainerPlayerStates();
    }
    public void SetSatisfactionState(SatisfactionStates ss)
    {
        managerSS = ss;
        EventContainerSatisStates();
    }


    public void CheckActiveEvents()
    {
        for (int w = 0; w < levelEvents.Length; w++)
        {
            activeLevelEvents[w][(int)levelEvents[w]] = true;
        }
    }
}