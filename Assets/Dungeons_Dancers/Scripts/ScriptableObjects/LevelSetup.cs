using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

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

    void BeatBehaviour(KoreographyEvent kIntroEvent)
    {
        EvtOnBeat();
    }

    void MultipleBeatBehaviours(KoreographyEvent kIntroEvent)
    {
        EvtBeatBehaviours();
    }

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
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtIntroStartEdit();
        Debug.Log("IntroStart");
    }
    public void EvtIntroEnd()
    {
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtIntroEndEdit();
        Debug.Log("IntroEnd");
    }
    public void EvtStartPlay()
    {
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtStartPlayEdit();
        Debug.Log("StartPlay");
    }
    public void EvtOnBeat()
    {
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtOnBeatEdit();
        Debug.Log("OnBeat");
    }
    public void EvtBeatBehaviours()
    {
        //xd de momento.
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtBeatBehavioursEdit();
        Debug.Log("OnBeatBehaviour");
    }
    public void EvtOnCheckpoint()
    {
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtOnCheckpointEdit();
        Debug.Log("OnCheckpoint");
    }
    public void EvtWinLevel()
    {
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtWinLevelEdit();
        Debug.Log("WinLevel");
    }
    public void EvtGetSparkle()
    {
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtGetSparkleEdit();
        Debug.Log("GetSparkle");
    }
    public void EvtGetKey()
    {
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtGetKeyEdit();
        Debug.Log("GetKey");
    }
    public void EvtTimeNearOver()
    {
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtTimeNearOverEdit();
        Debug.Log("TimeNearOver");
    }
    public void EvtTimeOver()
    {
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtTimeOverEdit();
        Debug.Log("TimeOver");
    }
    public void EvtSatisfactionZero()
    {
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtSatisfactionZeroEdit();
        Debug.Log("Satisfaction_0");
    }
    public void EvtSatisfactionLvl1()
    {
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtSatisfactionLvl1Edit();
        Debug.Log("Satisfaction_1");
    }
    public void EvtSatisfactionLvl2()
    {
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtSatisfactionLvl2Edit();
        Debug.Log("Satisfaction_2");
    }
    public void EvtSatisfactionLvl3()
    {
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtSatisfactionLvl3Edit();
        Debug.Log("Satisfaction_3");
    }
    public void EvtSatisfactionClimax()
    {
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtSatisfactionClimaxEdit();
        Debug.Log("Satisfaction_Climax");
    }
    //Player Events
    public void EvtOnHit()
    {
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtOnHitEdit();
        Debug.Log("OnHit");
    }
    public void EvtPerfectMove()
    {
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtPerfectMoveEdit();
        Debug.Log("Perfect Move");
    }
    public void EvtWrongMove()
    {
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtWrongMoveEdit();
        Debug.Log("Wrong Move");
    }
    //Trap Events(?)
    public void EvtOnShoot()
    {
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtOnShootEdit();
        Debug.Log("OnShoot");
    }
    #endregion
}

public class LevelEventsClass : ScriptableObject
{
    public Animator animator;
    public AudioClip auClip;
    public GameObject particles;
    public AudioSource audioSource;

    #region Events
    //Level Events
    public void EvtIntroStartEdit()
    {
        audioSource.Play();
    }
    public void EvtIntroEndEdit()
    {
    }
    public void EvtStartPlayEdit()
    {
    }
    public void EvtOnBeatEdit()
    {
    }
    public void EvtBeatBehavioursEdit()
    {
    }
    public void EvtOnCheckpointEdit()
    {
    }
    public void EvtWinLevelEdit()
    {
    }
    public void EvtGetSparkleEdit()
    {
    }
    public void EvtGetKeyEdit()
    {
    }
    public void EvtTimeNearOverEdit()
    {
    }
    public void EvtTimeOverEdit()
    {
    }
    public void EvtSatisfactionZeroEdit()
    {
    }
    public void EvtSatisfactionLvl1Edit()
    {
    }
    public void EvtSatisfactionLvl2Edit()
    {
    }
    public void EvtSatisfactionLvl3Edit()
    {
    }
    public void EvtSatisfactionClimaxEdit()
    {
    }
    //Player Events
    public void EvtOnHitEdit()
    {
    }
    public void EvtPerfectMoveEdit()
    {
    }
    public void EvtWrongMoveEdit()
    {
    }
    //Trap Events(?)
    public void EvtOnShootEdit()
    {
    }
    #endregion
}