using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class InputFeedbackController : MonoBehaviour {
    
    private GameManager gameManager;

    [Header("Sprites Feedback")]
    [SerializeField]
    GameObject correctFloorSprite;
    [SerializeField]
    GameObject incorrectFloorSprite;

    [Header("Duration Fade")]
    [SerializeField]
    float correctSpriteDuration = 0.5f;
    [SerializeField]
    float incorrectSpriteDuration = 0.5f;
    [SerializeField]
    float textDuration = 0.5f;

    [Header("Text Easing")]
    [SerializeField]
    Ease textEasing;

    [SerializeField]
    GameObject goodTextParticle;
    [SerializeField]
    GameObject greatTextParticle;
    [SerializeField]
    GameObject perfectTextParticle;

    List<GameObject> instantiatedParticlesGO = new List<GameObject>();

    List<GameObject> correctTrail = new List<GameObject>();
    List<GameObject> incorrectTrail = new List<GameObject>();


    TextMeshPro greatText;
    TextMeshPro goodText;
    TextMeshPro perfectText;
    private GameObject correctGO;
    private GameObject incorrectGO;
    private GameObject particlesGO;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
	}
	
    public void CorrectFeedbackBehaviour()
    {
        correctTrail.Add((GameObject)Instantiate(correctFloorSprite, transform.parent.parent));
        correctGO = correctTrail[correctTrail.Count - 1];
        correctGO.transform.position = new Vector3(transform.position.x, correctGO.transform.position.y, transform.position.z);


        //Easing Fade Out Correct
        switch(gameManager.GetRhythmAccuracy())
        {
            case 0: //Good
                instantiatedParticlesGO.Add((GameObject)Instantiate(goodTextParticle, transform.parent.parent));
                particlesGO = instantiatedParticlesGO[instantiatedParticlesGO.Count - 1];
                goodText = particlesGO.GetComponent<TextMeshPro>();
                particlesGO.transform.position = new Vector3(transform.position.x, goodTextParticle.transform.position.y, transform.position.z);
                goodText.DOFade(0, textDuration).SetEase(textEasing);
                particlesGO.transform.DOMove(new Vector3(particlesGO.transform.localPosition.x, particlesGO.transform.localPosition.y, particlesGO.transform.localPosition.z+1), textDuration);

                break;
            case 1: //Perfect
                instantiatedParticlesGO.Add((GameObject)Instantiate(perfectTextParticle, transform.parent.parent));
                particlesGO = instantiatedParticlesGO[instantiatedParticlesGO.Count - 1];
                perfectText = particlesGO.GetComponent<TextMeshPro>();
                particlesGO.transform.position = new Vector3(transform.position.x, perfectTextParticle.transform.position.y, transform.position.z);
                perfectText.DOFade(0, textDuration).SetEase(textEasing);
                particlesGO.transform.DOMove(new Vector3(particlesGO.transform.localPosition.x, particlesGO.transform.localPosition.y, particlesGO.transform.localPosition.z + 1), textDuration);
                break;
            case 2: //Great
                instantiatedParticlesGO.Add((GameObject)Instantiate(greatTextParticle, transform.parent.parent));
                particlesGO = instantiatedParticlesGO[instantiatedParticlesGO.Count - 1];
                greatText = particlesGO.GetComponent<TextMeshPro>();
                particlesGO.transform.position = new Vector3(transform.position.x, greatTextParticle.transform.position.y, transform.position.z);
                greatText.DOFade(0, textDuration).SetEase(textEasing);
                particlesGO.transform.DOMove(new Vector3(particlesGO.transform.localPosition.x, particlesGO.transform.localPosition.y, particlesGO.transform.localPosition.z + 1), textDuration);
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

        for (int i = 0; i < correctTrail.Count; i++)
        {
            Destroy(correctTrail[i]);
            if (i == (correctTrail.Count - 1)) correctTrail.Clear();
        }


    }

    void FadeOut(GameObject go, bool correct)
    {
        if(correct)
        {
            go.GetComponent<SpriteRenderer>().DOFade(0, correctSpriteDuration).OnComplete(() => DestroyStuff(go, correct));
        }
        else
        {
            go.GetComponent<SpriteRenderer>().DOFade(0, incorrectSpriteDuration).OnComplete(() => DestroyStuff(go, correct));
            incorrectTrail.Clear();
        }
    }

    void DestroyStuff(GameObject go, bool correct){

        if (correct)
        {
            for (int i = 0; i < correctTrail.Count; i++)
            {
                if (go == correctTrail[i])
                {
                    Destroy(instantiatedParticlesGO[i]);
                }
            }
            Destroy(go);
        }
        else
        {
            Destroy(go);

            for (int w = 0; w < instantiatedParticlesGO.Count; w++)
            {
                Destroy(instantiatedParticlesGO[w]);
                if (w == (instantiatedParticlesGO.Count - 1)) instantiatedParticlesGO.Clear();
            }

        }
    }
}
