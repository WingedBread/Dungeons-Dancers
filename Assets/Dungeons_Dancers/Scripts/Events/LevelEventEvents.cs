using UnityEngine;
using DG.Tweening;

public class LevelEventEvents : MonoBehaviour {

    private AudioSource audioSource;
    private Animator animator;
    private GameObject particles;

    Ease easingListIn;
    Ease easingListOut;

    LevelEvents levelEvents;

    //[HideInInspector]
    public bool[] activeEvents = new bool[20];

    void Start()
    {
        if(this.gameObject.GetComponent<AudioSource>() != null) audioSource = GetComponent<AudioSource>();
        if (this.gameObject.GetComponent<Animator>() != null) animator = GetComponent<Animator>();
    }

    public void SetLevelEvents(LevelEvents state)
    {
        levelEvents = state;
    }

    public void EventContainer()
    {
        audioSource.Play();
    }

    public int GetRuntimeActiveEvents()
    {
        return (int)levelEvents;
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
        activeEvents[(int)levelEvents] = false;
    }

    public int GetActiveEventsCount()
    {
        int w = 0;
        for (int i = 0; i < activeEvents.Length; i++) if (activeEvents[i] == true) w++;
        return w;
    }
}
