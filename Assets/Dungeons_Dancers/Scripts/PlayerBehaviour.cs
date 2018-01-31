using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class PlayerBehaviour : MonoBehaviour
{
    private Material mat;
    private bool activePlayerInputEvent;
    private bool activePlayerBeatEvent;
    [Header("Choose Idle Return Time")]
    [SerializeField]
    private float timeIdle = 0.25f;
    [Header("Choose Player Jump Height")]
    [SerializeField]
    private float playerJumpHeight = 1.5f;

    private GameObject[] spawnList;
    private GameObject[] exitList;

    [Header("Choose Player Speed")]
    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    private GameObject WinUI;

    private bool inputFlag = true;

    // Use this for initialization
    void Start()
    {
        spawnList = GameObject.FindGameObjectsWithTag("Spawn");
        exitList = GameObject.FindGameObjectsWithTag("Exit");

        Koreographer.Instance.RegisterForEventsWithTime("PlayerInputEvent", PlayerInputBehaviour);
        Koreographer.Instance.RegisterForEvents("PlayerBeatEvent", PlayerBeatBehaviour);
        mat = this.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        if(inputFlag)PlayerInput();
    }

    void PlayerInputBehaviour(KoreographyEvent kInputEvent, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
    {
        
        if (sampleTime < kInputEvent.EndSample)
        {
            activePlayerInputEvent = true;
        }
        else
        {
            activePlayerInputEvent = false;
            inputFlag = true;
        } 

    }

    void PlayerBeatBehaviour(KoreographyEvent kBeatEvent)
    {
        if(!activePlayerBeatEvent){
            activePlayerBeatEvent = true;
            //this.transform.position = new Vector3(this.transform.position.x, playerJumpHeight, this.transform.position.z);
        }
        else{
            activePlayerBeatEvent = false;
            //this.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);
        }
    }

    void PlayerInput()
    {
        //LEFT
        if (Equals(Input.GetAxisRaw("Horizontal"), -1f))
        {
            inputFlag = false;
            if (activePlayerInputEvent)
            {
                
                this.transform.Translate(-speed, 0, 0); 
                mat.color = Color.green;
                StartCoroutine(ReturnIdle(timeIdle));

            }
            else
            {
                //Debug.Log("incorrect!");
                mat.color = Color.red;
                StartCoroutine(ReturnIdle(timeIdle));
            }
        }

        //RIGHT
        else if(Equals(Input.GetAxisRaw("Horizontal"), 1f))
        {
            inputFlag = false;
            if (activePlayerInputEvent)
            {
                this.transform.Translate(speed, 0, 0);
                //Debug.Log("perfect!");
                mat.color = Color.green;
                StartCoroutine(ReturnIdle(timeIdle));

            }
            else
            {
                //Debug.Log("incorrect!");
                mat.color = Color.red;
                StartCoroutine(ReturnIdle(timeIdle));
            }
        }

        //DOWN
        else if (Equals(Input.GetAxisRaw("Vertical"), -1f))
        {
            inputFlag = false;
            if (activePlayerInputEvent)
            {
                this.transform.Translate(0, 0,-speed);
                //Debug.Log("perfect!");
                mat.color = Color.green;
                StartCoroutine(ReturnIdle(timeIdle));

            }
            else
            {
                //Debug.Log("incorrect!");
                mat.color = Color.red;
                StartCoroutine(ReturnIdle(timeIdle));
            }
        }

        //UP
        else if (Equals(Input.GetAxisRaw("Vertical"), 1f))
        {
            inputFlag = false;
            if (activePlayerInputEvent)
            {
                this.transform.Translate(0, 0,speed);
                //Debug.Log("perfect!");
                mat.color = Color.green;
                StartCoroutine(ReturnIdle(timeIdle));
                inputFlag = false;

            }
            else
            {
                //Debug.Log("incorrect!");
                mat.color = Color.red;
                StartCoroutine(ReturnIdle(timeIdle));
            }
        }

    }

    private IEnumerator ReturnIdle(float time)
    {
        yield return new WaitForSeconds(time);
        //Debug.Log("idle player!");
        mat.color = Color.white;
        StopCoroutine("ReturnIdle");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Trap")
        {
            this.transform.position = new Vector3(spawnList[0].transform.position.x,spawnList[0].transform.position.y+0.85f, spawnList[0].transform.position.z);
        }

        else if(collision.gameObject.tag == "Exit"){
            WinUI.SetActive(true);

        }
    }
}