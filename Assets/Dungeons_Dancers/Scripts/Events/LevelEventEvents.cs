using UnityEngine;
using DG.Tweening;
using SonicBloom.Koreo;

[RequireComponent(typeof(AudioSource))]
public class LevelEventEvents : MonoBehaviour {

    private AudioSource audioSource;
    private Animator animator;
    private GameObject particles;

    Ease easingListIn;
    Ease easingListOut;

    [SerializeField]
    LevelStates levelStates;
    [Header("ONLY WORKS ON LEVEL STATE = PLAY!")]
    [SerializeField]
    PlayerStates playerStates;
    [SerializeField]
    SatisfactionStates satisfactionStates;

    [SerializeField]
    LevelEvents[] levelEvents;

    [SerializeField]
    private AudioClip[] auClip;
    [Space]

    [EventID]
    public string beatBhv;

    [HideInInspector]
    public bool[] activeEvents = new bool[21];

    void Start()
    {
        if(this.gameObject.GetComponent<AudioSource>() != null) audioSource = GetComponent<AudioSource>();
        if (this.gameObject.GetComponent<Animator>() != null) animator = GetComponent<Animator>();
    }

    public void SetLevelEvents(LevelEvents state)
    {
        levelEvents[0] = state;
    }

    public void EventContainer()
    {
        audioSource.Play();
    }

    public int GetRuntimeActiveEvents()
    {
        return (int)levelEvents[0];
    }

    public void CheckActiveEvents()
    {
        activeEvents[GetRuntimeActiveEvents()] = true;
    }

    public void UncheckAllEvents()
    {
        for (int i = 0; i < activeEvents.Length; i++) activeEvents[i] = false;
    }

    public void UncheckLastEvent()
    {
        activeEvents[(int)levelEvents[0]] = false;
    }

    public int GetActiveEventsCount()
    {
        int w = 0;
        for (int i = 0; i < activeEvents.Length; i++) if (activeEvents[i] == true) w++;
        return w;
    }
}
