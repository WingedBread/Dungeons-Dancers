using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SonicBloom.Koreo;

public class PlayerBehaviour : MonoBehaviour
{
    private Material mat;
    private bool activePlayerInputEvent;
    private bool activePlayerBeatEvent;
    [Header("Choose Idle Return Time")]
    [SerializeField]
    private float timeIdle = 0.25f;

    private GameObject[] spawnList;
    private GameObject[] exitList;

    [Header("Choose Player Speed")]
    [SerializeField]
    private float speed = 1f;

    [Header("UI")]
    [SerializeField]
    private GameObject WinUI;
    [SerializeField]
    private Text pointsText;
    private int points;

    private bool inputFlag = true;

    //Raycasts
    private RaycastHit rayHit;
    private Ray ray;
    [Header("Ray Lenght")]
    [SerializeField]
    private float rayLenght = 0.5f;


    // Use this for initialization
    void Start()
    {
        pointsText.text = points.ToString();
        spawnList = GameObject.FindGameObjectsWithTag("Spawn");
        exitList = GameObject.FindGameObjectsWithTag("Exit");

        Koreographer.Instance.RegisterForEventsWithTime("PlayerInputEvent", PlayerInputBehaviour);
        Koreographer.Instance.RegisterForEvents("PlayerBeatEvent", PlayerBeatBehaviour);
        mat = this.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        if (inputFlag) PlayerInput();
    }

    #region Player Behaviours
    void PlayerInputBehaviour(KoreographyEvent kInputEvent, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
    {

        if (sampleTime < kInputEvent.EndSample)
        {
            activePlayerInputEvent = true;
        }
        else
        {
            activePlayerInputEvent = false;
            //inputFlag = true;
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
    #endregion

    #region Player Input
    void PlayerInput()
    {
        //LEFT
        if (Equals(Input.GetAxisRaw("Horizontal"), -1f))
        {
            
            if (!LeftCollision())
            {
                this.transform.Translate(-speed, 0, 0);
                inputFlag = false;

                if (activePlayerInputEvent)
                {
                    mat.color = Color.green;
                    AddPoint();
                    StartCoroutine(ReturnIdle(timeIdle));
                }
                else
                {
                    mat.color = Color.red;
                    RemovePoint();
                    StartCoroutine(ReturnIdle(timeIdle));
                }
            }
        }

        //RIGHT
        else if (Equals(Input.GetAxisRaw("Horizontal"), 1f))
        {
            
            if (!RightCollision())
            {
                this.transform.Translate(speed, 0, 0);
                inputFlag = false;

                if (activePlayerInputEvent)
                {
                    mat.color = Color.green;
                    AddPoint();
                    StartCoroutine(ReturnIdle(timeIdle));
                }
                else
                {
                    mat.color = Color.red;
                    RemovePoint();
                    StartCoroutine(ReturnIdle(timeIdle));
                }
            }
        }

        //DOWN
        else if (Equals(Input.GetAxisRaw("Vertical"), -1f))
        {
            inputFlag = false;
            if (!DownCollision())
            {
                this.transform.Translate(0, 0, -speed);

                if (activePlayerInputEvent)
                {
                    mat.color = Color.green;
                    AddPoint();
                    StartCoroutine(ReturnIdle(timeIdle));
                }
                else
                {
                    mat.color = Color.red;
                    RemovePoint();
                    StartCoroutine(ReturnIdle(timeIdle));
                }
            }
        }

        //UP
        else if (Equals(Input.GetAxisRaw("Vertical"), 1f))
        {
            inputFlag = false;
            if (!UpCollision())
            {
                this.transform.Translate(0, 0, speed);

                if (activePlayerInputEvent)
                {
                    mat.color = Color.green;
                    AddPoint();
                    StartCoroutine(ReturnIdle(timeIdle));
                }
                else
                {
                    mat.color = Color.red;
                    RemovePoint();
                    StartCoroutine(ReturnIdle(timeIdle));
                }
            }
        }

    }
    #endregion

    private IEnumerator ReturnIdle(float time)
    {
        yield return new WaitForSeconds(time);
        mat.color = Color.white;
        inputFlag = true;
        StopCoroutine("ReturnIdle");
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Trap")
        {
            this.transform.position = new Vector3(spawnList[0].transform.position.x, spawnList[0].transform.position.y + 0.85f, spawnList[0].transform.position.z);
        }

        else if (col.gameObject.tag == "Exit")
        {
            WinUI.SetActive(true);

        }
    }

    #region Raycast Collisions

    private bool RightCollision()
    {
        Vector3 colliderCenter = new Vector3(this.GetComponent<Collider>().bounds.center.x, this.GetComponent<Collider>().bounds.center.y - 0.5f, this.GetComponent<Collider>().bounds.center.z);
        ray = new Ray(colliderCenter, Vector3.right);

        if (Physics.Raycast(ray, out rayHit, rayLenght))
        {
            Debug.Log("Collision with  " + rayHit.collider.gameObject.name);
            if (rayHit.collider.gameObject.tag == "Obstacle") return true;

        }
        return false;
    }

    private bool LeftCollision()
    {
        Vector3 colliderCenter = new Vector3(this.GetComponent<Collider>().bounds.center.x, this.GetComponent<Collider>().bounds.center.y - 0.5f, this.GetComponent<Collider>().bounds.center.z);
        ray = new Ray(colliderCenter, Vector3.right * -1);

        if (Physics.Raycast(ray, out rayHit, rayLenght))
        {
            Debug.Log("Collision with  " + rayHit.collider.gameObject.name);
            if (rayHit.collider.gameObject.tag == "Obstacle") return true;
        }
        return false;
    }

    private bool DownCollision()
    {
        Vector3 colliderCenter = new Vector3(this.GetComponent<Collider>().bounds.center.x, this.GetComponent<Collider>().bounds.center.y - 0.5f, this.GetComponent<Collider>().bounds.center.z);
        ray = new Ray(colliderCenter, Vector3.forward * -1);

        if (Physics.Raycast(ray, out rayHit, rayLenght))
        {
            Debug.Log("Collision with  " + rayHit.collider.gameObject.name);
            if (rayHit.collider.gameObject.tag == "Obstacle") return true;
        }
        return false;
    }

    private bool UpCollision()
    {
        Vector3 colliderCenter = new Vector3(this.GetComponent<Collider>().bounds.center.x, this.GetComponent<Collider>().bounds.center.y - 0.5f, this.GetComponent<Collider>().bounds.center.z);
        ray = new Ray(colliderCenter, Vector3.forward);

        if (Physics.Raycast(ray, out rayHit, rayLenght))
        {
            Debug.Log("Collision with  " + rayHit.collider.gameObject.name);
            if (rayHit.collider.gameObject.tag == "Obstacle") return true;
        }
        return false;
    }

    #endregion

    private int AddPoint()
    {
        points++;
        pointsText.text = points.ToString();
        return points;
    }

    private int RemovePoint(){
        points--;
        pointsText.text = points.ToString();
        return points;
        
    }
}