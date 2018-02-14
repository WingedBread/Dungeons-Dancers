using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatisfactionController : MonoBehaviour {

    [Header("Game Manager")]
    private GameManager gameManager;

    [Header("Initial Satisfaction")]
    [SerializeField]
    private int initPoints = 5;
    [Header("Maximum Satisfaction")]
    [SerializeField]
    private int maxPoints = 20;
    [Header("Fever Satisfaction")]
    [SerializeField]
    private int initFeverPoints = 1;
    [Header("Soon Satisfaction")]
    [SerializeField]
    private int soonPoints = 1;
    [Header("Perfect Satisfaction")]
    [SerializeField]
    private int perfectPoints = 2;
    [Header("Late Satisfaction")]
    [SerializeField]
    private int latePoints = 1;
    [Header("Amount of Satisfaction Removal")]
    [SerializeField]
    private int removePoints = 1;
    private int points = 5;
    private int feverPoints = 1;

	// Use this for initialization
	void Start () {
        gameManager = GetComponent<GameManager>();
        points = initPoints;
        feverPoints = initFeverPoints;
	}
	
    public void AddPoint()
    {
        if (points >= maxPoints)
        {
            FeverState();
        }
        else
        {
            switch (gameManager.GetRhythmAccuracy())
            {
                case 0:
                    points = points + soonPoints;
                    break;
                case 1:
                    points = points + perfectPoints;
                    break;
                case 2:
                    points = points + latePoints;
                    break;
            }
            if (points >= maxPoints) points = maxPoints;
        }
    }

    public void RemovePoint()
    {
        if (points >= maxPoints)
        {
            feverPoints--;
            FeverState();
        }
        else
        {
            points = points - removePoints;
            if (points <= 0) gameManager.Dead();
        }
    }

    private void FeverState()
    {
        if (feverPoints <= 0)
        {
            points--;
            feverPoints = initFeverPoints;
        }
    }

    public void ResetSatisfaction(){
        points = initPoints;
        feverPoints = initFeverPoints;
    }

    public int GetSatisfactionPoints()
    {
        return points;
    }

    public bool GetFeverState(){
        if (points >= maxPoints) return true;
        else return false;
    }

}
