using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CheckpointBehaviour : MonoBehaviour {

	[Header("Emoji Particles")]
	[SerializeField]
	private GameObject emojiParticles;
	[Header("UI Flash Image")]
	[SerializeField]
	private Image flashUI;
	[Space]
	[Header("Player Stuff:")]
	[SerializeField]
	private GameObject rainbowMouth;
	[SerializeField]
	private GameObject mobilePhone;

    [Header("Selfie")]
    [SerializeField]
    private GameObject selfiePrefab;
    [SerializeField]
    private Sprite[] selfieSprites = new Sprite[3];

    [Header("Selfie Easing")]
    [SerializeField]
    private float selfieTime = 2;
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


	[Header("Chekpoint Sprites:")]
	[SerializeField]
	private Sprite checkpointPass;
	[SerializeField]
    private Sprite checkpointOld;

    private GameObject instantiatedSelfie;

	private List<GameObject> checkpoints = new List<GameObject>();

	private List<GameObject> instantiatedObj = new List<GameObject>();

	[Header("Checkpoint Stuff Duration:")]
	[SerializeField]
	float duration = 0.5f; //TODO: Beat Duration

    [SerializeField]
    PlayerManager player;

    //Use this for initialization
    void Start () 
	{
		flashUI.gameObject.SetActive(false);
		rainbowMouth.SetActive(false);
		mobilePhone.SetActive(false);
	}

    public IEnumerator OnCheckpoint(GameObject currectcheckpoint)
	{
        StartCoroutine(CheckpointSelfie());
		instantiatedObj.Add((GameObject)Instantiate(emojiParticles, currectcheckpoint.transform));
		flashUI.gameObject.SetActive(true);
        rainbowMouth.SetActive(true);
        mobilePhone.SetActive(true);
		flashUI.DOFade(0, duration);
		Sequence s = DOTween.Sequence();
		s.Append(mobilePhone.transform.GetChild(0).DOScale((mobilePhone.transform.GetChild(0).localScale * 3), duration/4));
		s.Append(mobilePhone.transform.GetChild(0).DOScale(mobilePhone.transform.GetChild(0).localScale * 0, duration/3));

		checkpoints.Add(currectcheckpoint);

		currectcheckpoint.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = checkpointPass;

		for (int w = 0; w < checkpoints.Count; w++)
		{
			if (checkpoints[w].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite == checkpointPass && checkpoints[w] != currectcheckpoint)
			{
				checkpoints[w].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = checkpointOld;
			}
		}

		for (int i = 0; i < emojiParticles.transform.childCount; i++)
        {
            emojiParticles.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
        }
		yield return new WaitForSeconds(duration);

        ResetCheckpoint();
	}

    IEnumerator CheckpointSelfie()
    {
        if (instantiatedSelfie != null)
        {
            Destroy(instantiatedSelfie);

            instantiatedSelfie = (GameObject)Instantiate(selfiePrefab, transform.parent);
            instantiatedSelfie.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = selfieSprites[Random.Range(0, selfieSprites.Length)];
            Sequence g = DOTween.Sequence();
            g.Append(instantiatedSelfie.transform.DOLocalMove(inPositionVector3, easingInDuration).SetEase(easingInList));
            yield return new WaitForSeconds(selfieTime);
            g.Append(instantiatedSelfie.transform.DOLocalMove(outPositionVector3, easingOutDuration).OnComplete(DestroySelfie).SetEase(easingOutList));
        }
        else
        {
            instantiatedSelfie = (GameObject)Instantiate(selfiePrefab, transform.parent);
            instantiatedSelfie.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = selfieSprites[Random.Range(0, selfieSprites.Length)];
            Sequence g = DOTween.Sequence();
            g.Append(instantiatedSelfie.transform.DOLocalMove(inPositionVector3, easingInDuration).SetEase(easingInList));
            yield return new WaitForSeconds(selfieTime);
            g.Append(instantiatedSelfie.transform.DOLocalMove(outPositionVector3, easingOutDuration).OnComplete(DestroySelfie).SetEase(easingOutList));
        }
    }

    public void ResetCheckpoint(){

        for (int i = 0; i < emojiParticles.transform.childCount; i++)
        {
            emojiParticles.transform.GetChild(i).GetComponent<ParticleSystem>().Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        for (int w = 0; w < instantiatedObj.Count; w++)
        {
            Destroy(instantiatedObj[w]);
        }

        player.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("onCheckpoint", false);

        flashUI.gameObject.SetActive(false);
        mobilePhone.transform.GetChild(0).localScale = new Vector3(0.1f, 0.1f, 0.1f);
        flashUI.color = Color.white;
        rainbowMouth.SetActive(false);
        mobilePhone.SetActive(false);

        StopCoroutine("OnCheckpoint");
    }

    void DestroySelfie()
    {
        Destroy(instantiatedSelfie);
        StopCoroutine("CheckpointSelfie");
    }
}
