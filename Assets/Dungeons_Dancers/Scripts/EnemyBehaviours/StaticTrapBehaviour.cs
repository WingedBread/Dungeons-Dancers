using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTrapBehaviour : MonoBehaviour {
    
    private Material mat;

    [Header("Choose Trap Behaviour from 1-3")]
    [SerializeField]
    private int trapBehaviour;
    [Header("Choose Idle Return Time")]
    [SerializeField]
    private float timeIdle = 0.25f;
    [Header("Choose Trap Height")]
    [SerializeField]
    private float trapHeight = 1f;

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
        this.transform.position = new Vector3(this.transform.position.x, trapHeight, this.transform.position.z);
        StartCoroutine(ReturnIdle(timeIdle));
    }

    public void DisableTrap()
    {
        activeTrapEvent = false;
        mat.color = Color.black;
        this.gameObject.GetComponent<Collider>().enabled = false;
        this.transform.position = new Vector3(this.transform.position.x, 0f, this.transform.position.z);
    }

    private IEnumerator ReturnIdle(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.GetComponent<Collider>().enabled = false;
        mat.color = Color.black;
        this.transform.position = new Vector3(this.transform.position.x, 0f, this.transform.position.z);

        StopCoroutine("ReturnIdle");
    }

    public int GetTrapBehaviour(){
        return trapBehaviour;
    }

    public bool GetActiveTrapEvent(){
        return activeTrapEvent;
    }
}
