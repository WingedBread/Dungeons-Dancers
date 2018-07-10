using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SonicBloom.Koreo;
using System.Collections;
using SonicBloom.Koreo.Players;
using TMPro;

public class MenuSceneScript : MonoBehaviour {

    private SimpleMusicPlayer splayer;

    [SerializeField]
    private TextMeshProUGUI startText;
    [SerializeField]
    private Image logoImage;
    [SerializeField]
    private Image spotImage01;
    [SerializeField]
    private Image spotImage02;
    [SerializeField]
    private Image spotImage03;
    [SerializeField]
    private Image spotImage04;

    [EventID]
    public string eventID_text;
    [EventID]
    public string eventID_spot1;
    [EventID]
    public string eventID_spot2;
    [EventID]
    public string eventID_spot3;
    [EventID]
    public string eventID_spot4;

    private int controllerConfig = 0;

    [Header("LogoScreen")]
    [SerializeField]
    private GameObject logoScreen;

    [Header("OptionsScreen")]
    [SerializeField]
    private GameObject optionsScreen;
    [SerializeField]
    private Toggle controllerToggle;

    private string[] submitTexts = { "Submit", "Submit_DDR"};
    private string[] cancelTexts = { "Cancel", "Cancel_DDR"};

    [Header("Restart With Controller?")]
    [SerializeField]
    private bool restart_with_controller = false;

    // Use this for initialization
    void Start () {
        PlayerPrefs.SetInt("TotalScore", 0);
        PlayerPrefs.SetInt("TotalScore", 0);
        PlayerPrefs.SetInt("NumGoodMoves", 0);
        PlayerPrefs.SetInt("NumGreatMoves", 0);
        PlayerPrefs.SetInt("NumPerfectMoves", 0);
        PlayerPrefs.SetInt("NumBadMoves", 0);
        PlayerPrefs.SetInt("MovesInClimax", 0);
        PlayerPrefs.SetInt("MovesAfterClimax", 0);
        if(restart_with_controller )PlayerPrefs.SetInt("ControllerType", 0);
        splayer = GetComponent<SimpleMusicPlayer>();
        Koreographer.Instance.RegisterForEvents(eventID_text, FadeText);
        Koreographer.Instance.RegisterForEvents(eventID_spot1, FadeSpot1);
        Koreographer.Instance.RegisterForEvents(eventID_spot2, FadeSpot2);
        Koreographer.Instance.RegisterForEvents(eventID_spot3, FadeSpot3);
        Koreographer.Instance.RegisterForEvents(eventID_spot4, FadeSpot4);
	}
	
	// Update is called once per frame
	void Update () {

        if (logoScreen.activeInHierarchy) LogoScreenBehaviour();
        else OptionsScreenBehaviour();
	}


    void LogoScreenBehaviour()
    {
        if (Input.GetButtonDown(submitTexts[PlayerPrefs.GetInt("ControllerType")]))
        {
            splayer.Stop();
            Koreographer.Instance.UnregisterForEvents(eventID_text, FadeText);
            Koreographer.Instance.UnregisterForEvents(eventID_spot1, FadeSpot1);
            Koreographer.Instance.UnregisterForEvents(eventID_spot2, FadeSpot2);
            Koreographer.Instance.UnregisterForEvents(eventID_spot3, FadeSpot3);
            Koreographer.Instance.UnregisterForEvents(eventID_spot4, FadeSpot4);
            StartCoroutine(LoadYourAsyncScene());
        }
        else if (Input.GetButtonDown(cancelTexts[PlayerPrefs.GetInt("ControllerType")]))
        {
            logoScreen.SetActive(false);
            optionsScreen.SetActive(true);
        }
    }

    void OptionsScreenBehaviour()
    {
        if (controllerToggle.isOn)
        {
            PlayerPrefs.SetInt("ControllerType", 0);
        }
        else
        {
            PlayerPrefs.SetInt("ControllerType", 1);
        }

        if (Input.GetButtonDown(cancelTexts[PlayerPrefs.GetInt("ControllerType")]))
        {
            logoScreen.SetActive(true);
            optionsScreen.SetActive(false);
        }
    }


    void FadeText(KoreographyEvent kevent)
    {
        Sequence s = DOTween.Sequence();
        s.Append(startText.DOFade(0, 0.1f));
        s.Append(startText.DOFade(1, 0.05f));
    }
    void FadeSpot1(KoreographyEvent kevent)
    {
        Sequence l = DOTween.Sequence();
        l.Append(spotImage01.DOFade(0, 0.1f));
        l.Append(spotImage01.DOFade(1, 0.05f));
    }
    void FadeSpot2(KoreographyEvent kevent)
    {
        Sequence n = DOTween.Sequence();
        n.Append(spotImage02.DOFade(0, 0.1f));  
        n.Append(spotImage02.DOFade(1, 0.05f));
    }
    void FadeSpot3(KoreographyEvent kevent)
    {
        Sequence w = DOTween.Sequence();
        w.Append(spotImage03.DOFade(0, 0.1f));
        w.Append(spotImage03.DOFade(1, 0.05f));
    }
    void FadeSpot4(KoreographyEvent kevent)
    {
        Sequence j = DOTween.Sequence();
        j.Append(spotImage04.DOFade(0, 0.1f));
        j.Append(spotImage04.DOFade(1, 0.05f));
    }

    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void SetControllerValue(int value)
    {
        controllerConfig = value;
    }
}
