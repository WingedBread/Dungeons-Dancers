using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour {

    public List<LevelEventEvents> levelEventsEvt;
    public List<LevelStateEvents> levelStatesEvt;
    public List<PlayerStatesEvents> playerStatesEvt;

    #region Events
    //Level Events
    public void EvtIntroStart()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[0])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("IntroStart_Event");
            }
        }
        Debug.Log("IntroStart");
    }
    public void EvtIntroEnd()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[1])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("IntroEnd_Event");
            }
        }
        Debug.Log("IntroEnd");
    }
    public void EvtStartPlay()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[2])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("StartPlay_Event");
            }
        }
        Debug.Log("StartPlay");
    }
    public void EvtOnBeat()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[3])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("OnBeat_Event");
            }
        }
        Debug.Log("OnBeat");
    }
    public void EvtBeatBehaviours()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[4])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("BeatBehaviours_Event");
            }
        }
        Debug.Log("BeatBehaviours");
    }
    public void EvtOnCheckpoint()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[5])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("OnCheackpoint_Event");
            }
        }
        Debug.Log("OnCheackpoint");
    }
    public void EvtWinLevel()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[6])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("WinLevel_Event");
            }
        }
        Debug.Log("WinLevel");
    }
    public void EvtGetSparkle()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[7])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("GetSparkle_Event");
            }
        }
        Debug.Log("GetSparkle");
    }
    public void EvtGetKey()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[8])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("GetKey_Event");
            }
        }
        Debug.Log("GetKey");
    }
    public void EvtTimeNearOver()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[9])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("TimeNearOver_Event");
            }
        }
        Debug.Log("TimeNearOver");
    }
    public void EvtTimeOver()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[10])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("TimeOver_Event");
            }
        }
        Debug.Log("TimeOver");
    }
    public void EvtSatisfactionZero()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[11])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("Satisfaction_0_Event");
            }
        }
        Debug.Log("Satisfaction_0");
    }
    public void EvtSatisfactionLvl1()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[12])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("Satisfaction_1_Event");
            }
        }
        Debug.Log("Satisfaction_1");
    }
    public void EvtSatisfactionLvl2()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[13])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("Satisfaction_2_Event");
            }
        }
        Debug.Log("Satisfaction_2");
    }
    public void EvtSatisfactionLvl3()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[14])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("Satisfaction_3_Event");
            }
        }
        Debug.Log("Satisfaction_3");
    }
    public void EvtSatisfactionClimax()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[15])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("Satisfaction_Climax_Event");
            }
        }
        Debug.Log("Satisfaction_Climax");
    }
    //Player Events
    public void EvtOnHit()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[16])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("OnHit_Event");
            }
        }
        Debug.Log("OnHit");
    }
    public void EvtPerfectMove()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[17])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("PerfectMove_Event");
            }
        }
        Debug.Log("PerfectMove");
    }
    public void EvtGoodMove()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[18])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("GoodMove_Event");
            }
        }
        Debug.Log("GoodMove");
    }

    public void EvtWrongMove()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[19])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("WrongMove_Event");
            }
        }
        Debug.Log("WrongMove");
    }
    //Trap Events(?)
    public void EvtOnShoot()
    {
        for (int i = 0; i < levelEventsEvt.Count; i++)
        {
            if (levelEventsEvt[i].activeEvents[20])
            {
                levelEventsEvt[i].EventContainer();
                Debug.Log("OnShoot_Event");
            }
        }
        Debug.Log("OnShoot");
    }
    #endregion

    public void LevelStatesEvts(LevelStates current)
    {
        switch(current)
        {
            case LevelStates.LevelStart:
                for (int i = 0; i < levelStatesEvt.Count; i++)
                {
                    levelStatesEvt[i].EventContainer();
                    Debug.Log("LevelStart_Event");
                }
                Debug.Log("LevelStart");
                break;
            case LevelStates.LevelPaused:
                for (int i = 0; i < levelStatesEvt.Count; i++)
                {
                    levelStatesEvt[i].EventContainer();
                    Debug.Log("LevelPaused_Event");
                }
                Debug.Log("LevelPaused");
                break;
            case LevelStates.LevelPlay:
                for (int i = 0; i < levelStatesEvt.Count; i++)
                {
                    levelStatesEvt[i].EventContainer();
                    Debug.Log("LevelPlay_Event");
                }
                Debug.Log("LevelPlay");
                break;
            case LevelStates.LevelEnd:
                for (int i = 0; i < levelStatesEvt.Count; i++)
                {
                    levelStatesEvt[i].EventContainer();
                    Debug.Log("LevelEnd_Event");
                }
                Debug.Log("LevelEnd");
                break;
        }
    }

    public void PlayerStatesEvts(PlayerStates current)
    {
        switch (current)
        {
            case PlayerStates.Dancing:
                for (int i = 0; i < playerStatesEvt.Count; i++)
                {
                    playerStatesEvt[i].EventContainer();
                    Debug.Log("PlayerDancing_Event");
                }
                Debug.Log("PlayerDancing");
                break;
            case PlayerStates.Hit:
                for (int i = 0; i < playerStatesEvt.Count; i++)
                {
                    playerStatesEvt[i].EventContainer();
                    Debug.Log("PlayerHit_Event");
                }
                Debug.Log("PlayerHit");
                break;
            case PlayerStates.Succeed:
                for (int i = 0; i < playerStatesEvt.Count; i++)
                {
                    playerStatesEvt[i].EventContainer();
                    Debug.Log("PlayerSucceed_Event");
                }
                Debug.Log("PlayerSucceed");
                break;
        }
    }
}
