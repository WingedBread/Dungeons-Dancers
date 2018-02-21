using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTrapBehaviour : MonoBehaviour {
    
    private Material mat;

    [Header("Choose Trap Behaviour from 1-3")]
    [SerializeField]
    private int trapBehaviour;
    [Header("Choose Trap Max Height")]
    [SerializeField]
    private float trapMaxHeight = 1f;
    [Header("Choose Trap MinHeight")]
    [SerializeField]
    private float trapMinHeight = 0.05f;
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

	// Use this for initialization
	void Start () 
    {
        mat = this.GetComponent<MeshRenderer>().material;
	}
	
    public void ActiveTrap()
    {
        activeTrapEvent = true;
        mat.color = Color.white;
        this.gameObject.GetComponent<Collider>().enabled = true;
        iTween.MoveTo(gameObject, iTween.Hash("y", trapMaxHeight, "time", easingOnDuration, "easetype", easingList));
        StartCoroutine(ReturnIdle(easingOffDuration));
    }

    public void DisableTrap()
    {
        activeTrapEvent = false;
        mat.color = Color.black;
        this.gameObject.GetComponent<Collider>().enabled = false;
        iTween.MoveTo(gameObject, iTween.Hash("y", trapMinHeight, "time", easingOffDuration, "easetype", easingList));
    }

    private IEnumerator ReturnIdle(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.GetComponent<Collider>().enabled = false;
        mat.color = Color.black;
        iTween.MoveTo(gameObject, iTween.Hash("y", trapMinHeight, "time", easingOffDuration, "easetype", easingList));

        StopCoroutine("ReturnIdle");
    }

    public int GetTrapBehaviour(){
        return trapBehaviour;
    }

    public bool GetActiveTrapEvent(){
        return activeTrapEvent;
    }
}
