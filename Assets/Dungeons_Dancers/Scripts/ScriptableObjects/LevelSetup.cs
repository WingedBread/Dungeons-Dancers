using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour{

    public List<LevelEventEvents> levelEventsEvt;
    public List<LevelStateEvents> levelStatesEvt;
    public List<PlayerStatesEvents> playerStatesEvt;


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
        Debug.Log("OnBeatBehav");
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
    public void EvtSatisfactionZero()
    {
        Debug.Log("Satisfaction_0");
    }
    public void EvtSatisfactionLvl1()
    {
        Debug.Log("Satisfaction_1");
    }
    public void EvtSatisfactionLvl2()
    {
        Debug.Log("Satisfaction_2");
    }
    public void EvtSatisfactionLvl3()
    {
        Debug.Log("Satisfaction_3");
    }
    public void EvtSatisfactionClimax()
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
