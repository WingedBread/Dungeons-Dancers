using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

[CreateAssetMenu(fileName = "LevelValues", menuName = "Tools/Level Setup")]
public class LevelSetup : ScriptableObject
{
    [Header("List LevelEvent Class")]
    [HideInInspector]
    public List<LevelEvents> eventsLevel;

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

    public void AddEvent(LevelEvents temp)
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
        Debug.Log("IntroStart");
    }
    public void EvtIntroEnd()
    {
        Debug.Log("IntroEnd");
    }
    public void EvtStartPlay()
    {
        Debug.Log("StartPlay");
    }
    public void EvtOnBeat()
    {
        Debug.Log("OnBeat");
    }
    public void EvtBeatBehaviours()
    {
        //xd de momento.
        Debug.Log("OnBeatBehaviour");
    }
    public void EvtOnCheckpoint()
    {
        Debug.Log("OnCheckpoint");
    }
    public void EvtWinLevel()
    {
        Debug.Log("WinLevel");
    }

    public void EvtGetSparkle()
    {
        Debug.Log("GetSparkle");
    }
    public void EvtGetKey()
    {
        Debug.Log("GetKey");
    }
    public void EvtTimeNearOver()
    {
        Debug.Log("TimeNearOver");
    }
    public void EvtTimeOver()
    {
        Debug.Log("TimeOver");
    }
    public void EvtStatisfactionZero()
    {
        Debug.Log("Satisfaction_0");
    }
    public void EvtStatisfactionLv1()
    {
        Debug.Log("Satisfaction_1");
    }
    public void EvtStatisfactionLv2()
    {
        Debug.Log("Satisfaction_2");
    }
    public void EvtStatisfactionLv3()
    {
        Debug.Log("Satisfaction_3");
    }
    public void EvtStatisfactionClimax()
    {
        Debug.Log("Satisfaction_Climax");
    }

    //Player Events
    public void EvtOnHit()
    {
        Debug.Log("OnHit");
    }
    public void EvtPerfectMove()
    {
        Debug.Log("Perfect Move");
    }
    public void EvtWrongMove()
    {
        Debug.Log("Wrong Move");
    }

    //Trap Events(?)
    public void EvtOnShoot()
    {
        Debug.Log("OnShoot");
    }
    #endregion
}

public class LevelEvents : ScriptableObject
{

    private Animator animator;
    private AudioClip auClip;
    private GameObject particles;

    //SAVE EDITOR VARIABLES ONLY
}