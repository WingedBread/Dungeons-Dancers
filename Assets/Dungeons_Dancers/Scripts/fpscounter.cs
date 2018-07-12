using UnityEngine;
using TMPro;

public class fpscounter : MonoBehaviour {

    private TextMeshProUGUI fpsCounter;

    private int counter;

	// Use this for initialization
	void Start () {
        fpsCounter = GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
        counter = (int)(1f / Time.unscaledDeltaTime);
		fpsCounter.text = "FPS :" + counter.ToString ();
	}
}
