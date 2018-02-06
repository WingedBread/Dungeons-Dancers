using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class TrapBehaviour : MonoBehaviour {
    
    [Header("Game Manager")]
    private GameManager gameManager;

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
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        switch (trapBehaviour)
        {
            case 1:
                Koreographer.Instance.RegisterForEvents("Trap1Event", TrapBeatBehaviour);
                break;
            case 2:
                Koreographer.Instance.RegisterForEvents("Trap2Event", TrapBeatBehaviour);
                break;
            case 3:
                Koreographer.Instance.RegisterForEvents("Trap3Event", TrapBeatBehaviour);
                break;
        }
        mat = this.GetComponent<MeshRenderer>().material;
	}
	
    void TrapBeatBehaviour(KoreographyEvent kTrapEvent)
    {
        if (gameManager.GetGameStatus())
        {
            if (!activeTrapEvent)
            {
                activeTrapEvent = true;
                mat.color = Color.white;
                this.gameObject.GetComponent<Collider>().enabled = activeTrapEvent;
                this.transform.position = new Vector3(this.transform.position.x, trapHeight, this.transform.position.z);
                StartCoroutine(ReturnIdle(timeIdle));
            }
            else
            {
                activeTrapEvent = false;
                mat.color = Color.black;
                this.gameObject.GetComponent<Collider>().enabled = activeTrapEvent;
                this.transform.position = new Vector3(this.transform.position.x, 0f, this.transform.position.z);

            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            gameManager.Respawn();
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
