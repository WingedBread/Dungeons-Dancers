using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController: MonoBehaviour {

    [Header("Game Manager")]
    private GameManager gameManager;

    [Header("Dungeon Timer")]
    [SerializeField]
    private Text dungeonTimerText;
    [Header("Intro")]
    [SerializeField]
    private Text introText;

    [Header("Points")]
    public Text pointsText;

    [Header("Win/Dead/Pause")]
    [SerializeField]
    private GameObject WinGo;
    [SerializeField]
    private GameObject DeadGo;
    [SerializeField]
    private GameObject PauseGo;

    [Header("Slider")]
    public Slider pointsSlider;
    [SerializeField]
    private Text sliderText;
    [SerializeField]
    private Text sliderAccuracyText;

    #region Editor Text
    [Header("Intro Text")]
    [TextArea]
    [SerializeField]
    private string finalIntro;
    [Header("Slider Texts")]
    [TextArea]
    [SerializeField]
    private string sliderFirst;
    [TextArea]
    [SerializeField]
    private string sliderSecond, sliderThird, sliderFourth;
    [Header("Accuracy Texts")]
    [TextArea]
    [SerializeField]
    private string accuracySoon;
    [TextArea]
    [SerializeField]
    private string accuracyPerfect, accuracyLate;
    #endregion

    [Header("Intro Text Time")]
    [SerializeField]
    private float introTextTime;
    [Header("Slider Text Time")]
    [SerializeField]
    private float sliderTextTime;
    [Header("Accuracy Text Time")]
    [SerializeField]
    private float sliderAccuracyTextTime;

	// Use this for initialization
	void Start () {
        gameManager = GetComponent<GameManager>();
        pointsSlider.value = gameManager.GetPoints();
        pointsText.text = gameManager.GetPoints().ToString();
	}
    void Update(){
        dungeonTimerText.text = gameManager.GetDungeonTime().ToString("00");
    }

    public void ResetUI()
    {
        pointsText.text = gameManager.GetPoints().ToString();
        pointsSlider.value = gameManager.GetPoints();
        WinGo.SetActive(false);
        DeadGo.SetActive(false);
        PauseGo.SetActive(false);
    }

    public void WinUI()
    {
        WinGo.SetActive(true);
    }

    public void DeadUI()
    {
        DeadGo.SetActive(true);
    }

    public void PauseUI(){
        PauseGo.SetActive(true);
    }

    public void RemovePointUI(){
        pointsText.text = gameManager.GetPoints().ToString();
        pointsSlider.value = gameManager.GetPoints();
        PointsSliderChecker();
    }

    public void AddPointUI(){
        AccuracySliderCheck();
        pointsText.text = gameManager.GetPoints().ToString();
        pointsSlider.value = gameManager.GetPoints();
        PointsSliderChecker();
    }

    private void PointsSliderChecker()
    {
        switch (gameManager.GetPoints())
        {
            case 5:
                sliderText.text = sliderFirst;
                sliderText.gameObject.SetActive(true);
                StartCoroutine(DeactivatorUI(sliderText, sliderTextTime));
                break;
            case 10:
                sliderText.text = sliderSecond;
                sliderText.gameObject.SetActive(true);
                StartCoroutine(DeactivatorUI(sliderText, sliderTextTime));
                break;
            case 15:
                sliderText.text = sliderThird;
                sliderText.gameObject.SetActive(true);
                StartCoroutine(DeactivatorUI(sliderText, sliderTextTime));
                break;
            case 20:
                sliderText.text = sliderFourth;
                sliderText.gameObject.SetActive(true);
                StartCoroutine(DeactivatorUI(sliderText, sliderTextTime));
                break;
        }
    }

    private void AccuracySliderCheck()
    {
        switch(gameManager.GetRhythmAccuracy())
        {
            case 0:
                sliderAccuracyText.text = accuracySoon;
                sliderAccuracyText.gameObject.SetActive(true);
                StartCoroutine(DeactivatorUI(sliderAccuracyText, sliderAccuracyTextTime));
                break;
            case 1:
                sliderAccuracyText.text = accuracyPerfect;
                sliderAccuracyText.gameObject.SetActive(true);
                StartCoroutine(DeactivatorUI(sliderAccuracyText, sliderAccuracyTextTime));
                break;
            case 2:
                sliderAccuracyText.text = accuracyLate;
                sliderAccuracyText.gameObject.SetActive(true);
                StartCoroutine(DeactivatorUI(sliderAccuracyText, sliderAccuracyTextTime));
                break;
        }
    }

    public void IntroUICheck(int intro)
    {
        introText.gameObject.SetActive(true);
        if (intro != 0) introText.text = intro.ToString();
        else 
        { 
            introText.text = finalIntro;
            StartCoroutine(DeactivatorUI(introText, introTextTime));
        }
    }

    private IEnumerator DeactivatorUI(Text text, float time)
    {
        yield return new WaitForSeconds(time);
        text.gameObject.SetActive(false);
        StopCoroutine("DeactivatorUI");
    }
}
