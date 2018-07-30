using UnityEngine;
using SonicBloom.Koreo;
using DG.Tweening;

public class LevelEventsLightsColor : MonoBehaviour
{
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

    [Header("End Color Settings")]
    [SerializeField]
    private Color [] endColor = new Color[3];

    [Space]
    [Header("DURATION SETTINGS:")]
    [Header("Easing Color Duration")]
    [SerializeField]
    private float colorEasingDuration = 2f;

    [HideInInspector]
    public LevelStates managerLS;
    [HideInInspector]
    public PlayerStates managerPS;
    [HideInInspector]
    public SatisfactionStates managerSS;

    bool eventPlaying = false;

    public bool[,] activeLevelEvents = new bool[23, 23];

    private Light _light;

    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        gameManager.levelEventsLightsColors.Add(this);
        for (int i = 0; i < levelEvents.Length; i++)
        {
            for (int w = 0; w < 23; w++)
            {
                activeLevelEvents[i, w] = false;
            }
        }
    }

    // Use this for initialization
    void Start()
	{
        if (this.gameObject.GetComponent<Light>() != null) _light = GetComponent<Light>();

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
                if (levelStates == managerLS) eventPlaying = true;
                break;
            case LevelStates.LevelPaused:
                if (levelStates == managerLS) eventPlaying = true;
                break;
            case LevelStates.LevelPlay:
                break;
            case LevelStates.LevelEnd:
                if (levelStates == managerLS) eventPlaying = true;
                break;
        }
    }

    void EventContainerPlayerStates()
    {
        switch (playerStates)
        {
            case PlayerStates.None:
                break;
            case PlayerStates.Dancing:
                if (playerStates == managerPS && levelStates == LevelStates.LevelPlay) eventPlaying = true;
                break;
            case PlayerStates.Hit:
                if (playerStates == managerPS && levelStates == LevelStates.LevelPlay) eventPlaying = true;
                break;
            case PlayerStates.Succeed:
                if (playerStates == managerPS && levelStates == LevelStates.LevelPlay) eventPlaying = true;
                break;
        }
    }

    void EventContainerSatisStates()
    {
        switch (satisfactionStates)
        {
            case SatisfactionStates.None:
                break;
            case SatisfactionStates.SatisfactionLvl1:
                if (satisfactionStates == managerSS && levelStates == LevelStates.LevelPlay) eventPlaying = true;
                break;
            case SatisfactionStates.SatisfactionLvl2:
                if (satisfactionStates == managerSS && levelStates == LevelStates.LevelPlay) eventPlaying = true;
                break;
            case SatisfactionStates.SatisfactionLvl3:
                if (satisfactionStates == managerSS && levelStates == LevelStates.LevelPlay) eventPlaying = true;
                break;
            case SatisfactionStates.SatisfactionClimax:
                if (satisfactionStates == managerSS && levelStates == LevelStates.LevelPlay) eventPlaying = true;
                break;
        }
    }

    #region Level Events Functions
    public void IntroStart()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 0])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }

    public void IntroEnd()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 1])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void StartPlay()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 2])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void OnBeat()
    {
        if (eventPlaying && gameManager.GetGameStatus() && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 3])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void BeatBehaviour(KoreographyEvent kevent)
    {
        if (eventPlaying && gameManager.GetGameStatus() && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 4])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void OnCheckpoint()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 5])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void WinLevel()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 6])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void GetSparkle()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 7])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void GetKey()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 8])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void TimeNearOver()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 9])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void TimeOver()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 10])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void SatisfactionZero()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 11])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void SatisfactionLvl1()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 12])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }

    public void SatisfactionLvl2()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 13])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void SatisfactionLvl3()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 14])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void SatisfactionClimax()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 15])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void OnHit()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 16])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void GoodMove()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 17])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void GreatMove()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 18])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void PerfectMove()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 19])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void CorrectMove()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 20])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void WrongMove()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 21])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void OnShoot()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 22])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
                    eventPlaying = false;
                }
            }
        }
    }
    public void Door()
    {
        if (eventPlaying && (this.gameObject != null || this.gameObject.activeInHierarchy == true))
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 23])
                {
                    _light.DOColor(endColor[w], colorEasingDuration);
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
            activeLevelEvents[w, (int)levelEvents[w]] = true;
        }
    }
}