using UnityEngine;
using UnityEngine.UI;
public class DebugController : MonoBehaviour {

    [Header("Beat")]
    [SerializeField]
    private GameObject beatConsole;
    [SerializeField]
    private Text beatPlayerTime;
    [SerializeField]
    private Text beatDuration;
    [SerializeField]
    private Text inputTime;
    [SerializeField]
    private Text inputTimeBeat;
    [SerializeField]
    private Text startPlayerSpan;
    [SerializeField]
    private Text endPlayerSpan;
    [SerializeField]
    private Text playerSpanDuration;
    [SerializeField]
    private Text fpsCounterText;

    [Header("States")]
    [SerializeField]
    private GameObject statesConsole;
    [SerializeField]
    private Text playerState;
    [SerializeField]
    private Text gameState;

    [Header("Events")]
    [SerializeField]
    private GameObject eventsConsole;
    [SerializeField]
    private Text[] eventsDebug = new Text[10];
    public static string[] eventsStaticDebug = new string[10];


    private float counter;
    private bool showDebug = true;
    private bool showDebugEvent = true;
    private bool showDebugState = true;
    bool ownFlag;

    private int pushTime;
    private int currentbeat;

    // Update is called once per frame
    void Update () {
		if (showDebug) {
			counter = (int)(1f / Time.unscaledDeltaTime);
			fpsCounterText.text = "FPS :" + counter.ToString ();
		}

        if(Input.GetKeyDown(KeyCode.J)){
            showDebug = !showDebug;
            showDebugEvent = false;
            showDebugState = false;
            beatConsole.SetActive(showDebug);
            statesConsole.SetActive(showDebugState);
            eventsConsole.SetActive(showDebugEvent);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            showDebugState = !showDebugState;
            showDebugEvent = false;
            showDebug = false;
            beatConsole.SetActive(showDebug);
            statesConsole.SetActive(showDebugState);
            eventsConsole.SetActive(showDebugEvent);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            showDebugEvent = !showDebugEvent;
            showDebug = false;
            showDebugState = false;
            beatConsole.SetActive(showDebug);
            statesConsole.SetActive(showDebugState);
            eventsConsole.SetActive(showDebugEvent);

            for (int i = 0; i < eventsDebug.Length - 1; i++)
            {
                eventsStaticDebug[i] = eventsDebug[i].text;
            }
        }
    }

    public void RhythmBeatPlayerDebug(int BeatPlayer)
    {
		if (showDebug) {
			beatPlayerTime.text = ConvertToSeconds (BeatPlayer).ToString ("F");
			currentbeat = BeatPlayer;
		}
    }
    public void RhythmBeatDurationDebug(int BeatDuration)
    {
		if (showDebug) beatDuration.text = ConvertToSeconds(BeatDuration).ToString("F");
    }
    public void RhythmStartSpanDebug(int StartSpan)
    {
		if (showDebug) startPlayerSpan.text = ConvertToSeconds(StartSpan).ToString("F");
    }
    public void RhythmEndSpanDebug(int EndSpan)
    {
			if (showDebug) endPlayerSpan.text = ConvertToSeconds(EndSpan).ToString("F");
    }
    public void RhythmDurationSpanDebug(int DurationSpan)
    {
		if (showDebug) playerSpanDuration.text = ConvertToSeconds(DurationSpan).ToString("F");
    }
    public void SetHitTime(int PushTime)
    {
		if (showDebug) pushTime = PushTime;
    }
    public void InputPlayerDebug(bool notHit)
	{
		if (showDebug) {
			if (!notHit && ownFlag) {
				inputTime.text = ConvertToSeconds (pushTime).ToString ("F");
				inputTimeBeat.text = ConvertToSeconds (currentbeat).ToString ("F");
				ownFlag = false;
			} else if (!ownFlag && notHit)
				ownFlag = true;
		}
    }

    public void PlayerState(int currentState)
    {
        if (showDebugState)
        {
            switch (currentState)
            {
                case 1:
                    playerState.text = "Dancing";
                    break;
                case 2:
                    playerState.text = "Hit";
                    break;
                case 3:
                    playerState.text = "Succeed";
                    break;
            }
        }
    }

    public void GameState(int currentState)
    {
        if (showDebugState)
        {
            switch (currentState)
            {
                case 1:
                    gameState.text = "LeveStart";
                    break;
                case 2:
                    gameState.text = "LevelPaused";
                    break;
                case 3:
                    gameState.text = "LevelPlay";
                    break;
                case 4:
                    gameState.text = "LevelEnd";
                    break;
            }
        }
    }

    float ConvertToSeconds(int sampleTime)
    {
		return ((sampleTime*0.26100591705f)/10000f);
    }
}
