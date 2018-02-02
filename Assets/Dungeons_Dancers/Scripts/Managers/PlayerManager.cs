using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SonicBloom.Koreo;

[RequireComponent(typeof(RaycastCollisions))]
public class PlayerManager : MonoBehaviour
{
    [Header("Game Manager")]
    private GameManager gameManager;
    private RaycastCollisions rayCollision;

    private Material mat;
    private bool activePlayerInputEvent;
    private bool activePlayerBeatEvent;

    [Header("Choose Idle Return Time")]
    [SerializeField]
    private float timeIdle = 0.25f;

    [Header("Choose Player Speed")]
    [SerializeField]
    private float speed = 1f;

    [Header("Flags")]
    private bool inputFlag = false;

    private int accuracy = 0;
    private float duration;
    private float segmentDuration;
    private float segment3, segment2, segment1;


    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        rayCollision = GetComponent<RaycastCollisions>();

        Koreographer.Instance.RegisterForEventsWithTime("PlayerInputEvent", PlayerInputBehaviour);
        Koreographer.Instance.RegisterForEvents("PlayerBeatEvent", PlayerBeatBehaviour);
        mat = this.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        PlayerInput();
    }

    #region Player Behaviours
    void PlayerInputBehaviour(KoreographyEvent kInputEvent, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
    {
        CalculateTiming(sampleTime, kInputEvent);

        if (sampleTime < segment1) accuracy = 0;
        else if (sampleTime < segment2) accuracy = 1;
        else if (sampleTime < segment3) accuracy = 2;

        if (sampleTime < kInputEvent.EndSample)
        {
            activePlayerInputEvent = true;
        }
        else
        {
            activePlayerInputEvent = false;
        }

    }

    void PlayerBeatBehaviour(KoreographyEvent kBeatEvent)
    {
        if (!activePlayerBeatEvent)
        {
            activePlayerBeatEvent = true;
            //this.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
        }
        else
        {
            activePlayerBeatEvent = false;
            //this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    void CalculateTiming(int currentTime, KoreographyEvent kCalcEvent)
    {
        
        duration = kCalcEvent.StartSample - kCalcEvent.EndSample;
        segmentDuration = duration / 3;
        segment3 = kCalcEvent.EndSample - segmentDuration;
        segment2 = segment3 - segmentDuration;
        segment1 = segment2 - segmentDuration;
    }
    #endregion

    #region Player Input
    void PlayerInput()
    {
        //LEFT
        if (Equals(Input.GetAxisRaw("Horizontal"), -1f))
        {
            if (!rayCollision.LeftCollision() && inputFlag)
            {
                this.transform.Translate(-speed, 0, 0);
                inputFlag = false;

                if (activePlayerInputEvent)
                {
                    mat.color = Color.green;
                    gameManager.AddPoint();
                    StartCoroutine(ReturnIdle(timeIdle));
                }
                else
                {
                    mat.color = Color.red;
                    gameManager.RemovePoint();
                    StartCoroutine(ReturnIdle(timeIdle));
                }
            }
        }

        //RIGHT
        else if (Equals(Input.GetAxisRaw("Horizontal"), 1f))
        {
            if (!rayCollision.RightCollision() && inputFlag)
            {
                this.transform.Translate(speed, 0, 0);
                inputFlag = false;

                if (activePlayerInputEvent)
                {
                    mat.color = Color.green;
                    gameManager.AddPoint();
                    StartCoroutine(ReturnIdle(timeIdle));
                }
                else
                {
                    mat.color = Color.red;
                    gameManager.RemovePoint();
                    StartCoroutine(ReturnIdle(timeIdle));
                }
            }
        }

        //DOWN
        else if (Equals(Input.GetAxisRaw("Vertical"), -1f))
        {
            if (!rayCollision.DownCollision() && inputFlag)
            {
                this.transform.Translate(0, 0, -speed);
                inputFlag = false;

                if (activePlayerInputEvent)
                {
                    mat.color = Color.green;
                    gameManager.AddPoint();
                    StartCoroutine(ReturnIdle(timeIdle));
                }
                else
                {
                    mat.color = Color.red;
                    gameManager.RemovePoint();
                    StartCoroutine(ReturnIdle(timeIdle));
                }
            }
        }

        //UP
        else if (Equals(Input.GetAxisRaw("Vertical"), 1f))
        {
            if (!rayCollision.UpCollision())
            {
                this.transform.Translate(0, 0, speed);
                inputFlag = false;

                if (activePlayerInputEvent)
                {
                    mat.color = Color.green;
                    gameManager.AddPoint();
                    StartCoroutine(ReturnIdle(timeIdle));
                }
                else
                {
                    mat.color = Color.red;
                    gameManager.RemovePoint();
                    StartCoroutine(ReturnIdle(timeIdle));
                }
            }
        }
        else inputFlag = true;

    }
    #endregion

    private IEnumerator ReturnIdle(float time)
    {
        yield return new WaitForSeconds(time);
        mat.color = Color.white;
        StopCoroutine("ReturnIdle");
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Trap")
        {
            gameManager.Respawn();
        }

        else if (col.gameObject.tag == "Exit")
        {
            gameManager.Win();

        }
    }
}