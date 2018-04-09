using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

[CreateAssetMenu(fileName = "LevelValues", menuName = "Tools/Level Setup")]
public class LevelSetup : ScriptableObject
{
    [Header("List LevelEvent Class")]
    [HideInInspector]
    public List<LevelEventsClass> eventsLevel;

<<<<<<< HEAD
=======
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

>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
    public void AddEvent(LevelEventsClass temp)
    {
        eventsLevel.Add(temp);
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].DebugCall(i, true);
        Debug.Log("ArrayCount_"+eventsLevel.Count);
    }

    public void DeleteEvent(){
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].DebugCall(i, false);
        eventsLevel.RemoveAt(eventsLevel.Count-1);
        Debug.Log("ArrayCount_" +eventsLevel.Count);
    }

    #region Events
    //Level Events
    public void EvtIntroStart()
    {
<<<<<<< HEAD
        for (int i = 0; i < eventsLevel.Count; i++) if(eventsLevel[i].activeEvents[0])
        {
            eventsLevel[i].EvtContainer();
            Debug.Log("IntroStart_with_Event");
        }
=======
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtIntroStartEdit();
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
        Debug.Log("IntroStart");
    }
    public void EvtIntroEnd()
    {
<<<<<<< HEAD
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[1])
        {
            eventsLevel[i].EvtContainer();
            Debug.Log("IntroEnd_with_Event");
        }
=======
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtIntroEndEdit();
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
        Debug.Log("IntroEnd");
    }
    public void EvtStartPlay()
    {
<<<<<<< HEAD
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[2])
        {
            eventsLevel[i].EvtContainer();
            Debug.Log("StartPlay_with_Event");
        }
=======
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtStartPlayEdit();
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
        Debug.Log("StartPlay");
    }
    public void EvtOnBeat()
    {
<<<<<<< HEAD
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[3])
        {
            eventsLevel[i].EvtContainer();
            Debug.Log("OnBeat_with_Event");
        }
=======
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtOnBeatEdit();
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
        Debug.Log("OnBeat");
    }
    public void EvtBeatBehaviours()
    {
        //xd de momento.
<<<<<<< HEAD
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[4])
        {
            eventsLevel[i].EvtContainer();
            Debug.Log("BeatBehav_with_Event");
        }
        Debug.Log("OnBeatBehav");
    }
    public void EvtOnCheckpoint()
    {
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[5])
        {
            eventsLevel[i].EvtContainer();
            Debug.Log("OnCheckpoint_with_Event");
        }
=======
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtBeatBehavioursEdit();
        Debug.Log("OnBeatBehaviour");
    }
    public void EvtOnCheckpoint()
    {
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtOnCheckpointEdit();
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
        Debug.Log("OnCheckpoint");
    }
    public void EvtWinLevel()
    {
<<<<<<< HEAD
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[6])
        {
            eventsLevel[i].EvtContainer();
            Debug.Log("WinLevel_with_Event");
        }
=======
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtWinLevelEdit();
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
        Debug.Log("WinLevel");
    }
    public void EvtGetSparkle()
    {
<<<<<<< HEAD
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[7])
        {
            eventsLevel[i].EvtContainer();
            Debug.Log("GetSparkle_with_Event");
        }        
=======
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtGetSparkleEdit();
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
        Debug.Log("GetSparkle");
    }
    public void EvtGetKey()
    {
<<<<<<< HEAD
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[8])
        {
            eventsLevel[i].EvtContainer();
            Debug.Log("GetKey_with_Event");
        }        
=======
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtGetKeyEdit();
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
        Debug.Log("GetKey");
    }
    public void EvtTimeNearOver()
    {
<<<<<<< HEAD
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[9])
        {
            eventsLevel[i].EvtContainer();
            Debug.Log("TimeNearOver_with_Event");
        }        
=======
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtTimeNearOverEdit();
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
        Debug.Log("TimeNearOver");
    }
    public void EvtTimeOver()
    {
<<<<<<< HEAD
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[10])
        {
            eventsLevel[i].EvtContainer();
            Debug.Log("TimeOver_with_Event");
        }        
=======
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtTimeOverEdit();
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
        Debug.Log("TimeOver");
    }
    public void EvtSatisfactionZero()
    {
<<<<<<< HEAD
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[11])
        {
            eventsLevel[i].EvtContainer();
            Debug.Log("Satisfaction_0_with_Event");
        }        
