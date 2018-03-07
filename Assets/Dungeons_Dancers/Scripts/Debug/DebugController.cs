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

    private float counter;


    private bool showDebug = true;
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
            beatConsole.SetActive((showDebug));
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

    float ConvertToSeconds(int sampleTime)
    {
		return ((sampleTime*0.26100591705f)/10000f);
    }
}
