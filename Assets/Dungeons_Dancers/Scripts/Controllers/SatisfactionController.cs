using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatisfactionController : MonoBehaviour {

    [Header("Game Manager")]
    private GameManager gameManager;

    [Header("Initial Satisfaction")]
    [SerializeField]
    private int initPoints;
    [Header("Minimum Satisfaction")]
    [SerializeField]
    private int minPoints;
    [Header("Maximum Satisfaction")]
    [SerializeField]
    private int maxPoints;
    [Header("Fever Satisfaction")]
    [SerializeField]
    private int initFeverPoints;
    [Header("Soon Satisfaction")]
    [SerializeField]
    private int soonPoints;
    [Header("Perfect Satisfaction")]
    [SerializeField]
    private int perfectPoints;
    [Header("Late Satisfaction")]
    [SerializeField]
    private int latePoints;
    [Header("Amount of Satisfaction Removal")]
    [SerializeField]
    private int removePoints;
    private int points;
    private int feverPoints;

	// Use this for initialization
    void Awake()
    {
        points = initPoints;
        feverPoints = initFeverPoints;
    }
	void Start () {
        gameManager = GetComponent<GameManager>();
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
            if (points <= minPoints) {
                gameManager.Dead();
                points = minPoints;
            }
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

    public int GetSatisfactionPoints(int min, int current, int max)
    {

        if (min == 1 && current == 0 && max == 0) return minPoints;
        else if (min == 0 && current == 1 && max == 0) return points;
        else if (min == 0 && current == 0 && max == 1) return maxPoints;

        else return 0;
    }

    public bool GetFeverState(){
        if (points >= maxPoints) return true;
        else return false;
    }

}
