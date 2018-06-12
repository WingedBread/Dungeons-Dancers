using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CollectiblesController))]
[RequireComponent(typeof(InputFeedbackController))]
[RequireComponent(typeof(InputController))]
public class PlayerManager : MonoBehaviour
{
	[Header("Checkpoint Behaviour")]
    [SerializeField]
	private GameObject scriptedEvents;

	private CheckpointBehaviour checkpointBhv;
	private WinBehaviour winBhv;
	private LoseBehaviour loseBhv;

    [Header("Game Manager")]
    [HideInInspector]
    public GameManager gameManager;

    [Header("Controllers")]
    private InputController inputController;
    private CollectiblesController collectiblesController;
    private InputFeedbackController inputFeedback;

    private Material mat;

    [Header("Animator")]
    private Animator animator;

    [Header("Idle Return Time")]
    [SerializeField]
    private float timeIdle = 0.25f;

    private Vector3 spawnPosition;
    private Vector3 spawnInitPosition;
    private List<GameObject> collectibles = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
        {
            gameManager.levelEventsAudios[i].SetPlayerState(PlayerStates.Dancing);
        }
        if (gameManager.fmod_enabled)
        {
            for (int i = 0; i < gameManager.levelEventsAudios_FMOD.Count; i++)
            {
                gameManager.levelEventsAudios_FMOD[i].SetPlayerState(PlayerStates.Dancing);
            }
        }
		for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
        {
			gameManager.levelEventsMaterials[i].SetPlayerState(PlayerStates.Dancing);
        }
        for (int i = 0; i < gameManager.levelEventsEasing1.Count; i++)
        {
            gameManager.levelEventsEasing1[i].SetPlayerState(PlayerStates.Dancing);
        }
        for (int i = 0; i < gameManager.levelEventsEasing2.Count; i++)
        {
            gameManager.levelEventsEasing2[i].SetPlayerState(PlayerStates.Dancing);
        }
        for (int i = 0; i < gameManager.levelEventsEasing3.Count; i++)
        {
            gameManager.levelEventsEasing3[i].SetPlayerState(PlayerStates.Dancing);
        }
        for (int i = 0; i < gameManager.levelEventsEasing4.Count; i++)
        {
            gameManager.levelEventsEasing4[i].SetPlayerState(PlayerStates.Dancing);
        }
        inputController = GetComponent<InputController>();
        inputFeedback = GetComponent<InputFeedbackController>();
        collectiblesController = GetComponent<CollectiblesController>();
        animator = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        mat = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
		checkpointBhv = scriptedEvents.GetComponent<CheckpointBehaviour>();
		winBhv = scriptedEvents.GetComponent<WinBehaviour>();
		loseBhv = scriptedEvents.GetComponent<LoseBehaviour>();
        spawnPosition = transform.position;
        spawnInitPosition = transform.position;
		animator.SetBool("onWin", false);
    }

    void Update()
    {
        if(gameManager.GetGameStatus()) PlayerBeatAnimatorCheck();
    }

    void PlayerBeatAnimatorCheck()
    {
		if (gameManager.GetRhythmActiveBeat() && !animator.GetBool("onBeat"))
		{
			animator.SetBool("onBeat", true);
		}
		else if (!gameManager.GetRhythmActiveBeat() && animator.GetBool("onBeat"))
		{
			animator.SetBool("onBeat", false);
		}
    }

    public IEnumerator CorrectInput()
    {
        checkpointBhv.ResetCheckpoint();
        for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
        {
            gameManager.levelEventsAudios[i].GoodMove();
        }
        if (gameManager.fmod_enabled)
        {
            for (int i = 0; i < gameManager.levelEventsAudios_FMOD.Count; i++)
            {
                gameManager.levelEventsAudios_FMOD[i].GoodMove();
            }
        }
		for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
        {
			gameManager.levelEventsMaterials[i].GoodMove();
        }
        for (int i = 0; i < gameManager.levelEventsEasing1.Count; i++)
        {
            gameManager.levelEventsEasing1[i].GoodMove();
        }
        for (int i = 0; i < gameManager.levelEventsEasing2.Count; i++)
        {
            gameManager.levelEventsEasing2[i].GoodMove();
        }
        for (int i = 0; i < gameManager.levelEventsEasing3.Count; i++)
        {
            gameManager.levelEventsEasing3[i].GoodMove();
        }
        for (int i = 0; i < gameManager.levelEventsEasing4.Count; i++)
        {
            gameManager.levelEventsEasing4[i].GoodMove();
        }
        mat.color = Color.green;
        gameManager.AddPoint();
        StartCoroutine(ReturnIdle());
        while (inputController.GetEasingEnd() == false)
        {
            yield return null;
        }
        inputFeedback.CorrectFeedbackBehaviour();
    }

    public IEnumerator IncorrectInput()
    {
        checkpointBhv.ResetCheckpoint();
        for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
        {
            gameManager.levelEventsAudios[i].WrongMove();
        }
        if (gameManager.fmod_enabled)
        {
            for (int i = 0; i < gameManager.levelEventsAudios_FMOD.Count; i++)
            {
                gameManager.levelEventsAudios_FMOD[i].WrongMove();
            }
        }
		for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
        {
			gameManager.levelEventsMaterials[i].WrongMove();
        }
        for (int i = 0; i < gameManager.levelEventsEasing1.Count; i++)
        {
            gameManager.levelEventsEasing1[i].WrongMove();
        }
        for (int i = 0; i < gameManager.levelEventsEasing2.Count; i++)
        {
            gameManager.levelEventsEasing2[i].WrongMove();
        }
        for (int i = 0; i < gameManager.levelEventsEasing3.Count; i++)
        {
            gameManager.levelEventsEasing3[i].WrongMove();
        }
        for (int i = 0; i < gameManager.levelEventsEasing4.Count; i++)
        {
            gameManager.levelEventsEasing4[i].WrongMove();
        }
        mat.color = Color.red;
        gameManager.RemovePoint();
        StartCoroutine(ReturnIdle());
        while (inputController.GetEasingEnd() == false)
        {
            yield return null;
        }
        inputFeedback.IncorrectFeedbackBehaviour();
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
        for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
        {
            gameManager.levelEventsAudios[i].SetPlayerState(PlayerStates.Succeed);
        }
        if (gameManager.fmod_enabled)
        {
            for (int i = 0; i < gameManager.levelEventsAudios_FMOD.Count; i++)
            {
                gameManager.levelEventsAudios_FMOD[i].SetPlayerState(PlayerStates.Succeed);
            }
        }
		for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
        {
			gameManager.levelEventsMaterials[i].SetPlayerState(PlayerStates.Succeed);
        }
        for (int i = 0; i < gameManager.levelEventsEasing1.Count; i++)
        {
            gameManager.levelEventsEasing1[i].SetPlayerState(PlayerStates.Succeed);
        }
        for (int i = 0; i < gameManager.levelEventsEasing2.Count; i++)
        {
            gameManager.levelEventsEasing2[i].SetPlayerState(PlayerStates.Succeed);
        }
        for (int i = 0; i < gameManager.levelEventsEasing3.Count; i++)
        {
            gameManager.levelEventsEasing3[i].SetPlayerState(PlayerStates.Succeed);
        }
        for (int i = 0; i < gameManager.levelEventsEasing4.Count; i++)
        {
            gameManager.levelEventsEasing4[i].SetPlayerState(PlayerStates.Succeed);
        }
		animator.SetBool("onWin", true);
		inputController.SetRotation(2);
        gameManager.Win();
		winBhv.OnWin(this.transform);
    }

    public void Lose()
    {
        checkpointBhv.ResetCheckpoint();
        StartCoroutine(loseBhv.OnLose(this));
    }

    public void TrapBehaviour()
    {
        for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
        {
            gameManager.levelEventsAudios[i].SetPlayerState(PlayerStates.Hit);
            gameManager.levelEventsAudios[i].OnHit();
        }
        if (gameManager.fmod_enabled)
        {
            for (int i = 0; i < gameManager.levelEventsAudios_FMOD.Count; i++)
            {
                gameManager.levelEventsAudios_FMOD[i].SetPlayerState(PlayerStates.Hit);
                gameManager.levelEventsAudios_FMOD[i].OnHit();
            }
        }
		for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
        {
			gameManager.levelEventsMaterials[i].SetPlayerState(PlayerStates.Hit);
			gameManager.levelEventsMaterials[i].OnHit();
        }
        for (int i = 0; i < gameManager.levelEventsEasing1.Count; i++)
        {
            gameManager.levelEventsEasing1[i].SetPlayerState(PlayerStates.Hit);
            gameManager.levelEventsEasing1[i].OnHit();
        }
        for (int i = 0; i < gameManager.levelEventsEasing2.Count; i++)
        {
            gameManager.levelEventsEasing2[i].SetPlayerState(PlayerStates.Hit);
            gameManager.levelEventsEasing2[i].OnHit();
        }
        for (int i = 0; i < gameManager.levelEventsEasing3.Count; i++)
        {
            gameManager.levelEventsEasing3[i].SetPlayerState(PlayerStates.Hit);
            gameManager.levelEventsEasing3[i].OnHit();
        }
        for (int i = 0; i < gameManager.levelEventsEasing4.Count; i++)
        {
            gameManager.levelEventsEasing4[i].SetPlayerState(PlayerStates.Hit);
            gameManager.levelEventsEasing4[i].OnHit();
        }
        StartCoroutine(ResetPlayer(false));
    }

    public void CoinBehaviour(Collider col)
    {
        collectibles.Add(col.gameObject);
        collectiblesController.AddCoin();
        col.gameObject.SetActive(false);
        for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
        {
            gameManager.levelEventsAudios[i].GetSparkle();
        }
        if (gameManager.fmod_enabled)
        {
            for (int i = 0; i < gameManager.levelEventsAudios_FMOD.Count; i++)
            {
                gameManager.levelEventsAudios_FMOD[i].GetSparkle();
            }
        }
		for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
        {
            gameManager.levelEventsMaterials[i].GetSparkle();
        }
        for (int i = 0; i < gameManager.levelEventsEasing1.Count; i++)
        {
            gameManager.levelEventsEasing1[i].GetSparkle();
        }
        for (int i = 0; i < gameManager.levelEventsEasing2.Count; i++)
        {
            gameManager.levelEventsEasing2[i].GetSparkle();
        }
        for (int i = 0; i < gameManager.levelEventsEasing3.Count; i++)
        {
            gameManager.levelEventsEasing3[i].GetSparkle();
        }
        for (int i = 0; i < gameManager.levelEventsEasing4.Count; i++)
        {
            gameManager.levelEventsEasing4[i].GetSparkle();
        }
        gameManager.CoinBehaviour(collectiblesController.GetCoins(gameManager.GetSatisfactionFever()));
    }

    public void KeyBehaviour(Collider col)
    {
        collectibles.Add(col.gameObject);
        collectiblesController.AddKey(int.Parse(col.gameObject.name.Substring(0, 2)));
        col.gameObject.SetActive(false);
        for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
        {
            gameManager.levelEventsAudios[i].GetKey();
        }
        if (gameManager.fmod_enabled)
        {
            for (int i = 0; i < gameManager.levelEventsAudios_FMOD.Count; i++)
            {
                gameManager.levelEventsAudios_FMOD[i].GetKey();
            }
        }
		for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
        {
            gameManager.levelEventsMaterials[i].GetKey();
        }
        for (int i = 0; i < gameManager.levelEventsEasing1.Count; i++)
        {
            gameManager.levelEventsEasing1[i].GetKey();
        }
        for (int i = 0; i < gameManager.levelEventsEasing2.Count; i++)
        {
            gameManager.levelEventsEasing2[i].GetKey();
        }
        for (int i = 0; i < gameManager.levelEventsEasing3.Count; i++)
        {
            gameManager.levelEventsEasing3[i].GetKey();
        }
        for (int i = 0; i < gameManager.levelEventsEasing4.Count; i++)
        {
            gameManager.levelEventsEasing4[i].GetKey();
        }
        gameManager.CollectibleBehaviour(int.Parse(col.gameObject.name.Substring(0, 2)));
    }

    public void SpawnBehaviour(Collider col)
    {
        if (spawnPosition != col.gameObject.transform.position) 
        {
			if (gameManager.GetGameStatus())
			{
				inputController.SetRotation(2);
				animator.SetBool("onCheckpoint", true);
                StartCoroutine(checkpointBhv.OnCheckpoint(col.gameObject));
				animator.Play("Checkpoint");

				for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
				{
					gameManager.levelEventsAudios[i].OnCheckpoint();
				}
                if (gameManager.fmod_enabled)
                {
                    for (int i = 0; i < gameManager.levelEventsAudios_FMOD.Count; i++)
                    {
                        gameManager.levelEventsAudios_FMOD[i].OnCheckpoint();
                    }
                }
				for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
				{
					gameManager.levelEventsMaterials[i].OnCheckpoint();
				}
				for (int i = 0; i < gameManager.levelEventsEasing1.Count; i++)
				{
					gameManager.levelEventsEasing1[i].OnCheckpoint();
				}
				for (int i = 0; i < gameManager.levelEventsEasing2.Count; i++)
				{
					gameManager.levelEventsEasing2[i].OnCheckpoint();
				}
				for (int i = 0; i < gameManager.levelEventsEasing3.Count; i++)
				{
					gameManager.levelEventsEasing3[i].OnCheckpoint();
				}
				for (int i = 0; i < gameManager.levelEventsEasing4.Count; i++)
				{
					gameManager.levelEventsEasing4[i].OnCheckpoint();
				}
			}
        }

        spawnPosition = col.gameObject.transform.position;
    }

    public void DoorBehaviour()
    {
        collectiblesController.OpenDoor();
        gameManager.DoorBehaviour();
    }
