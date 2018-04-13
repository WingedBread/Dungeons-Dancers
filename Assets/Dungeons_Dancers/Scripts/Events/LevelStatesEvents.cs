using UnityEngine;
using DG.Tweening;

public class LevelStatesEvents : MonoBehaviour {
    
    private AudioSource audioSource;
    private Animator animator;
    private GameObject particles;

    Ease easingListIn;
    Ease easingListOut;

    LevelStates levelStates;

    public bool[] activeEvents = new bool[3];

	void Start()
    {
        if (this.gameObject.GetComponent<AudioSource>() != null) audioSource = GetComponent<AudioSource>();
        if (this.gameObject.GetComponent<Animator>() != null) animator = GetComponent<Animator>();
    }

    public void SetLevelState(LevelStates state)
    {
        levelStates = state;
    }

    public void EventContainer()
    {
        audioSource.Play();
    }

    public int GetRuntimeActiveEvents()
    {
        return (int)levelStates;
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
        activeEvents[(int)levelStates] = false;
    }

    public int GetActiveEventsCount()
    {
        int w = 0;
        for (int i = 0; i < activeEvents.Length; i++) if (activeEvents[i] == true) w++;
        return w;
    }
}
