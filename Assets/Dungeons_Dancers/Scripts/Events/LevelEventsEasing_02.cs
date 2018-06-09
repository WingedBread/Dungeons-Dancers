using UnityEngine;
using SonicBloom.Koreo;
using DG.Tweening;

public class LevelEventsEasing_02 : MonoBehaviour {
    
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
    [Header("Scale -- Move -- Rotate")]
    [SerializeField]
    private bool scaleEasing;
    [SerializeField]
    private bool moveEasing;
    [SerializeField]
    private bool rotationEasing;

    [Space]
    [Header("EASING SETTINGS:")]
    [Header("Choose Off Beat Easing")]
    [SerializeField]
    private Ease easingOffBeatList = Ease.Linear;
    [Header("Choose On Beat Easing")]
    [SerializeField]
    private Ease easingOnBeatList = Ease.Linear;

    [Space]
    [Header("SCALE SETTINGS:")]
    [Header("Choose Scale IN Easing")]
    [SerializeField]
    private Vector3 onBeatScaleVector3 = new Vector3(1, 1, 1);
    [Header("Choose Scale OUT Easing")]
    [SerializeField]
    private Vector3 offBeatScaleVector3 = new Vector3(1, 1, 1);

    [Space]
    [Header("MOVEMENT SETTINGS:")]
    [Header("Choose Position IN Easing")]
    [SerializeField]
    private Vector3 onBeatPositionVector3 = new Vector3(0, 0, 0);
    [Header("Choose Position OUT Easing")]
    [SerializeField]
    private Vector3 offBeatPositionVector3 = new Vector3(0, 0, 0);

    [Space]
    [Header("ROTATION SETTINGS:")]
    [Header("Choose Rotation IN Easing")]
    [SerializeField]
    private Vector3 onBeatRotationVector3 = new Vector3(0, 0, 0);
    [Header("Choose Rotation OUT Easing)")]
    [SerializeField]
    private Vector3 offBeatRotationVector3 = new Vector3(0, 0, 0);

    [Space]
    [Header("DURATION SETTINGS:")]
    [Header("Easing Duration IN")]
    [SerializeField]
    private float easingOnDuration = 0.05f;
    [Header("Easing Duration OUT")]
    [SerializeField]
    private float easingOffDuration = 0.1f;

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
        gameManager.levelEventsEasing2.Add(this);
        for (int i = 0; i < levelEvents.Length; i++){
            for (int w = 0; w < 21; w++)
            {
                activeLevelEvents[i,w] = false;
            }
        }
	}

	// Use this for initialization
	void Start () 
    {
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
        if (eventPlaying)
        {
            for (int w = 0; w < levelEvents.Length; w++)
            {
                if (activeLevelEvents[w, 0])
                {
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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

                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList)); ;
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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
                    if (scaleEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOScale(onBeatScaleVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOScale(offBeatScaleVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (moveEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DOLocalMove(onBeatPositionVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DOLocalMove(offBeatPositionVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }

                    if (rotationEasing)
                    {
                        Sequence s = DOTween.Sequence();
                        s.Append(transform.DORotate(onBeatRotationVector3, easingOnDuration).SetEase(easingOnBeatList));
                        s.Append(transform.DORotate(offBeatRotationVector3, easingOffDuration).SetEase(easingOffBeatList));
                        eventPlaying = false;
                    }
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