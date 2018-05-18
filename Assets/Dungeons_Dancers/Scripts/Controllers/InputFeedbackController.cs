using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InputFeedbackController : MonoBehaviour {
    
    private GameManager gameManager;

    [Header("Sprites Feedback")]
    [SerializeField]
    Sprite correctFloorSprite;
    [SerializeField]
    Sprite incorrectFloorSprite;

    [Header("Duration Fade Sprites")]
    [SerializeField]
    float spriteDuartion;

    ParticleSystem textParticle;
    ParticleSystem lightParticle;

    string moveText;

    GameObject instantiatedGO;



	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
	}
	
    public void CorrectFeedbackBehaviour()
    {
        switch(gameManager.GetRhythmAccuracy())
        {
            case 0: //Good
                moveText = "Good";
                
                break;
            case 1: //Perfect
                moveText = "Perfect!";
                
                break;
            case 2: //Great
                moveText = "Great";

                break;
        }
    }

    public void IncorrectFeedbackBehaviour()
    {
        Instantiate(incorrectFloorSprite, transform);
    }
}
