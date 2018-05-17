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
    float fadeDuration =3f;

    [Header("Lose Stuff Duration:")]
    [SerializeField]
    float delayDuration = 0;
    [SerializeField]
    float duration = 3f;

    [SerializeField]
    Transform playerEndPos;

    [SerializeField]
    private Material offMaterial;
    [SerializeField]
    private Material onMaterial;

    [SerializeField]
    private GameObject parentFloor;

    [SerializeField]
    private GameObject loseUI;

    private Image loseUIFondo;
    private Image loseUIFace;

    private PlayerManager player;

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

        for (int i = 0; i < parentFloor.transform.childCount; i++)
        {
            parentFloor.transform.GetChild(i).GetComponent<MeshRenderer>().material = offMaterial;
        }
        yield return new WaitForSeconds(delayDuration);

        player.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("onLose", true);
        player.gameObject.transform.DOMove(playerEndPos.position, playerDuration).SetEase(playerEasing);
             
        yield return new WaitForSeconds(duration);
        loseUI.SetActive(true);
        loseUIFondo.DOFade(1, (fadeDuration/2));
        loseUIFace.DOFade(1, fadeDuration).OnComplete(() => ActivateUI(true));
        player.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("onLose", false);
	}

    public void Restart(){
        
        StartCoroutine(player.ResetPlayer(true));
        for (int i = 0; i < parentFloor.transform.childCount; i++)
        {
            parentFloor.transform.GetChild(i).GetComponent<MeshRenderer>().material = onMaterial;
        }
    }

    void ActivateUI(bool active){
        for (int i = 2; i < loseUI.transform.childCount; i++){
            loseUI.transform.GetChild(i).gameObject.SetActive(active);
        }
    }
}
