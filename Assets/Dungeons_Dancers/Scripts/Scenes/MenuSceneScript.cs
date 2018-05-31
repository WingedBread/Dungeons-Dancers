using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SonicBloom.Koreo;
using System.Collections;
using SonicBloom.Koreo.Players;

public class MenuSceneScript : MonoBehaviour {

    private SimpleMusicPlayer splayer;

    [SerializeField]
    private Text startText;
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

	private void Awake()
	{
        //Application.targetFrameRate = 60;
	}
	// Use this for initialization
	void Start () {
        splayer = GetComponent<SimpleMusicPlayer>();
        Koreographer.Instance.RegisterForEvents(eventID_text, FadeText);
        Koreographer.Instance.RegisterForEvents(eventID_spot1, FadeSpot1);
        Koreographer.Instance.RegisterForEvents(eventID_spot2, FadeSpot2);
        Koreographer.Instance.RegisterForEvents(eventID_spot3, FadeSpot3);
        Koreographer.Instance.RegisterForEvents(eventID_spot4, FadeSpot4);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Jump"))
        {
            splayer.Stop();
            Koreographer.Instance.UnregisterForEvents(eventID_text, FadeText);
            Koreographer.Instance.UnregisterForEvents(eventID_spot1, FadeSpot1);
            Koreographer.Instance.UnregisterForEvents(eventID_spot2, FadeSpot2);
            Koreographer.Instance.UnregisterForEvents(eventID_spot3, FadeSpot3);
            Koreographer.Instance.UnregisterForEvents(eventID_spot4, FadeSpot4);
            StartCoroutine(LoadYourAsyncScene());
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
}