=======
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtSatisfactionZeroEdit();
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
        Debug.Log("Satisfaction_0");
    }
    public void EvtSatisfactionLvl1()
    {
<<<<<<< HEAD
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[12])
        {
            eventsLevel[i].EvtContainer();
            Debug.Log("Satisfaction_1_with_Event");
        }        
=======
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtSatisfactionLvl1Edit();
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
        Debug.Log("Satisfaction_1");
    }
    public void EvtSatisfactionLvl2()
    {
<<<<<<< HEAD
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[13])
        {
            eventsLevel[i].EvtContainer();
            Debug.Log("Satisfaction_2_with_Event");
        }        
=======
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtSatisfactionLvl2Edit();
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
        Debug.Log("Satisfaction_2");
    }
    public void EvtSatisfactionLvl3()
    {
<<<<<<< HEAD
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[14])
        {
            eventsLevel[i].EvtContainer();
            Debug.Log("Satisfaction_3_with_Event");
        }        
=======
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtSatisfactionLvl3Edit();
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
        Debug.Log("Satisfaction_3");
    }
    public void EvtSatisfactionClimax()
    {
<<<<<<< HEAD
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[15])
        {
            eventsLevel[i].EvtContainer();
            Debug.Log("Satisfaction_Climax_with_Event");
        }        
=======
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtSatisfactionClimaxEdit();
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
        Debug.Log("Satisfaction_Climax");
    }
    //Player Events
    public void EvtOnHit()
    {
<<<<<<< HEAD
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[16])
        {
            eventsLevel[i].EvtContainer();
            Debug.Log("OnHit_with_Event");
        }        
=======
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtOnHitEdit();
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
        Debug.Log("OnHit");
    }
    public void EvtPerfectMove()
    {
<<<<<<< HEAD
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[17])
        {
            eventsLevel[i].EvtContainer();
            Debug.Log("Perfect_Move_with_Event");
        }        
=======
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtPerfectMoveEdit();
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
        Debug.Log("Perfect Move");
    }
    public void EvtWrongMove()
    {
<<<<<<< HEAD
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[18])
        {
            eventsLevel[i].EvtContainer();
            Debug.Log("WrongMove_with_Event");
        }        
=======
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtWrongMoveEdit();
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
        Debug.Log("Wrong Move");
    }
    //Trap Events(?)
    public void EvtOnShoot()
    {
<<<<<<< HEAD
        for (int i = 0; i < eventsLevel.Count; i++) if (eventsLevel[i].activeEvents[19])
        {
            eventsLevel[i].EvtContainer();
            Debug.Log("OnShoot_with_Event");
        }        
=======
        for (int i = 0; i < eventsLevel.Count; i++) eventsLevel[i].EvtOnShootEdit();
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
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
<<<<<<< HEAD
    public LevelEvents selectedEventsEnum;
    public LevelStates selectedStatesEnum;
    public PlayerStates selectedPlayerStateEnum;
    public bool[] activeEvents = new bool[19];
    public bool[] activeStates = new bool[19];

	public void EvtContainer()
=======

    #region Events
    //Level Events
    public void EvtIntroStartEdit()
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
    {
        audioSource.Play();
    }
<<<<<<< HEAD

    public void CheckActiveEventsAndStates(){
        for (int i = 0; i < activeEvents.Length; i++) activeEvents[i] = false;
        for (int i = 0; i < activeStates.Length; i++) activeStates[i] = false;

        activeEvents[GetRuntimeActiveEvents()] = true;
        activeStates[GetRuntimeActiveStates()] = true;

        for (int w = 0; w < activeStates.Length; w++) Debug.Log("activeEvent" + " " + w + "=" + activeEvents[w]);
=======
    public void EvtIntroEndEdit()
    {
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
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
<<<<<<< HEAD

    public int GetRuntimeActivePlayerState()
    {
        return (int)selectedPlayerStateEnum;
    }

    public void DebugCall(int i, bool b){
        if(b) Debug.Log("hi ArrayNumber_"+i);
        else Debug.Log("bye ArrayNumber_" + i);
    }
=======
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
>>>>>>> parent of 3569de3... Tool First Iteration AudioClip Working!
}