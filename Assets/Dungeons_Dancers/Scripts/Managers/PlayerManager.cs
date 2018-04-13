using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CollectiblesController))]
[RequireComponent(typeof(InputController))]
public class PlayerManager : MonoBehaviour
{
    [Header("Setup Level")]
    [SerializeField]
    private LevelSetup levelSetup;

    [Header("Game Manager")]
    [HideInInspector]
    public GameManager gameManager;

    [Header("Controllers")]
    private InputController inputController;
    private CollectiblesController collectiblesController;

    private Material mat;

    [Header("Animator")]
    private Animator animator;

    [Header("Idle Return Time")]
    [SerializeField]
    private float timeIdle = 0.25f;

    private Vector3 spawnPosition;
    private Vector3 spawnInitPosition;
    private List<GameObject> collectibles = new List<GameObject>();

    private PlayerStates state;

    [SerializeField]
    private DebugController debugController;

    // Use this for initialization
    void Start()
    {
        levelSetup = gameManager.GetComponent<LevelSetup>();
        state = PlayerStates.Dancing;
        levelSetup.PlayerStatesEvts(state);
        debugController.PlayerState((int)state);
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        inputController = GetComponent<InputController>();
        collectiblesController = GetComponent<CollectiblesController>();
        animator = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        mat = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
        spawnPosition = transform.position;
        spawnInitPosition = transform.position;
    }

    void Update()
    {
        if(gameManager.GetGameStatus()) PlayerBeatAnimatorCheck();
    }

    void PlayerBeatAnimatorCheck()
    {
        if (gameManager.GetRhythmActiveBeat() && !animator.GetBool("onBeat")) animator.SetBool("onBeat", true);
        else if(!gameManager.GetRhythmActiveBeat() && animator.GetBool("onBeat")) animator.SetBool("onBeat", false);
    }

    public void CorrectInput()
    {
        levelSetup.EvtGoodMove();
        mat.color = Color.green;
        gameManager.AddPoint();
        StartCoroutine(ReturnIdle());
    }

    public void IncorrectInput()
    {
        levelSetup.EvtWrongMove();
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

    #region OnTriggerMethods
    public void ExitBehaviour()
    {
        state = PlayerStates.Succeed;
        levelSetup.PlayerStatesEvts(state);
        debugController.PlayerState((int)state);
        gameManager.Win();
    }

    public void TrapBehaviour()
    {
        state = PlayerStates.Hit;
        levelSetup.PlayerStatesEvts(state);
        levelSetup.EvtOnHit();
        debugController.PlayerState((int)state);
        StartCoroutine(ResetPlayer(false));
    }

    public void CoinBehaviour(Collider col)
    {
        collectibles.Add(col.gameObject);
        collectiblesController.AddCoin();
        col.gameObject.SetActive(false);
        levelSetup.EvtGetSparkle();
        gameManager.CoinBehaviour(collectiblesController.GetCoins(gameManager.GetSatisfactionFever()));
    }

    public void KeyBehaviour(Collider col)
    {
        collectibles.Add(col.gameObject);
        collectiblesController.AddKey(int.Parse(col.gameObject.name.Substring(0, 2)));
        col.gameObject.SetActive(false);
        levelSetup.EvtGetKey();
        gameManager.CollectibleBehaviour(int.Parse(col.gameObject.name.Substring(0, 2)));
    }

    public void SpawnBehaviour(Collider col)
    {
        if (spawnPosition != col.gameObject.transform.position) levelSetup.EvtOnCheckpoint();
        spawnPosition = col.gameObject.transform.position;
    }

    public void DoorBehaviour()
    {
        collectiblesController.OpenDoor();
        gameManager.DoorBehaviour();
    }
#endregion

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

    public IEnumerator ResetPlayer(bool dead)
    {
        while (inputController.GetEasingEnd() == false)
        {
            yield return null;
        }

        if (dead)
        {
            transform.position = spawnInitPosition;
            transform.parent.GetChild(1).position = spawnInitPosition;
            spawnPosition = spawnInitPosition;
            inputController.SetRotation(1);
            for (int i = 0; i < collectibles.Count; i++)
            {
                collectibles[i].SetActive(true);
            }
            collectibles.Clear();
            collectiblesController.Reset();
            state = PlayerStates.Dancing;
            levelSetup.PlayerStatesEvts(state);
            debugController.PlayerState((int)state);
            StopCoroutine("ResetPlayer");
        }
        else
        {
            gameManager.Respawn();
            transform.position = spawnPosition;
            transform.parent.GetChild(1).position = spawnPosition;
            if (spawnPosition != spawnInitPosition) inputController.SetRotation(2);
            else inputController.SetRotation(1);
            state = PlayerStates.Dancing;
            levelSetup.PlayerStatesEvts(state);
            debugController.PlayerState((int)state);
            StopCoroutine("ResetPlayer");

        }
    }
}