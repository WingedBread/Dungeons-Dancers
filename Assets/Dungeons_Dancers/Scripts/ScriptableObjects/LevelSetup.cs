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
		Koreographer.Instance.RegisterForEvents("PlayerBeatEvent", BeatBehaviour);
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
        DebugController.eventsStaticDebug[0] = "1";
    }
    public void EvtIntroEnd()
    {
        DebugController.eventsStaticDebug[0] = "0";
        DebugController.eventsStaticDebug[1] = "1";
    }
    public void EvtStartPlay()
    {
        DebugController.eventsStaticDebug[1] = "0";
        DebugController.eventsStaticDebug[2] = "1";
    }
    public void EvtOnBeat()
    {
        DebugController.eventsStaticDebug[3] = "1";
    }
    public void EvtBeatBehaviours()
    {
        //xd de momento.
        DebugController.eventsStaticDebug[4] = "1";
    }
    public void EvtOnCheckpoint()
    {

    }
    public void EvtGetCollectible()
    {

    }
    public void EvtTimeNearOver()
    {

    }
    public void EvtTimeOver()
    {

    }
    public void EvtStatisfactionZero()
    {

    }
    public void EvtStatisfactionLv1()
    {

    }
    public void EvtStatisfactionLv2()
    {

    }
    public void EvtStatisfactionLv3()
    {

    }
    public void EvtStatisfactionClimax()
    {

    }
    //Player Events
    public void EvtOnHit()
    {

    }

    //Trap Events(?)
    public void EvtOnShoot()
    {

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