#endregion
    public int GetCoins(){
        return collectiblesController.GetCoins(gameManager.GetSatisfactionFever());
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

    public IEnumerator ResetPlayer(bool dead)
    {
        while (inputController.GetEasingEnd() == false)
        {
            yield return null;
        }

        if (dead)
        {
            animator.SetBool("onLose", false);
            transform.position = spawnInitPosition;
            inputController.SetRotation(1);
            transform.parent.GetChild(1).position = spawnInitPosition;
            spawnPosition = spawnInitPosition;
            for (int i = 0; i < collectibles.Count; i++)
            {
                collectibles[i].SetActive(true);
            }
            collectibles.Clear();
            collectiblesController.Reset();
            for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
            {
                gameManager.levelEventsAudios[i].SetPlayerState(PlayerStates.Dancing);
            }
            if (gameManager.fmod_enabled)
            {
                for (int i = 0; i < gameManager.levelEventsAudios_FMOD.Count; i++)
                {
                    gameManager.levelEventsAudios_FMOD[i].SetPlayerState(PlayerStates.Dancing);
                }
            }
			for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
            {
                gameManager.levelEventsMaterials[i].SetPlayerState(PlayerStates.Dancing);
            }
            for (int i = 0; i < gameManager.levelEventsEasing1.Count; i++)
            {
                gameManager.levelEventsEasing1[i].SetPlayerState(PlayerStates.Dancing);
            }
            for (int i = 0; i < gameManager.levelEventsEasing2.Count; i++)
            {
                gameManager.levelEventsEasing2[i].SetPlayerState(PlayerStates.Dancing);
            }
            for (int i = 0; i < gameManager.levelEventsEasing3.Count; i++)
            {
                gameManager.levelEventsEasing3[i].SetPlayerState(PlayerStates.Dancing);
            }
            for (int i = 0; i < gameManager.levelEventsEasing4.Count; i++)
            {
                gameManager.levelEventsEasing4[i].SetPlayerState(PlayerStates.Dancing);
            }
            gameManager.GameDeadReset();
            StopCoroutine("ResetPlayer");
        }
        else
        {
            transform.position = spawnPosition;
            transform.parent.GetChild(1).position = spawnPosition;
            for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
            {
                gameManager.levelEventsAudios[i].SetPlayerState(PlayerStates.Dancing);
            }
            if (gameManager.fmod_enabled)
            {
                for (int i = 0; i < gameManager.levelEventsAudios_FMOD.Count; i++)
                {
                    gameManager.levelEventsAudios_FMOD[i].SetPlayerState(PlayerStates.Dancing);
                }
            }
			for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
            {
                gameManager.levelEventsMaterials[i].SetPlayerState(PlayerStates.Dancing);
            }
            for (int i = 0; i < gameManager.levelEventsEasing1.Count; i++)
            {
                gameManager.levelEventsEasing1[i].SetPlayerState(PlayerStates.Dancing);
            }
            for (int i = 0; i < gameManager.levelEventsEasing2.Count; i++)
            {
                gameManager.levelEventsEasing2[i].SetPlayerState(PlayerStates.Dancing);
            }
            for (int i = 0; i < gameManager.levelEventsEasing3.Count; i++)
            {
                gameManager.levelEventsEasing3[i].SetPlayerState(PlayerStates.Dancing);
            }
            for (int i = 0; i < gameManager.levelEventsEasing4.Count; i++)
            {
                gameManager.levelEventsEasing4[i].SetPlayerState(PlayerStates.Dancing);
            }
            StopCoroutine("ResetPlayer");

        }
    }
}