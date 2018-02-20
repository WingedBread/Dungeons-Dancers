using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMovementController : MonoBehaviour {
    [Header("Rhythm Controller")]
    private RhythmController rhythmController;

    [Header("Choose No Beat Scale")]
    [SerializeField]
    private Vector3 ogScale = new Vector3(1, 1, 1);
    [Header("Choose Beat Scale")]
    [SerializeField]
    private Vector3 beatScale = new Vector3(1.1f, 1.1f, 1.1f);
    [Header("Choose Easing -READ DESCRIPTION-")]
    [Tooltip("-Linear:  Linear\n" +
             "\n-Exponential:  ExpoEaseOut, ExpoEaseIn, ExpoEaseInOut, ExpoEaseOutIn\n" +
             "\n-Circular:  CircEaseOut, CircEaseIn, CircEaseInOut, CircEaseOutIn\n" +
             "\n-Quad:  QuadEaseOut, QuadEaseIn, QuadEaseInOut, QuadEaseOutIn\n" +
             "\n-Sine:  SineEaseOut, SineEaseIn, SineEaseInOut, SineEaseOutIn\n" +
             "\n-Cubic:  CubicEaseOut, CubicEaseIn, CubicEaseInOut, CubicEaseOutIn\n" +
             "\n-Quartic:  QuartEaseOut, QuartEaseIn, QuartEaseInOut, QuartEaseOutIn\n" +
             "\n-Quintic:  QuintEaseOut, QuintEaseIn, QuintEaseInOut, QuintEaseOutIn\n" +
             "\n-Elastic:  ElasticEaseOut, ElasticEaseIn, ElasticEaseInOut, ElasticEaseOutIn\n" +
             "\n-Bounce:  BounceEaseOut, BounceEaseIn, BounceEaseInOut, BounceEaseOutIn\n" +
             "\n-Back:  BackEaseOut, BackEaseIn, BackEaseInOut, BackEaseOutIn")]
    [SerializeField]
    private string easingName;
    [Header("Easing Speed")]
    [SerializeField]
    private float easingSpeed;
    private bool beatflag = false;
    private int currentFrame;

	// Use this for initialization
	void Start () {
        currentFrame = 0;
        rhythmController = GameObject.FindWithTag("GameManager").GetComponent<RhythmController>();
	}

    // Update is called once per frame
    void Update()
    {
        if (rhythmController.ActivePlayerBeatEvent())
        {
            OnBeatEvent();
        }
        else if (!rhythmController.ActivePlayerBeatEvent())
        {
            NoBeatEvent();
        }
    }

    void OnBeatEvent(){
        iTween.ScaleUpdate(this.gameObject, beatScale, easingSpeed);
        //Easing.ElasticEaseIn(Time.time, this.gameObject.transform.localScale.x, beatScale.x, easingSpeed);
        //Easing.ElasticEaseIn(Time.time, this.gameObject.transform.localScale.y, beatScale.y, easingSpeed);
        //Easing.ElasticEaseIn(Time.time, this.gameObject.transform.localScale.z, beatScale.z, easingSpeed);
        //beatflag = true;
    }

    void NoBeatEvent(){
        iTween.ScaleUpdate(this.gameObject, ogScale, easingSpeed);
        //Easing.ElasticEaseIn(Time.time, beatScale.x, this.gameObject.transform.localScale.x, easingSpeed);
        //Easing.ElasticEaseIn(Time.time, beatScale.y, this.gameObject.transform.localScale.y, easingSpeed);
        //Easing.ElasticEaseIn(Time.time, beatScale.z, this.gameObject.transform.localScale.z, easingSpeed);
        //beatflag = false;
    }       
}
