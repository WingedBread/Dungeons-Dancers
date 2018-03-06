using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    [Header("Player Jump Height")]
    [SerializeField]
    private float jump = 1f;

    [Header("Flags")]
    private bool inputFlag = false;

    private bool blockPlayer = true;

    private Vector3 playerInitPos;
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
            if (!rayCollision.LeftCollision() && inputFlag && easingBool) 
            {
                Sequence s = DOTween.Sequence();
                s.Append(transform.DOMoveX(transform.position.x - speed, easingSpeedDuration, false)).OnComplete(EasingBool);
                s.Insert(0, transform.GetChild(0).GetChild(0).DOLocalMoveY(transform.position.y + jump, easingSpeedDuration/2, false));
                s.Insert(easingSpeedDuration / 2, transform.GetChild(0).GetChild(0).DOLocalMoveY(0.1f, easingSpeedDuration / 2, false));
                playerChild.rotation = Quaternion.Euler(0, -90, -45);
                inputFlag = false;
                easingBool = false;

                if (playerManager.gameManager.GetRhythmActiveInput()) playerManager.CorrectInput();
                else playerManager.IncorrectInput();
            }
        }

        //RIGHT
        else if (Equals(Input.GetAxisRaw("Horizontal"), 1f))
        {
            
            if (!rayCollision.RightCollision() && inputFlag && easingBool)
            {
                Sequence s = DOTween.Sequence();
                s.Append(transform.DOMoveX(transform.position.x + speed, easingSpeedDuration, false)).OnComplete(EasingBool);
                s.Insert(0, transform.GetChild(0).GetChild(0).DOLocalMoveY(transform.position.y + jump, easingSpeedDuration / 2, false));
                s.Insert(easingSpeedDuration / 2, transform.GetChild(0).GetChild(0).DOLocalMoveY(0.1f, easingSpeedDuration / 2, false));                
                playerChild.rotation = Quaternion.Euler(0, 90, 45);
                inputFlag = false;
                easingBool = false;

                if (playerManager.gameManager.GetRhythmActiveInput()) playerManager.CorrectInput();
                else playerManager.IncorrectInput();
            }
        }

        //DOWN
        else if (Equals(Input.GetAxisRaw("Vertical"), -1f))
        {
            
            if (!rayCollision.DownCollision() && inputFlag && easingBool)
            {
                Sequence s = DOTween.Sequence();
                s.Append(transform.DOMoveZ(transform.position.z - speed, easingSpeedDuration, false)).OnComplete(EasingBool);
                s.Insert(0, transform.GetChild(0).GetChild(0).DOLocalMoveY(transform.position.y + jump, easingSpeedDuration / 2, false));
                s.Insert(easingSpeedDuration / 2, transform.GetChild(0).GetChild(0).DOLocalMoveY(0.1f, easingSpeedDuration / 2, false));  
                playerChild.rotation = Quaternion.Euler(-45, 180, 0);
                inputFlag = false;
                easingBool = false;

                if (playerManager.gameManager.GetRhythmActiveInput()) playerManager.CorrectInput();
                else playerManager.IncorrectInput();
            }
        }

        //UP
        else if (Equals(Input.GetAxisRaw("Vertical"), 1f))
        {
            
            if (!rayCollision.UpCollision() && inputFlag && easingBool)
            {
                Sequence s = DOTween.Sequence();
                s.Append(transform.DOMoveZ(transform.position.z + speed, easingSpeedDuration, false)).OnComplete(EasingBool);
                s.Insert(0, transform.GetChild(0).GetChild(0).DOLocalMoveY(transform.position.y + jump, easingSpeedDuration / 2, false));
                s.Insert(easingSpeedDuration / 2, transform.GetChild(0).GetChild(0).DOLocalMoveY(0.1f, easingSpeedDuration / 2, false));  
                playerChild.rotation = Quaternion.Euler(45, 0, 0);
                inputFlag = false;
                easingBool = false;

                if (playerManager.gameManager.GetRhythmActiveInput()) playerManager.CorrectInput();
                else playerManager.IncorrectInput();
            }
        }
        else inputFlag = true;
    }

    void EasingBool() { easingBool = true; }

    public void SetStartRotation(int direction)
    {
        switch (direction)
        {
            case 0: //LEFT
                playerChild.rotation = Quaternion.Euler(0, -90, -45);
                break;
            case 1: //RIGHT
                playerChild.rotation = Quaternion.Euler(0, 90, 45);
                break;
            case 2: //DOWN
                playerChild.rotation = Quaternion.Euler(-45, 180, 0);
                break;
            case 3: //UP
                playerChild.rotation = Quaternion.Euler(45, 0, 0);
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
