using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelValues", menuName = "Tools/Level Setup")]
public class LevelSetup : ScriptableObject
{
    [Header("List LevelEvent Class")]
    [HideInInspector]
    public List<LevelEventsClass> eventsLevel;

    //[EventID]
    //public string eventID;

    private void Awake()
	{
        //Koreographer.Instance.RegisterForEvents("PlayerBeatEvent", BeatBehaviour);
        //Koreographer.Instance.RegisterForEvents(eventID, BeatBehaviour);
    }

    //void BeatBehaviour(KoreographyEvent kIntroEvent)
    //{
    //    EvtOnBeat();
    //}

    //void MultipleBeatBehaviours(KoreographyEvent kIntroEvent)
    //{
    //    EvtBeatBehaviours();
    //}

    public void AddEvent(LevelEventsClass temp)
    {
        eventsLevel.Add(temp);
        Debug.Log(eventsLevel.Count);
    }

    public void DeleteEvent(){
        eventsLevel.RemoveAt(eventsLevel.Count - 1);
        Debug.Log(eventsLevel.Count);
    }


    #region Events
    //Level Events
    public void EvtIntroStart()
    {
        for (int i = 0; i < eventsLevel.Count; i++) if(eventsLevel[i].activeEvents[0]) eventsLevel[i].EvtContainer();

        Debug.Log("IntroStart");
    }
    public void EvtIntroEnd()
    {
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[1]) eventsLevel[i].EvtContainer();
        Debug.Log("IntroEnd");
    }
    public void EvtStartPlay()
    {
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[2]) eventsLevel[i].EvtContainer();
        Debug.Log("StartPlay");
    }
    public void EvtOnBeat()
    {
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[3]) eventsLevel[i].EvtContainer();
        Debug.Log("OnBeat");
    }
    public void EvtBeatBehaviours()
    {
        //xd de momento.
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[4]) eventsLevel[i].EvtContainer();
        Debug.Log("OnBeatBehaviour");
    }
    public void EvtOnCheckpoint()
    {
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[5]) eventsLevel[i].EvtContainer();
        Debug.Log("OnCheckpoint");
    }
    public void EvtWinLevel()
    {
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[6]) eventsLevel[i].EvtContainer();
        Debug.Log("WinLevel");
    }
    public void EvtGetSparkle()
    {
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[7]) eventsLevel[i].EvtContainer();
        Debug.Log("GetSparkle");
    }
    public void EvtGetKey()
    {
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[8]) eventsLevel[i].EvtContainer();
        Debug.Log("GetKey");
    }
    public void EvtTimeNearOver()
    {
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[9]) eventsLevel[i].EvtContainer();
        Debug.Log("TimeNearOver");
    }
    public void EvtTimeOver()
    {
            for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[10]) eventsLevel[i].EvtContainer();
        Debug.Log("TimeOver");
    }
    public void EvtSatisfactionZero()
    {
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[11]) eventsLevel[i].EvtContainer();
        Debug.Log("Satisfaction_0");
    }
    public void EvtSatisfactionLvl1()
    {
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[12]) eventsLevel[i].EvtContainer();
        Debug.Log("Satisfaction_1");
    }
    public void EvtSatisfactionLvl2()
    {
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[13]) eventsLevel[i].EvtContainer();
        Debug.Log("Satisfaction_2");
    }
    public void EvtSatisfactionLvl3()
    {
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[14]) eventsLevel[i].EvtContainer();
        Debug.Log("Satisfaction_3");
    }
    public void EvtSatisfactionClimax()
    {
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[15]) eventsLevel[i].EvtContainer();
        Debug.Log("Satisfaction_Climax");
    }
    //Player Events
    public void EvtOnHit()
    {
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[16]) eventsLevel[i].EvtContainer();
        Debug.Log("OnHit");
    }
    public void EvtPerfectMove()
    {
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[17]) eventsLevel[i].EvtContainer();
        Debug.Log("Perfect Move");
    }
    public void EvtWrongMove()
    {
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[18]) eventsLevel[i].EvtContainer();
        Debug.Log("Wrong Move");
    }
    //Trap Events(?)
    public void EvtOnShoot()
    {
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[19]) eventsLevel[i].EvtContainer();
        Debug.Log("OnShoot");
    }
    #endregion

}

public class LevelEventsClass : ScriptableObject
{
    public GameObject particles;
    public AudioSource audioSource;
    public LevelEvents selectedEventsEnum;
    public LevelStates selectedStatesEnum;
    public bool[] activeEvents = new bool[19];
    public bool[] activeStates = new bool[19];

	private void Awake()
	{
        for (int i = 0; i < activeEvents.Length; i++) activeEvents[i] = false;
        for (int i = 0; i < activeStates.Length; i++) activeStates[i] = false;
	}

	public void EvtContainer()
    {
        if (audioSource != null) audioSource.Play();
        //if (particles != null) particles play/active
    }

    public void CheckActiveEventsAndStates(){
        activeEvents[GetRuntimeActiveEvents()] = true;
        activeStates[GetRuntimeActiveStates()] = true;
    }

    public int GetRuntimeActiveEvents(){
        return (int)selectedEventsEnum;
    }

    public int GetRuntimeActiveStates()
    {
        return (int)selectedStatesEnum;
    }
}