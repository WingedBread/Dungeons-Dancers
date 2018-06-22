using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class WinBehaviour : MonoBehaviour {

	[Header("Win Particles")]
	[SerializeField]
    private GameObject winParticles;

	private Camera mainCamera;

    [Header("WinText Prefab")]
    [SerializeField]
    private GameObject winTextPrefab;

    [Header("WinText Easing")]
    [SerializeField]
    private float winTextTime = 2;
    [SerializeField]
    private Ease easingInList = Ease.InExpo;
    [SerializeField]
    private Ease easingOutList = Ease.OutElastic;
    [SerializeField]
    private Vector3 inPositionVector3 = new Vector3(6.14f, 6, 3.15f);
    [SerializeField]
    private float easingInDuration = 0.5f;
    [SerializeField]
    private Vector3 outPositionVector3 = new Vector3(-12, 6, 3.15f);
    [SerializeField]
    private float easingOutDuration = 1f;

	[Header("Camera Easing")]
	[SerializeField]
	private Ease cameraEasing;
       
	[Header("Light Sprites")]
	[SerializeField]
	private GameObject lightsParent;

	[Header("Camera End Position")]
	[SerializeField]
    Transform cameraEndPos;

	[Header("Delay Durations:")]
	[SerializeField]
	float cameraDelayDuration = 1.5f;
	[SerializeField]    
	float particlesDelayDuration = 1.5f;
	[SerializeField]   
	float lightsDelayDuration = 1.5f;
	[Space]
	[Header("Durations:")]
	[SerializeField]
	float cameraDuration = 1.5f;
	[SerializeField]
	float particlesDuration = 1.5f;
	[SerializeField]
    float lightsDuration = 1.5f;

	[Header("Travel to Scene")]
	[SerializeField]
	private int sceneNum = 3;
	bool[] done = new bool[3];

    private GameObject instantiatedWinText;
	private Vector3 ogMainCameraPosition;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
	}

	public void OnWin(Transform player)
	{
		StartCoroutine(CameraCoroutine());
		StartCoroutine(ParticlesCoroutine(player));
		StartCoroutine(LightsCoroutine());
        StartCoroutine(WinTextCoroutine());
	}

	public IEnumerator CameraCoroutine()
	{
		done[0] = false;
		ogMainCameraPosition = mainCamera.gameObject.transform.position;
		yield return new WaitForSeconds(cameraDelayDuration);
		mainCamera.gameObject.transform.DOMove(cameraEndPos.position, cameraDuration).SetEase(cameraEasing);
		yield return new WaitForSeconds(cameraDuration);
		done[0] = true;
		if(done[0] && done[1] && done[2]) SceneManager.LoadScene(sceneNum);
		StopCoroutine("CameraCoroutine");
	}
    
	public IEnumerator ParticlesCoroutine(Transform player)
    {
		done[1] = false;
		winParticles.SetActive(false);
		yield return new WaitForSeconds(particlesDelayDuration);
		winParticles.SetActive(true);
		Instantiate(winParticles, player);
		yield return new WaitForSeconds(particlesDuration);
		done[1] = true;
		if (done[0] && done[1] && done[2]) SceneManager.LoadScene(sceneNum);
		StopCoroutine("ParticlesCoroutine");
    }

	public IEnumerator LightsCoroutine()
    {
		done[2] = false;
		lightsParent.SetActive(false);
		yield return new WaitForSeconds(lightsDelayDuration);
		lightsParent.SetActive(true);
		yield return new WaitForSeconds(lightsDuration);
		done[2] = true;
		if (done[0] && done[1] && done[2]) SceneManager.LoadScene(sceneNum);
		StopCoroutine("LightsCoroutine");
    }

    IEnumerator WinTextCoroutine()
    {
        instantiatedWinText = (GameObject)Instantiate(winTextPrefab, transform.parent);
        Sequence g = DOTween.Sequence();
        g.Append(instantiatedWinText.transform.DOLocalMove(inPositionVector3, easingInDuration).SetEase(easingInList));
        yield return new WaitForSeconds(winTextTime);
        g.Append(instantiatedWinText.transform.DOLocalMove(outPositionVector3, easingOutDuration).SetEase(easingOutList));
    }
}
