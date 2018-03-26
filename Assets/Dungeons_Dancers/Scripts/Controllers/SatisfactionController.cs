using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatisfactionController : MonoBehaviour {

    [Header("Game Manager")]
    private GameManager gameManager;

    [Header("Setup Satisfaction")]
    [SerializeField]
    private SatisfactionBarSetup setupValues;

    private int points;
    private int feverPoints;

	// Use this for initialization
    void Awake()
    {
        points = setupValues.initPoints;
        feverPoints = setupValues.amountOfFailInputsWhenFever;
    }
	void Start () {
        gameManager = GetComponent<GameManager>();
	}
	
    public void AddPoint()
    {
        PointEvents();
        if (points >= setupValues.maxPoints)
        {
            FeverState();
        }
        else
        {
            switch (gameManager.GetRhythmAccuracy())
            {
                case 0:
                    points = points + setupValues.soonPoints;
                    break;
                case 1:
                    points = points + setupValues.perfectPoints;
                    break;
                case 2:
                    points = points + setupValues.latePoints;
                    break;
            }
            if (points >= setupValues.maxPoints) points = setupValues.maxPoints;
        }
    }

    void PointEvents()
    {
        //Just Once(?)
        if (points == 0) gameManager.levelSetup.EvtStatisfactionZero();
        else if (points < 15) gameManager.levelSetup.EvtStatisfactionLv1();
        else if (points < 30) gameManager.levelSetup.EvtStatisfactionLv2();
        else if (points < 45) gameManager.levelSetup.EvtStatisfactionLv3();
        else if (points < 60) gameManager.levelSetup.EvtStatisfactionClimax();
    } 

    public void RemovePoint()
    {
        if (points >= setupValues.maxPoints)
        {
            feverPoints--;
            FeverState();
        }
        else
        {
            points = points - setupValues.failPoints;
            if (points <= setupValues.minPoints) {
                gameManager.Dead();
                points = setupValues.minPoints;
            }
        }
    }

    private void FeverState()
    {
        if (feverPoints <= 0)
        {
            points--;
            feverPoints = setupValues.amountOfFailInputsWhenFever;
        }
    }

    public void ResetSatisfaction(){
        points = setupValues.initPoints;
        feverPoints = setupValues.amountOfFailInputsWhenFever;
    }

    public int GetSatisfactionPoints(int min, int current, int max)
    {

        if (min == 1 && current == 0 && max == 0) return setupValues.minPoints;
        else if (min == 0 && current == 1 && max == 0) return points;
        else if (min == 0 && current == 0 && max == 1) return setupValues.maxPoints;

        else return 0;
    }

    public bool GetFeverState(){
        if (points >= setupValues.maxPoints) return true;
        else return false;
    }

}
