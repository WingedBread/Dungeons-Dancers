using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InputFeedbackController : MonoBehaviour {
    
    private GameManager gameManager;

    [Header("Sprites Feedback")]
    [SerializeField]
    GameObject correctFloorSprite;
    [SerializeField]
    GameObject incorrectFloorSprite;

    [Header("Duration Fade Sprites")]
    [SerializeField]
    float spriteDuartion;

    [SerializeField]
    ParticleSystem textParticle;
    ParticleSystem lightParticle;

    [SerializeField]
    string goodText;
    [SerializeField]
    string greatText;
    [SerializeField]
    string perfectText;
    [SerializeField]
    Text particleText;

    List<GameObject> trailGO = new List<GameObject>();

    private GameObject instantiatedGO;
    private GameObject badGO;


	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
	}
	
    public void CorrectFeedbackBehaviour()
    {
        trailGO.Add((GameObject)Instantiate(correctFloorSprite, transform.parent.parent));
        instantiatedGO = trailGO[trailGO.Count - 1];
        instantiatedGO.transform.position = new Vector3(transform.position.x, instantiatedGO.transform.position.y, transform.position.z);
        lightParticle = instantiatedGO.transform.GetChild(0).GetComponent<ParticleSystem>();

        //Easing Fade Out Correct
        switch(gameManager.GetRhythmAccuracy())
        {
            case 0: //Good
                particleText.text = goodText;
                break;
            case 1: //Perfect
                particleText.text = perfectText;
                break;
            case 2: //Great
                particleText.text = greatText;
                break;
        }
    }

    public void IncorrectFeedbackBehaviour()
    {
        badGO = Instantiate(incorrectFloorSprite, transform.parent.parent);
        badGO.transform.position = new Vector3(transform.position.x, instantiatedGO.transform.position.y, transform.position.z);

        for (int i = 0; i < trailGO.Count; i++){
            Destroy(trailGO[i]);
            //StartCoroutine - Easing Fade Incorrect
            if (i == (trailGO.Count - 1)) trailGO.Clear();
        }
    }
}
