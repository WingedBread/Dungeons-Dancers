#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LevelValues", menuName = "Tools/Level Setup")]
public class LevelSetup : ScriptableObject 
{
    [Header("List LevelEvent Class")]
    [HideInInspector]
    public List<LevelEvents> eventsLevel;

    public void AddEvent(LevelEvents temp)
    {
        eventsLevel.Add(temp);
        Debug.Log(eventsLevel.Count);
    }

    public void DeleteEvent(){
        eventsLevel.RemoveAt(eventsLevel.Count - 1);
        Debug.Log(eventsLevel.Count);
    }

    #region Level Events
    public void EvtIntroStart()
    {

    }
    public void EvtIntroEnd()
    {

    }
    public void EvtStartPlay()
    {

    }
    public void EvtOnBeat()
    {
        //x
    }
    public void EvtBeatBehaviours()
    {
        //x --choose and execute eventonbeat on editor.
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
    public void EvtStatisfactionLZero()
    {
        //x
    }
    public void EvtStatisfactionLv1()
    {
        //x
    }
    public void EvtStatisfactionLv2()
    {
        //x
    }
    public void EvtStatisfactionLv3()
    {
        //x
    }
    public void EvtStatisfactionClimax()
    {
        //x
    }
    #endregion

    #region Player Events
    public void EvtOnHit()
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
#endif