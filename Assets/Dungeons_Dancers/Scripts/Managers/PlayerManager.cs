using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RaycastCollisions))]
public class PlayerManager : MonoBehaviour
{
    [Header("Game Manager")]
    private GameManager gameManager;
    private RaycastCollisions rayCollision;
    private RythmManager rythmManager;

    private int playerAccuracy;

    private Material mat;

    [Header("Choose Idle Return Time")]
    [SerializeField]
    private float timeIdle = 0.25f;

    [Header("Choose Player Speed")]
    [SerializeField]
    private float speed = 1f;

    [Header("Flags")]
    public bool inputFlag = false;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        rayCollision = GetComponent<RaycastCollisions>();
        rythmManager = GameObject.FindWithTag("GameManager").GetComponent<RythmManager>();
        mat = this.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        PlayerInput();
    }

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

                if (rythmManager.activePlayerInputEvent)
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

                if (rythmManager.activePlayerInputEvent)
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

                if (rythmManager.activePlayerInputEvent)
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

                if (rythmManager.activePlayerInputEvent)
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