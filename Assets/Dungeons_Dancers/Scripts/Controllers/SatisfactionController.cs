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

    private int pointsflag = 0;

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
                    gameManager.levelSetup.EvtPerfectMove();
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
        if (points == 0 && pointsflag != 0)
        {
            gameManager.levelSetup.EvtSatisfactionZero();
            pointsflag = 0;
        }
        else if (points < 15 && points > 0 &&pointsflag != 1) 
        { 
            gameManager.levelSetup.EvtSatisfactionLvl1();
            pointsflag = 1;
        }
        else if (points < 30 && points > 15 && pointsflag != 2)
        {
            gameManager.levelSetup.EvtSatisfactionLvl2();
            pointsflag = 2;
        }
        else if (points < 45 && points > 30 &&pointsflag != 3)
        {
            gameManager.levelSetup.EvtSatisfactionLvl3();
            pointsflag = 3;
        }
        else if (points < 60 && points > 45 &&pointsflag != 4)
        {
            gameManager.levelSetup.EvtSatisfactionClimax();
            pointsflag = 4;
        }
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
