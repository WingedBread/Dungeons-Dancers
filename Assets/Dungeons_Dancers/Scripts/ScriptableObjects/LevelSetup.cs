using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LevelValues", menuName = "Tools/Level Setup")]
public class LevelSetup : ScriptableObject 
{
    [Header("List LevelEvent Class")]
    [HideInInspector]
    public List<LevelEvent> eventsLevel;

    public void AddEvent()
    {
        LevelEvent temp = new LevelEvent();
    }


}

public class LevelEvent
{

    public virtual void AnimationEvent(Animator animator){
        
    }

    public virtual void SoundEvent(AudioClip auClip)
    {

    }
    public virtual void ParticlesEvent(GameObject particles)
    {

    }

    public virtual void EasingsEvent(){
        
    }
}
