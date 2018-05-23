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
    float correctSpriteDuartion = 0.5f;
    [SerializeField]
    float incorrectSpriteDuartion = 0.5f;

    ParticleSystem textParticle;
    ParticleSystem lightParticle;

    [Header("Text Particle Texts & Particle Intensity")]
    [SerializeField]
    string goodText;
    [SerializeField]
    float goodIntensity = 10f;
    [SerializeField]
    string greatText;
    [SerializeField]
    float greatIntensity = 15f;
    [SerializeField]
    string perfectText;
    [SerializeField]
    float perfectIntensity = 30f;

    Text particleText;


    List<GameObject> correctTrail = new List<GameObject>();
    List<GameObject> incorrectTrail = new List<GameObject>();


    private GameObject correctGO;
    private GameObject incorrectGO;


	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        textParticle = transform.GetChild(1).GetChild(0).GetComponent<ParticleSystem>();
        lightParticle = transform.GetChild(1).GetChild(1).GetComponent<ParticleSystem>();
        particleText = transform.parent.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>();
	}
	
    public void CorrectFeedbackBehaviour()
    {
        correctTrail.Add((GameObject)Instantiate(correctFloorSprite, transform.parent.parent));
        correctGO = correctTrail[correctTrail.Count - 1];
        correctGO.transform.position = new Vector3(transform.position.x, correctGO.transform.position.y, transform.position.z);
        textParticle.Play();
        lightParticle.Play();
        //Easing Fade Out Correct
        switch(gameManager.GetRhythmAccuracy())
        {
            case 0: //Good
                particleText.text = goodText;
                var emissionGood = lightParticle.emission;
                var rateGood = emissionGood.rateOverTime;
                rateGood.constantMax = goodIntensity;
                emissionGood.rateOverTime = rateGood;
                break;
            case 1: //Perfect
                particleText.text = perfectText;
                var emissionPerfect = lightParticle.emission;
                var ratePerfect = emissionPerfect.rateOverTime;
                ratePerfect.constantMax = perfectIntensity;
                emissionPerfect.rateOverTime = ratePerfect;
                break;
            case 2: //Great
                particleText.text = greatText;
                var emissionGreat = lightParticle.emission;
                var rateGreat = emissionGreat.rateOverTime;
                rateGreat.constantMax = greatIntensity;
                emissionGreat.rateOverTime = rateGreat;
                break;
        }

        FadeOut(correctGO, true);
    }

    public void IncorrectFeedbackBehaviour()
    {
        incorrectTrail.Add((GameObject)Instantiate(incorrectFloorSprite, transform.parent.parent));
        incorrectGO = incorrectTrail[incorrectTrail.Count - 1];
        incorrectGO.transform.position = new Vector3(transform.position.x, incorrectGO.transform.position.y, transform.position.z);

        FadeOut(incorrectGO, false);
        for (int i = 0; i < correctTrail.Count; i++){
            Destroy(correctTrail[i]);
            //StartCoroutine - Easing Fade Incorrect
            if (i == (correctTrail.Count - 1)) correctTrail.Clear();
        }

        textParticle.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        lightParticle.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    void FadeOut(GameObject go, bool correct)
    {
        if(correct) go.GetComponent<SpriteRenderer>().DOFade(0, correctSpriteDuartion);
        else go.GetComponent<SpriteRenderer>().DOFade(0, incorrectSpriteDuartion);
    }
}
