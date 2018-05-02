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
    [SerializeField]
    private Text pointsText;
    [Header("Coins")]
    [SerializeField]
    private Text coinsText;
    [Header("Keys")]
    [SerializeField]
    private Image[] keysImages = new Image[2];

    [Header("Climax Number")]
    [SerializeField]
    private Text climaxNumber;

    [Header("Climax Locks")]
    [SerializeField]
    private Image climaxUnlock;


    [Header("Separators")]
    [SerializeField]
    private GameObject barEnd;
    [SerializeField]
    private GameObject[] separatorsObj = new GameObject[3];
    private float barwidth;

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
    [SerializeField]
    private string finalIntro;
    [Header("Slider Texts")]
    [SerializeField]
    private string sliderFirst;
    [SerializeField]
    private string sliderSecond;
    [SerializeField]
    private string sliderThird;
    [SerializeField]
    private string sliderFourth;
    [Header("Accuracy Texts")]
    [SerializeField]
    private string accuracySoon;
    [SerializeField]
    private string accuracyPerfect;
    [SerializeField]
    private string accuracyLate;
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
        pointsSlider.minValue = gameManager.GetPoints(1, 0, 0);
        pointsSlider.maxValue = gameManager.GetPoints(0, 0, 1);
        pointsSlider.value = gameManager.GetPoints(0,1,0);
        pointsText.text = gameManager.GetPoints(0,1,0).ToString();
        barwidth = barEnd.transform.parent.transform.parent.GetComponent<RectTransform>().rect.width;
        barEnd.transform.localPosition  = new Vector3(barwidth,barEnd.transform.localPosition.y, barEnd.transform.localPosition.z);

        for (int i = 0; i < separatorsObj.Length; i++) {
            separatorsObj[i].transform.localPosition =  new Vector3(gameManager.GetSatisfactionTrackPos(i+1) * (barwidth/100), separatorsObj[i].transform.localPosition.y, separatorsObj[i].transform.localPosition.z);
        }
	}
    void Update(){
        if (gameManager.GetGameStatus()) dungeonTimerText.text = gameManager.GetDungeonTime().ToString("F");
    }

    public void ResetUI()
    {
        dungeonTimerText.text = gameManager.GetDungeonTime().ToString("F");
        pointsText.text = gameManager.GetPoints(0, 1, 0).ToString();
        pointsSlider.value = gameManager.GetPoints(0, 1, 0);
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
        dungeonTimerText.text = ("0.00");
        DeadGo.SetActive(true);
    }

    public void PauseUI(){
        PauseGo.SetActive(true);
    }

    public void RemovePointUI(){
        pointsText.text = gameManager.GetPoints(0, 1, 0).ToString();
        pointsSlider.value = gameManager.GetPoints(0, 1, 0);
        PointsSliderChecker();
    }

    public void AddPointUI(){
        AccuracySliderCheck();
        pointsText.text = gameManager.GetPoints(0, 1, 0).ToString();
        pointsSlider.value = gameManager.GetPoints(0, 1, 0);
        PointsSliderChecker();
    }

    private void PointsSliderChecker()
    {

        if (gameManager.GetPoints(0, 1, 0) == 15 || gameManager.GetPoints(0, 1, 0) == 16)
        {
            sliderText.text = sliderFirst;
            sliderText.gameObject.SetActive(true);
            StartCoroutine(DeactivatorUI(sliderText, sliderTextTime));
        }
        else if (gameManager.GetPoints(0, 1, 0) == 30 || gameManager.GetPoints(0, 1, 0) == 31)
        {
            sliderText.text = sliderSecond;
            sliderText.gameObject.SetActive(true);
            StartCoroutine(DeactivatorUI(sliderText, sliderTextTime));
        }
        else if (gameManager.GetPoints(0, 1, 0) == 45 || gameManager.GetPoints(0, 1, 0) == 46)
        {
            sliderText.text = sliderThird;
            sliderText.gameObject.SetActive(true);
            StartCoroutine(DeactivatorUI(sliderText, sliderTextTime));
        }
        else if (gameManager.GetPoints(0, 1, 0) == 60)
        {
            sliderText.text = sliderFourth;
            sliderText.gameObject.SetActive(true);
            StartCoroutine(DeactivatorUI(sliderText, sliderTextTime));
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
        if (intro > 0) introText.text = intro.ToString();
        else if(intro == 0)introText.text = "";
        else if(intro < 0)
        { 
            introText.text = finalIntro;
            StartCoroutine(DeactivatorUI(introText, introTextTime));
        }
    }

    public void CoinsUI(int coins){
        coinsText.text = coins.ToString();
    }
    public void CollectibleUI(int key){
        if(key == 0) for (int i = 0; i < keysImages.Length; i++ ) keysImages[i].color = Color.grey;
        else keysImages[key - 1].color = Color.white;
    }


    public void ClimaxUIBehaviour(int feverpoints, bool climax)
    {
        if(climax){
            climaxUnlock.gameObject.SetActive(true);
            climaxNumber.gameObject.SetActive(true);
            climaxNumber.text = feverpoints.ToString();
        } 
        else{
            climaxUnlock.gameObject.SetActive(false);
            climaxNumber.text = feverpoints.ToString();
            climaxNumber.gameObject.SetActive(false);
        }
    }


    private IEnumerator DeactivatorUI(Text text, float time)
    {
        yield return new WaitForSeconds(time);
        text.gameObject.SetActive(false);
        StopCoroutine("DeactivatorUI");
    }
}
