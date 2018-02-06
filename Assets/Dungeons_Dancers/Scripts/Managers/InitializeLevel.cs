using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    [Header("Game Manager")]
    private GameManager gameManager;

    [SerializeField]
    private bool gameStart = false;
    [Header("Intro Time")]
    [SerializeField]
    private float introTime = 4f;

    void Start(){
        gameManager = GetComponent<GameManager>();
        StartCoroutine(IntroCoroutine());
    }

    private IEnumerator IntroCoroutine()
    {
        yield return new WaitForSeconds(introTime);

        gameStart = true;
        StopCoroutine("IntroCoroutine");
    }

    public bool GameStart(){
        return gameStart;
    }
}
