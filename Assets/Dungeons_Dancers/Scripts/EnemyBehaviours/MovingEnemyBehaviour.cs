using System.Collections;
using UnityEngine;
using SonicBloom.Koreo;

public class MovingEnemyBehaviour : MonoBehaviour {

    private bool xAxis;
    private int direction;
	private Animator animator;
	private GameManager gameManager;
    [Header ("Choose Beat Behaviour")]
	[EventID]
    public string beatBhv;

    [Header("Choose Idle Return Time")]
    [SerializeField]
    private float timeIdle = 0.25f;

    private bool activeTrapEvent;

	private GameObject[] enemyDirection;

    private string playerBeatEvent = "PlayerBeatEvent";

    [Header("Skeleton Rotations")]
    [SerializeField]
    private Vector3 _rotationUP = new Vector3(45, 0, 0);
    [SerializeField]
    private Vector3 _rotationDOWN = new Vector3(-45, 180, 0);
    [SerializeField]
    private Vector3 _rotationLEFT = new Vector3(0, -90, -45);
    [SerializeField]
    private Vector3 _rotationRIGHT = new Vector3(0, 90, 45);

    private Quaternion rotationUP;
    private Quaternion rotationDOWN;
    private Quaternion rotationLEFT;
    private Quaternion rotationRIGHT;

	private void Start()
	{
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		animator = transform.GetChild(0).GetComponent<Animator>();
		enemyDirection = GameObject.FindGameObjectsWithTag("EnemyDirection");
		Koreographer.Instance.RegisterForEvents(beatBhv, BeatDetection);
        Koreographer.Instance.RegisterForEvents(playerBeatEvent, AnimDetector);
        rotationUP = Quaternion.Euler(_rotationUP.x, _rotationUP.y, _rotationUP.z);
        rotationDOWN = Quaternion.Euler(_rotationDOWN.x, _rotationDOWN.y, _rotationDOWN.z);
        rotationLEFT = Quaternion.Euler(_rotationLEFT.x, _rotationLEFT.y, _rotationLEFT.z);
        rotationRIGHT = Quaternion.Euler(_rotationRIGHT.x, _rotationRIGHT.y, _rotationRIGHT.z);

        
		for (int i = 0; i < enemyDirection.Length; i++)
		{
			for (int w = 0; w < enemyDirection[i].transform.childCount; w++)
			{
				enemyDirection[i].transform.GetChild(w).GetComponent<SpriteRenderer>().enabled = false;
			}

		}
	}
    
	void BeatDetection(KoreographyEvent evt)
	{
		if(gameManager.GetGameStatus())
		{
			if(!activeTrapEvent) ActiveTrap();
		}
	}

    void AnimDetector(KoreographyEvent evt)
    {
        if (gameManager.GetGameStatus())
        {
            if (gameManager.GetRhythmActiveBeat()) animator.SetBool("isRun", activeTrapEvent);
            else animator.SetBool("isRun", activeTrapEvent);
        }
    }

    void ActiveTrap()
    {
		activeTrapEvent = true;
		
        if (xAxis)
        {
            this.transform.position = new Vector3(this.transform.position.x + (direction), this.transform.position.y, this.transform.position.z);
			StartCoroutine("ReturnIdle");
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + (direction));
			StartCoroutine("ReturnIdle");
        }
    }
    void DisableTrap()
    {
		activeTrapEvent = false;
    }

    private IEnumerator ReturnIdle()
    {
		yield return new WaitForSeconds(timeIdle);
		DisableTrap();
        StopCoroutine("ReturnIdle");
    }   
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "EnemyDirection")
        {
            if (col.gameObject.transform.GetChild(0).name == "Up")
            {
                direction = 1;
                SetRotation(3);
                xAxis = false;
            }
            else if (col.gameObject.transform.GetChild(0).name == "Down")
            {
                direction = -1;
                SetRotation(2);
                xAxis = false;
            }
            else if (col.gameObject.transform.GetChild(0).name == "Right")
            {
                direction = 1;
                SetRotation(1);
                xAxis = true;
            }
            else if (col.gameObject.transform.GetChild(0).name == "Left")
            {
                direction = -1;
                SetRotation(0);
                xAxis = true;
            }
        }
    }

    void SetRotation(int Direction)
    {
        switch (Direction)
        {
            case 0: //LEFT
                transform.rotation = rotationLEFT;
                break;
            case 1: //RIGHT
                transform.rotation = rotationRIGHT;
                break;
            case 2: //DOWN
                transform.rotation = rotationDOWN;
                break;
            case 3: //UP
                transform.rotation = rotationUP;
                break;
        }
    }
}
