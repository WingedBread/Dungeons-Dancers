using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RaycastCollisions))]
public class PlayerManager : MonoBehaviour
{
    [Header("Game Manager")]
    private GameManager gameManager;

    [Header("Raycast Collisons")]
    private RaycastCollisions rayCollision;

    private Material mat;

    [Header("Animator")]
    private Animator animator;

    [Header("Idle Return Time")]
    [SerializeField]
    private float timeIdle = 0.25f;

    [Header("Player Speed")]
    [SerializeField]
    private float speed = 1f;

    [Header("Flags")]
    private bool inputFlag = false;
    private bool beatFlag = false;

    private Transform playerChild;

    [HideInInspector]
    public Vector3 playerInitPos;

    private bool blockPlayer = true;

    // Use this for initialization
    void Start()
    {
        playerChild = this.transform.GetChild(0).transform;
        animator = this.transform.GetChild(0).GetComponent<Animator>();
        playerInitPos = this.transform.position;
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        rayCollision = GetComponent<RaycastCollisions>();
        mat = playerChild.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
    }

    void Update()
    {
        if (!blockPlayer) PlayerMoveInput();

        PlayerBeatAnimatorCheck();
    }

    #region Player Input
    void PlayerMoveInput()
    {
        //LEFT
        if (Equals(Input.GetAxisRaw("Horizontal"), -1f))
        {
            if (!rayCollision.LeftCollision() && inputFlag)
            {
                this.transform.Translate(-speed, 0, 0);
                playerChild.rotation = Quaternion.Euler(0, -90, -65);
                inputFlag = false;

                if (gameManager.GetRythmActiveInput())
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
                playerChild.rotation = Quaternion.Euler(0, 90, 65);
                inputFlag = false;

                if (gameManager.GetRythmActiveInput())
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
                playerChild.rotation = Quaternion.Euler(-65, 180, 0);
                inputFlag = false;

                if (gameManager.GetRythmActiveInput())
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
            if (!rayCollision.UpCollision() && inputFlag)
            {
                this.transform.Translate(0, 0, speed);
                playerChild.rotation = Quaternion.Euler(65, 0, 0);
                inputFlag = false;

                if (gameManager.GetRythmActiveInput())
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

    void PlayerBeatAnimatorCheck()
    {
        if (gameManager.GetRythmActiveBeat())
        {
            animator.SetBool("onBeat", true);
            beatFlag = true;
            Debug.Log("hi");
        }
        else if(!gameManager.GetRythmActiveBeat() && beatFlag)
        {
            animator.SetBool("onBeat", false);
            beatFlag = false;

        }
    }

    private IEnumerator ReturnIdle(float time)
    {
        yield return new WaitForSeconds(time);
        mat.color = Color.white;
        StopCoroutine("ReturnIdle");
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Exit")
        {
            gameManager.Win();

        }
    }

    public void SetBlock(bool Block)
    {
        blockPlayer = Block;
    }

    public bool GetBlock()
    {
        return blockPlayer;
    }

    public bool GetInputFlag()
    {
        return inputFlag;
    }

    public void SetStartDirection(int direction){
        switch(direction)
        {
            case 0: //LEFT
                playerChild.rotation = Quaternion.Euler(0, -90, -65);
                break;
            case 1: //RIGHT
                playerChild.rotation = Quaternion.Euler(0, 90, 65);
                break;
            case 2: //DOWN
                playerChild.rotation = Quaternion.Euler(-65, 180, 0);
                break;
            case 3: //UP
                playerChild.rotation = Quaternion.Euler(65, 0, 0);
                break;
        }
        this.transform.position = playerInitPos;
    }
}