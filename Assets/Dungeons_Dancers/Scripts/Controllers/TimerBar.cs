using UnityEngine;

public class TimerBar : MonoBehaviour {

	private GameManager gameManager;
	private float initTimerWidth;
	private float initTimer;
	private float timerBarWidth;

    private string gameManagerText = "GameManager";

    private void Awake()
    {
        gameManager = GameObject.FindWithTag(gameManagerText).GetComponent<GameManager>();
    }
    // Use this for initialization
    void Start () {
		initTimerWidth = this.transform.localScale.x;
        initTimer = gameManager.GetInitDungeonTime();
		timerBarWidth = (gameManager.GetDungeonTime()*this.transform.localScale.x)/initTimer;
	}
	
	// Update is called once per frame
	void Update () {
		timerBarWidth = (gameManager.GetDungeonTime()*initTimerWidth)/initTimer;
		this.transform.localScale = new Vector3(timerBarWidth,this.transform.localScale.y,this.transform.localScale.z);
	}
}
