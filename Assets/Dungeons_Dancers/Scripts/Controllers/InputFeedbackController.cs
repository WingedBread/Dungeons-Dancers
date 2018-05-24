using System.Collections.Generic;
using UnityEngine;
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

    [SerializeField]
    GameObject goodTextParticle;
    [SerializeField]
    GameObject greatTextParticle;
    [SerializeField]
    GameObject perfectTextParticle;

    List<GameObject> instantiatedParticlesGO = new List<GameObject>();

    List<GameObject> correctTrail = new List<GameObject>();
    List<GameObject> incorrectTrail = new List<GameObject>();

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
                particlesGO.transform.position = new Vector3(transform.position.x, goodTextParticle.transform.position.y, transform.position.z);
                particlesGO.GetComponent<ParticleSystem>().Play();
                break;
            case 1: //Perfect
                instantiatedParticlesGO.Add((GameObject)Instantiate(perfectTextParticle, transform.parent.parent));
                particlesGO = instantiatedParticlesGO[instantiatedParticlesGO.Count - 1];
                particlesGO.transform.position = new Vector3(transform.position.x, perfectTextParticle.transform.position.y, transform.position.z);
                particlesGO.GetComponent<ParticleSystem>().Play();
                break;
            case 2: //Great
                instantiatedParticlesGO.Add((GameObject)Instantiate(greatTextParticle, transform.parent.parent));
                particlesGO = instantiatedParticlesGO[instantiatedParticlesGO.Count - 1];
                particlesGO.transform.position = new Vector3(transform.position.x, greatTextParticle.transform.position.y, transform.position.z);
                particlesGO.GetComponent<ParticleSystem>().Play();
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

    }

    void FadeOut(GameObject go, bool correct)
    {
        if(correct)
        {
            go.GetComponent<SpriteRenderer>().DOFade(0, correctSpriteDuartion).OnComplete(() => DestroyStuff(go, correct));
        }
        else
        {
            go.GetComponent<SpriteRenderer>().DOFade(0, incorrectSpriteDuartion).OnComplete(() => DestroyStuff(go, correct));
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

            for (int i = 0; i < correctTrail.Count; i++)
            {
                Destroy(correctTrail[i]);
                if (i == (correctTrail.Count - 1))
                {
                    correctTrail.Clear();

                    for (int w = 0; w < instantiatedParticlesGO.Count; w++)
                    {
                        Destroy(instantiatedParticlesGO[w]);
                        if (w == (instantiatedParticlesGO.Count - 1)) instantiatedParticlesGO.Clear();
                    }
                }
            }
        }
    }
}
