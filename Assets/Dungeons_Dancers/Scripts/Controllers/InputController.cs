using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InputController : MonoBehaviour 
{
    [Header("Player Manager")]
    private PlayerManager playerManager;

    [Header("Raycast Collisons")]
    private PlayerCollisions playerCollision;

    [Header("Player Rotations")]
    [SerializeField]
    private Vector3 _rotationUP = new Vector3(45, 0, 0);
    [SerializeField]
    private Vector3 _rotationDOWN = new Vector3(-45, 180, 0);
    [SerializeField]
    private Vector3 _rotationLEFT = new Vector3(0, -90, -45);
    [SerializeField]
    private Vector3 _rotationRIGHT = new Vector3(0, 90, 45);

    [Header("Player Speed")]
    [SerializeField]
    private float speed = 1f;
    [Header("Player Jump Height")]
    [SerializeField]
    private float jump = 1f;

    [Header("Flags")]
    private bool inputFlag = false;

    private bool blockPlayer = true;

    private Transform playerChild;

    [SerializeField]
    private DebugController debugController;

    [Header("Choose Walk Easing")]
    [SerializeField]
    private Ease easingSpeedList;

    [Header("Easing Walk Duration")]
    [SerializeField]
    private float easingSpeedDuration;

    [Header("Choose Jump Easing")]
    [SerializeField]
    private Ease easingJumpList;

    private Vector3[] verticalEasingCurve;

    private bool easingBool = true;

    private int playerDirection;

    private Quaternion rotationUP;
    private Quaternion rotationDOWN;
    private Quaternion rotationLEFT;
    private Quaternion rotationRIGHT;

    [SerializeField]
    private bool debugEnable = false;

    string[] directions = {"Horizontal", "Vertical"};


	// Use this for initialization
	void Start () 
    {
        playerManager = GetComponent<PlayerManager>();
        playerCollision = transform.parent.GetChild(1).GetComponent<PlayerCollisions>();
        playerChild = this.transform.GetChild(0).transform;
        rotationUP = Quaternion.Euler(_rotationUP.x, _rotationUP.y, _rotationUP.z);
        rotationDOWN = Quaternion.Euler(_rotationDOWN.x, _rotationDOWN.y, _rotationDOWN.z);
        rotationLEFT = Quaternion.Euler(_rotationLEFT.x, _rotationLEFT.y, _rotationLEFT.z);
        rotationRIGHT = Quaternion.Euler(_rotationRIGHT.x, _rotationRIGHT.y, _rotationRIGHT.z);

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!blockPlayer) PlayerMoveInput();
        if(debugEnable) debugController.InputPlayerDebug(inputFlag);
	}

    void PlayerMoveInput()
    {
        //LEFT
        if (Equals(Input.GetAxisRaw(directions[0]), -1f))
        {
            if (!playerCollision.LeftCollision() && inputFlag && easingBool) 
            {
                Sequence s = DOTween.Sequence();
                s.Append(transform.DOMoveX(transform.position.x - speed, easingSpeedDuration, false)).OnComplete(EasingBool);
                s.Insert(0, transform.GetChild(0).GetChild(0).DOLocalMoveY(transform.position.y + jump, easingSpeedDuration/2, false));
                s.Insert(easingSpeedDuration / 2, transform.GetChild(0).GetChild(0).DOLocalMoveY(0.1f, easingSpeedDuration / 2, false));
                transform.parent.GetChild(1).transform.position = new Vector3(transform.parent.GetChild(1).transform.position.x - speed, transform.parent.GetChild(1).transform.position.y, transform.parent.GetChild(1).transform.position.z);
                playerChild.rotation = rotationLEFT;
                playerDirection = 0;
                inputFlag = false;
                easingBool = false;

                if (playerManager.gameManager.GetRhythmActiveInput()) StartCoroutine(playerManager.CorrectInput());
                else StartCoroutine(playerManager.IncorrectInput());
            }
        }

        //RIGHT
        else if (Equals(Input.GetAxisRaw(directions[0]), 1f))
        {
            
            if (!playerCollision.RightCollision() && inputFlag && easingBool)
            {
                Sequence s = DOTween.Sequence();
                s.Append(transform.DOMoveX(transform.position.x + speed, easingSpeedDuration, false)).OnComplete(EasingBool);
                s.Insert(0, transform.GetChild(0).GetChild(0).DOLocalMoveY(transform.position.y + jump, easingSpeedDuration / 2, false));
                s.Insert(easingSpeedDuration / 2, transform.GetChild(0).GetChild(0).DOLocalMoveY(0.1f, easingSpeedDuration / 2, false));
                transform.parent.GetChild(1).transform.position = new Vector3(transform.parent.GetChild(1).transform.position.x + speed, transform.parent.GetChild(1).transform.position.y, transform.parent.GetChild(1).transform.position.z);

                playerChild.rotation = rotationRIGHT;
                playerDirection = 1;
                inputFlag = false;
                easingBool = false;

                if (playerManager.gameManager.GetRhythmActiveInput()) StartCoroutine(playerManager.CorrectInput());
                else StartCoroutine(playerManager.IncorrectInput());
            }
        }

        //DOWN
        else if (Equals(Input.GetAxisRaw(directions[1]), -1f))
        {
            
            if (!playerCollision.DownCollision() && inputFlag && easingBool)
            {
                Sequence s = DOTween.Sequence();
                s.Append(transform.DOMoveZ(transform.position.z - speed, easingSpeedDuration, false)).OnComplete(EasingBool);
                s.Insert(0, transform.GetChild(0).GetChild(0).DOLocalMoveY(transform.position.y + jump, easingSpeedDuration / 2, false));
                s.Insert(easingSpeedDuration / 2, transform.GetChild(0).GetChild(0).DOLocalMoveY(0.1f, easingSpeedDuration / 2, false));
                transform.parent.GetChild(1).transform.position = new Vector3(transform.parent.GetChild(1).transform.position.x, transform.parent.GetChild(1).transform.position.y, transform.parent.GetChild(1).transform.position.z - speed);
                playerChild.rotation = rotationDOWN;
                playerDirection = 2;
                inputFlag = false;
                easingBool = false;

                if (playerManager.gameManager.GetRhythmActiveInput()) StartCoroutine(playerManager.CorrectInput());
                else StartCoroutine(playerManager.IncorrectInput());
            }
        }

        //UP
        else if (Equals(Input.GetAxisRaw(directions[1]), 1f))
        {
            
            if (!playerCollision.UpCollision() && inputFlag && easingBool)
            {
                Sequence s = DOTween.Sequence();
                s.Append(transform.DOMoveZ(transform.position.z + speed, easingSpeedDuration, false)).OnComplete(EasingBool);
                s.Insert(0, transform.GetChild(0).GetChild(0).DOLocalMoveY(transform.position.y + jump, easingSpeedDuration / 2, false));
                s.Insert(easingSpeedDuration / 2, transform.GetChild(0).GetChild(0).DOLocalMoveY(0.1f, easingSpeedDuration / 2, false));
                transform.parent.GetChild(1).transform.position = new Vector3(transform.parent.GetChild(1).transform.position.x, transform.parent.GetChild(1).transform.position.y, transform.parent.GetChild(1).transform.position.z + speed);
                playerChild.rotation = rotationUP;
                playerDirection = 3;
                inputFlag = false;
                easingBool = false;

                if (playerManager.gameManager.GetRhythmActiveInput()) StartCoroutine(playerManager.CorrectInput());
                else StartCoroutine(playerManager.IncorrectInput());
            }
        }
        else inputFlag = true;
    }

    void EasingBool() { easingBool = true; }

    public void SetRotation(int direction)
    {
        switch (direction)
        {
            case 0: //LEFT
                playerChild.rotation = rotationLEFT;
                break;
            case 1: //RIGHT
                playerChild.rotation = rotationRIGHT;
                break;
            case 2: //DOWN
                playerChild.rotation = rotationDOWN;
                break;
            case 3: //UP
                playerChild.rotation = rotationUP;
                break;
        }
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

    public int GetDirection()
    {
        return playerDirection;
    }

    public bool GetEasingEnd(){
        return easingBool;
    }
}
