using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LoseBehaviour : MonoBehaviour {

    [Header("Player Easing")]
    [SerializeField]
    private Ease playerEasing;

    [Header("Player Easing Duration:")]
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

    private void Start()
    {
        loseUIFondo = loseUI.transform.GetChild(0).GetComponent<Image>();
        loseUIFace = loseUI.transform.GetChild(1).GetComponent<Image>();
    }

    public IEnumerator OnLose(PlayerManager playerM)
	{
        loseUIFondo.color = new Vector4(0, 0, 0, 0);
        loseUIFace.color = new Vector4(255, 0, 0, 0);
        ActivateUI(false);
        player = playerM;

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
        
        StartCoroutine(player.ResetPlayer(true));
        Destroy(instantiatedGO);
    }

    void ActivateUI(bool active){
        for (int i = 2; i < loseUI.transform.childCount; i++){
            loseUI.transform.GetChild(i).gameObject.SetActive(active);
        }
    }
}
