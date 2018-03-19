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

    public void AddEvent()
    {
        LevelEvents temp = new LevelEvents();
    }

    public void DeleteEvent(){
        eventsLevel.RemoveAt(eventsLevel.Count - 1);
    }

    //Repartir por los scripts.
    #region Level Events
    private void EvtIntroStart()
    {

    }
    private void EvtIntroEnd()
    {

    }
    private void EvtStartPlay()
    {

    }
    private void EvtOnBeat()
    {

    }
    private void EvtBeatBehaviours()
    {

    }
    private void EvtOnCheckpoint()
    {

    }
    private void EvtGetCollectible()
    {

    }
    private void EvtTimeNearOver()
    {

    }
    private void EvtTimeOver()
    {

    }
    private void EvtStatisfactionLZero()
    {

    }
    private void EvtStatisfactionLv1()
    {

    }
    private void EvtStatisfactionLv2()
    {

    }
    private void EvtStatisfactionLv3()
    {

    }
    private void EvtStatisfactionClimax()
    {


    }
    #endregion

    #region Player Events
    private void EvtOnHit()
    {

    }
    #endregion
}

public class LevelEvents
{

    private Animator animator;
    private AudioClip auClip;
    private GameObject particles;

    public void SetAnimator(Animator anim)
    {
        anim = animator;
    }

    public void SetAudioClip(AudioClip audioClip)
    {
        audioClip = auClip;
    }

    public void SetParticles(GameObject particleSystem)
    {
        particleSystem = particles;
    }

    public virtual void EasingsEvent()
    {
        
    }
}
