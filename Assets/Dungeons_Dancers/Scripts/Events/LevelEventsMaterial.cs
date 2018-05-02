using UnityEngine;
using SonicBloom.Koreo;
using MK.Glow;

[RequireComponent(typeof(Material))]
public class LevelEventsMaterial: MonoBehaviour {
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

    [Header("ONLY WORKS ON LEVEL EVENT = BEAT BEHAVIOUR!")]
    [EventID]
    public string beatBhv;

    [Space]
    [Header("Has Glow(?)")]
    [SerializeField]
    private bool hasGlow;
    [SerializeField]
    private float glowPowerValue;

    AudioClip[] auClip;
    [SerializeField]
    private Material material;

    [HideInInspector]
    public LevelStates managerLS;
    [HideInInspector]
    public PlayerStates managerPS;
    [HideInInspector]
    public SatisfactionStates managerSS;

    bool eventPlaying = false;

    public bool[,] activeLevelEvents = new bool [21,21];

	private void Awake()
	{
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        //gameManager.levelEventsAudios.Add(this);
        for (int i = 0; i < levelEvents.Length; i++){
            for (int w = 0; w < 21; w++)
            {
                activeLevelEvents[i,w] = false;
            }
        }
        material = this.GetComponent<MeshRenderer>().material;
    }
	// Use this for initialization
	void Start () 
    {
        if (hasGlow) material.SetFloat("__MKGlowPower", glowPowerValue);
        if (this.gameObject.GetComponent<AudioSource>() != null) audioSource = GetComponent<AudioSource>();
        for (int w = 0; w < levelEvents.Length; w++)
        {
            if (levelEvents[w] == LevelEvents.BeatBehaviour) Koreographer.Instance.RegisterForEvents(beatBhv, BeatBehaviour);
        }
        CheckActiveEvents();
	}

    private void Update()
    {
        EventContainerLevelStates();
        EventContainerPlayerStates();
        EventContainerSatisStates();
    }

	void EventContainerLevelStates()
    {
        switch (levelStates)
        {
            case LevelStates.None:
                eventPlaying = true;
                break;
            case LevelStates.LevelStart:
                if(levelStates == managerLS)eventPlaying = true;
                break;
            case LevelStates.LevelPaused:
                if (levelStates == managerLS)eventPlaying = true;
                break;
            case LevelStates.LevelPlay:
                break;
            case LevelStates.LevelEnd:
                if (levelStates == managerLS)eventPlaying = true;
                break;
        }
    }

    void EventContainerPlayerStates(){
        switch (playerStates)
        {
            case PlayerStates.None:
                break;
            case PlayerStates.Dancing:
                if(playerStates == managerPS && levelStates == LevelStates.LevelPlay)eventPlaying = true;
                break;
            case PlayerStates.Hit:
                if (playerStates == managerPS && levelStates == LevelStates.LevelPlay)eventPlaying = true;
                break;
            case PlayerStates.Succeed:
                if (playerStates == managerPS && levelStates == LevelStates.LevelPlay)eventPlaying = true;
                break;
        }
    }

    void EventContainerSatisStates(){
        switch (satisfactionStates)
        {
            case SatisfactionStates.None:
                break;
            case SatisfactionStates.SatisfactionLvl1:
                if(satisfactionStates == managerSS && levelStates == LevelStates.LevelPlay)eventPlaying = true;
                break;
            case SatisfactionStates.SatisfactionLvl2:
                if (satisfactionStates == managerSS && levelStates == LevelStates.LevelPlay)eventPlaying = true;
                break;
            case SatisfactionStates.SatisfactionLvl3:
                if (satisfactionStates == managerSS && levelStates == LevelStates.LevelPlay)eventPlaying = true;
                break;
            case SatisfactionStates.SatisfactionClimax:
                if (satisfactionStates == managerSS && levelStates == LevelStates.LevelPlay)eventPlaying = true;
                break;
        }
    }

    #region Level Events Functions
    public void IntroStart()
    {
        if (eventPlaying){
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 0])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }

    public void IntroEnd()
    {
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 1])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }
    public void StartPlay()
    {
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 2])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }
    public void OnBeat()
    {
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 3])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }
    public void BeatBehaviour(KoreographyEvent kevent)
    {
        if (eventPlaying && gameManager.GetGameStatus())
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 4])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }
    public void OnCheckpoint()
    {
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 5])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }
    public void WinLevel()
    {
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 6])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }
    public void GetSparkle()
    {
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 7])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }
    public void GetKey()
    {
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 8])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }
    public void TimeNearOver()
    {
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 9])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }
    public void TimeOver()
    {
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 10])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }
    public void SatisfactionZero()
    {
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 11])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }
    public void SatisfactionLvl1()
    {
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 12])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }

    public void SatisfactionLvl2()
    {
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 13])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }
    public void SatisfactionLvl3()
    {
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 14])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }
    public void SatisfactionClimax()
    {
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 15])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }
    public void OnHit()
    {
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 16])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }
    public void PerfectMove()
    {
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 17])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }
    public void GoodMove()
    {
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 18])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }
    public void WrongMove()
    {
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 19])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }
    public void OnShoot()
    {
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 20])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
    }
    public void Door()
    {
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 21])
                {
                    audioSource.clip = auClip[w];
                    audioSource.Play();
                    eventPlaying = false;
                }
            }
        }
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
            activeLevelEvents[w,(int)levelEvents[w]] = true;
        }
    }
}