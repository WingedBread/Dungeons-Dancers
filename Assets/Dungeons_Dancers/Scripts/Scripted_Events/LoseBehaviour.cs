using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoseBehaviour : MonoBehaviour {

    [Header("Player Easing")]
    [SerializeField]
    private Ease playerEasing;

    [Header("Player Easing Duration:")]
    [SerializeField]
    float playerDuration = 1.5f;

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

    private PlayerManager player;

    private void Start()
    {
        
    }

    public IEnumerator OnLose(PlayerManager playerM)
	{
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
        //loseui stuff
	}

    public void Restart(){
        
        StartCoroutine(player.ResetPlayer(true));
        for (int i = 0; i < parentFloor.transform.childCount; i++)
        {
            parentFloor.transform.GetChild(i).GetComponent<MeshRenderer>().material = onMaterial;
        }
    }
}
