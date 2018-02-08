﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputController))]
public class PlayerManager : MonoBehaviour
{
    [Header("Game Manager")]
    [HideInInspector]
    public GameManager gameManager;

    [Header("Controllers")]
    private InputController inputController;

    [HideInInspector]
    private Material mat;

    [Header("Animator")]
    private Animator animator;

    [Header("Idle Return Time")]
    [SerializeField]
    private float timeIdle = 0.25f;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        inputController = GetComponent<InputController>();

        animator = this.transform.GetChild(0).GetComponent<Animator>();
        mat = this.transform.GetChild(0).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
    }

    void Update()
    {
        PlayerBeatAnimatorCheck();
    }

    void PlayerBeatAnimatorCheck()
    {
        if (gameManager.GetRhythmActiveBeat() && !animator.GetBool("onBeat")) animator.SetBool("onBeat", true);
        else if(!gameManager.GetRhythmActiveBeat() && animator.GetBool("onBeat")) animator.SetBool("onBeat", false);
    }

    public void CorrectInput()
    {
        mat.color = Color.green;
        gameManager.AddPoint();
        StartCoroutine(ReturnIdle());
    }

    public void IncorrectInput()
    {
        mat.color = Color.red;
        gameManager.RemovePoint();
        StartCoroutine(ReturnIdle());
    }

    public IEnumerator ReturnIdle()
    {
        yield return new WaitForSeconds(timeIdle);
        mat.color = Color.white;
        StopCoroutine("ReturnIdle");
    }

    private void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Exit":
                gameManager.Win();
                break;
            case "Trap":
                gameManager.Respawn();
                break;
                
        }
    }
    public void SetPlayerStartDirection(int direction)
    {
        inputController.SetStartDirection(direction);
    }
    public void SetBlock(bool Block)
    {
        inputController.SetInputBlock(Block);
    }

    public bool GetBlock()
    {
        return inputController.GetInputBlock();
    }

    public bool GetPlayerInputFlag()
    {
        return inputController.GetInputFlag();
    }
}