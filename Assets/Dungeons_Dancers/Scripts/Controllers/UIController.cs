﻿using System.Collections;
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
    private Image[] keysImages;

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
	}
    void Update(){
        dungeonTimerText.text = gameManager.GetDungeonTime().ToString("F");
    }

    public void ResetUI()
    {
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
        switch (gameManager.GetPoints(0, 1, 0))
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

    public void CoinsUI(int coins){
        coinsText.text = coins.ToString();
    }
    public void CollectibleUI(int key){
        if(key == 0) for (int i = 0; i < keysImages.Length; i++ ) keysImages[i].color = Color.grey;
        else keysImages[key - 1].color = Color.white;
    }
    private IEnumerator DeactivatorUI(Text text, float time)
    {
        yield return new WaitForSeconds(time);
        text.gameObject.SetActive(false);
        StopCoroutine("DeactivatorUI");
    }
}
