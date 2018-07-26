using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LoseBehaviour : MonoBehaviour {

	// --- Curial add: ScreenShake ---
	private Camera mainCamera; 
	[Header("Camera shake")]
	[SerializeField]
	private Vector3 ShakeRotationStrenght = new Vector3 (0, 20, 0);
	[SerializeField]
	float ShakeDuration = 0.25f;
	[SerializeField]
	int ShakeVibration = 1;
	[SerializeField]
	float Randomness = 0;
	[SerializeField]
	bool FadeOut = true;
	// -------------------------------

    [Header("Player Kick out Easing")]
    [SerializeField]
    private Ease playerEasing;
    //[Header("Player Easing Duration:")]
    [SerializeField]
    float playerDuration = 1.5f;

    [Header("Fade Easing Duration:")]
    [SerializeField]
    float fadeFaceDuration = 3f;
    [SerializeField]
    float fadeFondoDuration = 6f;

    [Header("Lose Stuff Duration:")]
    [SerializeField]
    float delayDuration = 0;
    [SerializeField]
    float delayFade = 3f;

    [Header("Player Final Position")]
    [SerializeField]
    Transform playerEndPos;

    [Header("Particle System Lose")]
    [SerializeField]
    private GameObject loseParticleSystem;

    [Header("UI Lose Screen")]
    [SerializeField]
    private GameObject loseUI;

    private Image loseUIFondo;
    private Image loseUIFace;

    private PlayerManager player;

    private AudioSource audioSource;

    private GameObject instantiatedGO;

    [Header("Colors")]
    [SerializeField]
    private Color loseUIFondoColor;
    [SerializeField]
    private Color loseUIFaceColor;

    [Header("First Button Selected on Retry")]
    [SerializeField]
    private Button firstButton;

    private Transform ogCameraTransform;

    private void Start()
    {
		mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>(); // Curial add: ScreenShake
        loseUIFondo = loseUI.transform.GetChild(0).GetComponent<Image>();
        loseUIFace = loseUI.transform.GetChild(1).GetComponent<Image>();
        ogCameraTransform = mainCamera.gameObject.transform;
    }

    public IEnumerator OnLose(PlayerManager playerM)
	{
        loseUIFondo.color = loseUIFondoColor;
        loseUIFace.color = loseUIFaceColor;
        ActivateUI(false);
        player = playerM;
		mainCamera.gameObject.transform.DOShakeRotation(ShakeDuration, ShakeRotationStrenght, ShakeVibration, Randomness, FadeOut); // Curial add: ScreenShake

        yield return new WaitForSeconds(delayDuration);
        instantiatedGO = Instantiate(loseParticleSystem, transform.parent);
        instantiatedGO.transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

        player.transform.GetChild(0).localRotation = Quaternion.Euler(0, 120, 0);
        player.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("onLose", true);
        player.gameObject.transform.DOMove(playerEndPos.position, playerDuration).SetEase(playerEasing);
             
        yield return new WaitForSeconds(delayFade);
        loseUI.SetActive(true);
        loseUIFace.DOFade(1, (fadeFaceDuration));
        loseUIFondo.DOFade(1, fadeFondoDuration).OnComplete(() => ActivateUI(true));
	}

    public void Restart(){
        mainCamera.gameObject.transform.rotation = ogCameraTransform.localRotation; // Reinit Camera values
        mainCamera.gameObject.transform.position = ogCameraTransform.localPosition;
        StartCoroutine(player.ResetPlayer(true));
        Destroy(instantiatedGO);
    }

    void ActivateUI(bool active){
        for (int i = 2; i < loseUI.transform.childCount; i++){
            loseUI.transform.GetChild(i).gameObject.SetActive(active);
        }
        if(active) firstButton.Select();
    }
}
