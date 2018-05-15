using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class WinBehaviour : MonoBehaviour {

	[Header("Win Particles")]
	[SerializeField]
    private GameObject winParticles;

	private Camera mainCamera;

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

	bool[] done = new bool[3];

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
	}

	public IEnumerator CameraCoroutine()
	{
		done[0] = false;
		ogMainCameraPosition = mainCamera.gameObject.transform.position;
		yield return new WaitForSeconds(cameraDelayDuration);
		mainCamera.gameObject.transform.DOMove(cameraEndPos.position, cameraDuration).SetEase(cameraEasing);
		yield return new WaitForSeconds(cameraDuration);
		done[0] = true;
		if(done[0] && done[1] && done[2]) SceneManager.LoadScene(2);
		StopCoroutine("CameraCoroutine");
	}
    
	public IEnumerator ParticlesCoroutine(Transform player)
    {
		done[1] = false;
		winParticles.SetActive(false);
		yield return new WaitForSeconds(particlesDelayDuration);
		Instantiate(winParticles, player);
		winParticles.SetActive(true);
		yield return new WaitForSeconds(particlesDuration);
		done[1] = true;
		if (done[0] && done[1] && done[2]) SceneManager.LoadScene(2);
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
		if (done[0] && done[1] && done[2]) SceneManager.LoadScene(2);
		StopCoroutine("LightsCoroutine");
    }
}
