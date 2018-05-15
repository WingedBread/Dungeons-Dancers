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

	[Header("Camera Easing Duration:")]
    [SerializeField]
    float cameraDuration = 1.5f;

	[Header("Light Sprites")]
	[SerializeField]
	private GameObject lightsParent;

	[Header("Win Stuff Duration:")]
    [SerializeField]
    float duration = 3f;

	[SerializeField]
	Vector3 cameraEndPos;

	private Vector3 ogMainCameraPosition;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		lightsParent.SetActive(false);
		ogMainCameraPosition = mainCamera.gameObject.transform.position;
	}

	public IEnumerator OnWin(Transform player)
	{
		mainCamera.gameObject.transform.DOMove(cameraEndPos, cameraDuration).SetEase(cameraEasing);
		Instantiate(winParticles, player);
		winParticles.SetActive(true);
        lightsParent.SetActive(true);
		yield return new WaitForSeconds(duration);
		mainCamera.gameObject.transform.position = ogMainCameraPosition;
		SceneManager.LoadScene(2);
		StopCoroutine("OnWin");
	}
}
