using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DebugController : MonoBehaviour {

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

    [SerializeField]
    private GameObject statesConsole;
    [SerializeField]
    private Text playerState;
    [SerializeField]
    private Text gameState;

    private float counter;


    private bool showDebug = true;
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

        if(Input.GetKeyDown(KeyCode.C)){
            showDebug = !showDebug;
            statesConsole.SetActive(showDebug);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            showDebugState = !showDebugState;
            beatConsole.SetActive(showDebugState);
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
                case 0:
                    playerState.text = "Dancing";
                    break;
                case 1:
                    playerState.text = "Hit";
                    break;
                case 2:
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
                case 0:
                    gameState.text = "LeveStart";
                    break;
                case 1:
                    gameState.text = "LevelPaused";
                    break;
                case 2:
                    gameState.text = "LevelPlay";
                    break;
                case 3:
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
