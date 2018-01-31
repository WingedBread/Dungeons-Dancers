using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class TrapEvent : MonoBehaviour {

    private Material mat;
    private bool activeTrapEvent;
    [Header("Choose Trap Behaviour from 1-3")]
    [SerializeField]
    private int trapBehaviour;
    [Header("Choose Idle Return Time")]
    [SerializeField]
    private float timeIdle = 0.25f;
    [Header("Choose Trap Height")]
    [SerializeField]
    private float trapHeight = 1f;

	// Use this for initialization
	void Start () 
    {
        switch (trapBehaviour)
        {
            case 1:
                Koreographer.Instance.RegisterForEvents("Trap1Event", TrapBehaviour);
                break;
            case 2:
                Koreographer.Instance.RegisterForEvents("Trap2Event", TrapBehaviour);
                break;
            case 3:
                Koreographer.Instance.RegisterForEvents("Trap3Event", TrapBehaviour);
                break;
        }
        mat = this.GetComponent<MeshRenderer>().material;
	}
	
    void TrapBehaviour(KoreographyEvent kTrapEvent)
    {
        if (!activeTrapEvent)
        {
            activeTrapEvent = true;
            mat.color = Color.white;
            this.gameObject.tag = "Trap";
            this.transform.position = new Vector3(this.transform.position.x, trapHeight, this.transform.position.z);
            StartCoroutine(ReturnIdle(timeIdle));
        }
        else
        {
            activeTrapEvent = false;
            mat.color = Color.black;
            this.gameObject.tag = "Untagged";
            this.transform.position = new Vector3(this.transform.position.x, 0f, this.transform.position.z);

        }
    }

    private IEnumerator ReturnIdle(float time)
    {
        yield return new WaitForSeconds(time);
        //Debug.Log("idle trap!");
        mat.color = Color.black;
        this.gameObject.tag = "Untagged";
        this.transform.position = new Vector3(this.transform.position.x, 0f, this.transform.position.z);

        StopCoroutine("ReturnIdle");
    }
}
