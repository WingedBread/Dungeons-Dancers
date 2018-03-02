using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTrapBehaviour : MonoBehaviour {
    
    [Header("Choose Trap Behaviour from 1-3")]
    [SerializeField]
    private int trapBehaviour;
    [Header("Choose Trap Max Height")]
    [SerializeField]
    private Vector3 trapMaxHeight;
    [Header("Choose Trap MinHeight")]
    [SerializeField]
    private Vector3 trapMinHeight;
    [Header("Choose Easing")]
    [SerializeField]
    private iTween.EaseType easingList;
    [Header("Easing Duration On Beat")]
    [SerializeField]
    private float easingOnDuration;
    [Header("Easing Duration Off Beat")]
    [SerializeField]
    private float easingOffDuration = 0.1f;

    private bool activeTrapEvent;

    private GameObject childSpikes;

	// Use this for initialization
	void Start () 
    {
        childSpikes = transform.GetChild(0).GetChild(0).gameObject;
	}
	
    public void ActiveTrap()
    {
        activeTrapEvent = true;
        this.gameObject.GetComponent<Collider>().enabled = true;
        iTween.MoveTo(childSpikes, iTween.Hash("position", trapMaxHeight, "time", easingOnDuration, "islocal", true, "easetype", easingList, "oncomplete", "ReturnIdle"));
    }

    public void DisableTrap()
    {
        activeTrapEvent = false;
        this.gameObject.GetComponent<Collider>().enabled = false;
        iTween.MoveTo(childSpikes, iTween.Hash("position", trapMinHeight, "time", easingOffDuration,"islocal", true, "easetype", easingList));
    }

    private void ReturnIdle()
    {
        this.gameObject.GetComponent<Collider>().enabled = false;
        iTween.MoveTo(childSpikes, iTween.Hash("position" ,trapMinHeight, "time", easingOffDuration,"islocal", true, "easetype", easingList));
    }

    public int GetTrapBehaviour(){
        return trapBehaviour;
    }

    public bool GetActiveTrapEvent(){
        return activeTrapEvent;
    }
}
