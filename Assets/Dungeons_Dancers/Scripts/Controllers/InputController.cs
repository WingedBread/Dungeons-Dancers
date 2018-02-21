﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RaycastCollisions))]
public class InputController : MonoBehaviour 
{
    [Header("Player Manager")]
    private PlayerManager playerManager;

    [Header("Raycast Collisons")]
    private RaycastCollisions rayCollision;

    [Header("Player Speed")]
    [SerializeField]
    private float speed = 1f;

    [Header("Flags")]
    private bool inputFlag = false;

    private bool blockPlayer = true;

    private Vector3 playerInitPos;
    private Transform playerChild;

    [SerializeField]
    private DebugController debugController;

    [Header("Choose Easing")]
    [SerializeField]
    private iTween.EaseType easingList;
    [Header("Easing Duration")]
    [SerializeField]
    private float easingDuration;

	// Use this for initialization
	void Start () 
    {
        playerManager = GetComponent<PlayerManager>();
        rayCollision = GetComponent<RaycastCollisions>();
        playerChild = this.transform.GetChild(0).transform;
        playerInitPos = this.transform.position;

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!blockPlayer) PlayerMoveInput();
        debugController.InputPlayerDebug(inputFlag);
	}

    void PlayerMoveInput()
    {
        //LEFT
        if (Equals(Input.GetAxisRaw("Horizontal"), -1f))
        {
            if (!rayCollision.LeftCollision() && inputFlag)
            {
                //this.transform.Translate(-speed, 0, 0);
                iTween.MoveTo(gameObject, iTween.Hash("x", transform.position.x-speed, "time", easingDuration, "easetype", easingList));
                playerChild.rotation = Quaternion.Euler(0, -90, -65);
                inputFlag = false;

                if (playerManager.gameManager.GetRhythmActiveInput()) playerManager.CorrectInput();
                else playerManager.IncorrectInput();
            }
        }

        //RIGHT
        else if (Equals(Input.GetAxisRaw("Horizontal"), 1f))
        {
            if (!rayCollision.RightCollision() && inputFlag)
            {
                //this.transform.Translate(speed, 0, 0);
                Debug.Log("hi");
                iTween.MoveTo(gameObject, iTween.Hash("x", transform.position.x+speed, "time", easingDuration, "easetype", easingList));
                playerChild.rotation = Quaternion.Euler(0, 90, 65);
                inputFlag = false;

                if (playerManager.gameManager.GetRhythmActiveInput()) playerManager.CorrectInput();
                else playerManager.IncorrectInput();
            }
        }

        //DOWN
        else if (Equals(Input.GetAxisRaw("Vertical"), -1f))
        {
            if (!rayCollision.DownCollision() && inputFlag)
            {
                //this.transform.Translate(0, 0, -speed);
                iTween.MoveTo(gameObject, iTween.Hash("z", transform.position.x-speed, "time", easingDuration, "easetype", easingList));
                playerChild.rotation = Quaternion.Euler(-65, 180, 0);
                inputFlag = false;

                if (playerManager.gameManager.GetRhythmActiveInput()) playerManager.CorrectInput();
                else playerManager.IncorrectInput();
            }
        }

        //UP
        else if (Equals(Input.GetAxisRaw("Vertical"), 1f))
        {
            if (!rayCollision.UpCollision() && inputFlag)
            {
                //this.transform.Translate(0, 0, speed);
                iTween.MoveTo(gameObject, iTween.Hash("z", transform.position.x+speed, "time", easingDuration, "easetype", easingList));
                playerChild.rotation = Quaternion.Euler(65, 0, 0);
                inputFlag = false;

                if (playerManager.gameManager.GetRhythmActiveInput()) playerManager.CorrectInput();
                else playerManager.IncorrectInput();
            }
        }
        else inputFlag = true;
    }

    public void SetStartDirection(int direction)
    {
        switch (direction)
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

    public void SetInputBlock(bool Block)
    {
        blockPlayer = Block;
    }

    public bool GetInputBlock()
    {
        return blockPlayer;
    }

    public bool GetInputFlag()
    {
        return inputFlag;
    }
}
