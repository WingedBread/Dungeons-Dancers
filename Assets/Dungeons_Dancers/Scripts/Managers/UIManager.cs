using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private GameManager gameManager;

    [Header("UI")]
    public Text pointsText;
    [SerializeField]
    private GameObject WinGo;
    [SerializeField]
    private GameObject DeadGo;

    [Header("Slider")]
    public Slider pointsSlider;
    [SerializeField]
    private Text sliderText;
    [Header("Choose Slider Text Time")]
    [SerializeField]
    private int sliderTextTime;

	// Use this for initialization
	void Start () {
        gameManager = GetComponent<GameManager>();
        pointsSlider.value = gameManager.GetPoints();
        pointsText.text = gameManager.GetPoints().ToString();
	}
	
    public void ResetUI()
    {
        pointsText.text = gameManager.GetPoints().ToString();
        WinGo.SetActive(false);
        DeadGo.SetActive(false);
    }

    public void WinUI()
    {
        WinGo.SetActive(true);
    }

    public void DeadUI()
    {
        DeadGo.SetActive(true);
    }

    public void RemovePointUI(){
        pointsText.text = gameManager.GetPoints().ToString();
        pointsSlider.value = gameManager.GetPoints();
        PointsSliderChecker();
    }

    public void AddPointUI(){
        pointsText.text = gameManager.GetPoints().ToString();
        pointsSlider.value = gameManager.GetPoints();
        PointsSliderChecker();
    }

    private IEnumerator DeactivatorUI(Text text, int time)
    {
        yield return new WaitForSeconds(time);
        text.gameObject.SetActive(false);
        StopCoroutine("DeactivatorUI");
    }

    private void PointsSliderChecker()
    {
        switch (gameManager.GetPoints())
        {
            case 5:
                sliderText.text = "Begginner...";
                sliderText.gameObject.SetActive(true);
                StartCoroutine(DeactivatorUI(sliderText, sliderTextTime));
                break;
            case 10:
                sliderText.text = "Halfway!";
                sliderText.gameObject.SetActive(true);
                StartCoroutine(DeactivatorUI(sliderText, sliderTextTime));
                break;
            case 15:
                sliderText.text = "Almost there!!!";
                sliderText.gameObject.SetActive(true);
                StartCoroutine(DeactivatorUI(sliderText, sliderTextTime));
                break;
            case 20:
                sliderText.text = "FEEEEVEEER!!!";
                sliderText.gameObject.SetActive(true);
                StartCoroutine(DeactivatorUI(sliderText, sliderTextTime));
                break;
        }
    }
}
