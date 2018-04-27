using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatisfactionController : MonoBehaviour {

    [Header("Game Manager")]
    private GameManager gameManager;

	// --- Afegit pel Curial (pot ser que acabi modificant-se pel Jesús!) --- //
	[Header("Satisfaction Bar modificators")]
    [SerializeField]
    private int initPoints;
	[SerializeField]
	private int ScoreGood;
	[SerializeField]
	private int ScoreGreat;
	[SerializeField]
	private int ScorePerfect;
	[SerializeField]
	private int ScoreBad;
	[SerializeField]
	private int ClimaxMaxFails;

	[Header("Set track position by Satisfaction Bar percent")] // Canviar els valors hardcodejats a PointsEvents pels d'aquesta llista
	[SerializeField]
    private float[] TracksPosition = new float[5];
    // --- End Afegit pel Curial --- //

    [Header("Separators")]
    [SerializeField]
    private GameObject[] separatorsObj = new GameObject[3];
    //Init 0
    //End 600


    private int points;
    private int feverPoints;

    private int pointsflag = 0;

	// Use this for initialization
    void Awake()
    {
        points = initPoints;
        feverPoints = ClimaxMaxFails;
    }
	void Start () {
        gameManager = GetComponent<GameManager>();
	}
	
    public void AddPoint()
    {
        PointEvents();
        if (points >= TracksPosition[4])
        {
            FeverState();
        }
        else
        {
            switch (gameManager.GetRhythmAccuracy())
            {
                case 0:
                    points = points + ScoreGood;
                    break;
                case 1:
                    points = points + ScorePerfect;
                    for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
                    {
                        gameManager.levelEventsAudios[i].PerfectMove();
                    }
                    for (int i = 0; i < gameManager.levelEventsEasing1.Count; i++)
                    {
                        gameManager.levelEventsEasing1[i].PerfectMove();
                    }
                    for (int i = 0; i < gameManager.levelEventsEasing2.Count; i++)
                    {
                        gameManager.levelEventsEasing2[i].PerfectMove();
                    }
                    for (int i = 0; i < gameManager.levelEventsEasing3.Count; i++)
                    {
                        gameManager.levelEventsEasing3[i].PerfectMove();
                    }
                    for (int i = 0; i < gameManager.levelEventsEasing4.Count; i++)
                    {
                        gameManager.levelEventsEasing4[i].PerfectMove();
                    }
                    break;
                case 2:
                    points = points + ScoreGreat;
                    break;
            }
            if (points >= TracksPosition[4]) 
            { 
                points = (int)TracksPosition[4]; 
                gameManager.ClimaxUIBehaviour(feverPoints, true); 
            }
        }
    }

    void PointEvents()
    {
        if (points <= TracksPosition[0] && pointsflag != 0)
        {
            for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
            {
                gameManager.levelEventsAudios[i].SatisfactionZero();
                gameManager.levelEventsAudios[i].SetSatisfactionState(SatisfactionStates.None);
            }
            for (int i = 0; i < gameManager.levelEventsEasing1.Count; i++)
            {
                gameManager.levelEventsEasing1[i].SatisfactionZero();
                gameManager.levelEventsEasing1[i].SetSatisfactionState(SatisfactionStates.None);
            }
            for (int i = 0; i < gameManager.levelEventsEasing2.Count; i++)
            {
                gameManager.levelEventsEasing2[i].SatisfactionZero();
                gameManager.levelEventsEasing2[i].SetSatisfactionState(SatisfactionStates.None);
            }
            for (int i = 0; i < gameManager.levelEventsEasing3.Count; i++)
            {
                gameManager.levelEventsEasing3[i].SatisfactionZero();
                gameManager.levelEventsEasing3[i].SetSatisfactionState(SatisfactionStates.None);
            }
            for (int i = 0; i < gameManager.levelEventsEasing4.Count; i++)
            {
                gameManager.levelEventsEasing4[i].SatisfactionZero();
                gameManager.levelEventsEasing4[i].SetSatisfactionState(SatisfactionStates.None);
            }
            pointsflag = 0;
        }
        else if (points < TracksPosition[1]  && points > TracksPosition[0] && pointsflag != 1) 
        { 
            for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
            {
                gameManager.levelEventsAudios[i].SatisfactionLvl1();
                gameManager.levelEventsAudios[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl1);
            }
            for (int i = 0; i < gameManager.levelEventsEasing1.Count; i++)
            {
                gameManager.levelEventsEasing1[i].SatisfactionLvl1();
                gameManager.levelEventsEasing1[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl1);
            }
            for (int i = 0; i < gameManager.levelEventsEasing2.Count; i++)
            {
                gameManager.levelEventsEasing2[i].SatisfactionLvl1();
                gameManager.levelEventsEasing2[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl1);
            }
            for (int i = 0; i < gameManager.levelEventsEasing3.Count; i++)
            {
                gameManager.levelEventsEasing3[i].SatisfactionLvl1();
                gameManager.levelEventsEasing3[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl1);
            }
            for (int i = 0; i < gameManager.levelEventsEasing4.Count; i++)
            {
                gameManager.levelEventsEasing4[i].SatisfactionLvl1();
                gameManager.levelEventsEasing4[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl1);
            }
            pointsflag = 1;
        }
        else if (points < TracksPosition[2] && points > TracksPosition[1] && pointsflag != 2)
        {
            for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
            {
                gameManager.levelEventsAudios[i].SatisfactionLvl2();
                gameManager.levelEventsAudios[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl2);
            }
            for (int i = 0; i < gameManager.levelEventsEasing1.Count; i++)
            {
                gameManager.levelEventsEasing1[i].SatisfactionLvl2();
                gameManager.levelEventsEasing1[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl2);
            }
            for (int i = 0; i < gameManager.levelEventsEasing2.Count; i++)
            {
                gameManager.levelEventsEasing2[i].SatisfactionLvl2();
                gameManager.levelEventsEasing2[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl2);
            }
            for (int i = 0; i < gameManager.levelEventsEasing3.Count; i++)
            {
                gameManager.levelEventsEasing3[i].SatisfactionLvl2();
                gameManager.levelEventsEasing3[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl2);
            }
            for (int i = 0; i < gameManager.levelEventsEasing4.Count; i++)
            {
                gameManager.levelEventsEasing4[i].SatisfactionLvl2();
                gameManager.levelEventsEasing4[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl2);
            }
            pointsflag = 2;
        }
        else if (points < TracksPosition[3] && points > TracksPosition[2] &&pointsflag != 3)
        {
            for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
            {
                gameManager.levelEventsAudios[i].SatisfactionLvl3();
                gameManager.levelEventsAudios[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl3);
            }
            for (int i = 0; i < gameManager.levelEventsEasing1.Count; i++)
            {
                gameManager.levelEventsEasing1[i].SatisfactionLvl3();
                gameManager.levelEventsEasing1[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl3);
            }
            for (int i = 0; i < gameManager.levelEventsEasing2.Count; i++)
            {
                gameManager.levelEventsEasing2[i].SatisfactionLvl3();
                gameManager.levelEventsEasing2[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl3);
            }
            for (int i = 0; i < gameManager.levelEventsEasing3.Count; i++)
            {
                gameManager.levelEventsEasing3[i].SatisfactionLvl3();
                gameManager.levelEventsEasing3[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl3);
            }
            for (int i = 0; i < gameManager.levelEventsEasing4.Count; i++)
            {
                gameManager.levelEventsEasing4[i].SatisfactionLvl3();
                gameManager.levelEventsEasing4[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl3);
            }
            pointsflag = 3;
        }
        else if (points < TracksPosition[4] && points > TracksPosition[3] &&pointsflag != 4)
        {
            for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
            {
                gameManager.levelEventsAudios[i].SatisfactionClimax();
                gameManager.levelEventsAudios[i].SetSatisfactionState(SatisfactionStates.SatisfactionClimax);
            }
            for (int i = 0; i < gameManager.levelEventsEasing1.Count; i++)
            {
                gameManager.levelEventsEasing1[i].SatisfactionClimax();
                gameManager.levelEventsEasing1[i].SetSatisfactionState(SatisfactionStates.SatisfactionClimax);
            }
            for (int i = 0; i < gameManager.levelEventsEasing2.Count; i++)
            {
                gameManager.levelEventsEasing2[i].SatisfactionClimax();
                gameManager.levelEventsEasing2[i].SetSatisfactionState(SatisfactionStates.SatisfactionClimax);
            }
            for (int i = 0; i < gameManager.levelEventsEasing3.Count; i++)
            {
                gameManager.levelEventsEasing3[i].SatisfactionClimax();
                gameManager.levelEventsEasing3[i].SetSatisfactionState(SatisfactionStates.SatisfactionClimax);
            }
            for (int i = 0; i < gameManager.levelEventsEasing4.Count; i++)
            {
                gameManager.levelEventsEasing4[i].SatisfactionClimax();
                gameManager.levelEventsEasing4[i].SetSatisfactionState(SatisfactionStates.SatisfactionClimax);
            }
            pointsflag = 4;
        }
    } 

    public void RemovePoint()
    {
        if (points >= TracksPosition[4])
        {
            feverPoints--;
            gameManager.ClimaxUIBehaviour(feverPoints, true);
            FeverState();
        }
        else
        {
            points = points - ScoreBad;
            if (points <= TracksPosition[0]) {
                gameManager.Dead();
                points = (int)TracksPosition[0];
            }
        }
    }

    private void FeverState()
    {
        if (feverPoints <= 0)
        {
            points--;
            feverPoints = ClimaxMaxFails;
            gameManager.ClimaxUIBehaviour(feverPoints, false);
        }
    }

    public void ResetSatisfaction(){
        points = initPoints;
        feverPoints = ClimaxMaxFails;
    }

    public int GetSatisfactionPoints(int min, int current, int max)
    {

        if (min == 1 && current == 0 && max == 0) return (int)TracksPosition[0];
        else if (min == 0 && current == 1 && max == 0) return points;
        else if (min == 0 && current == 0 && max == 1) return (int)TracksPosition[4];

        else return 0;
    }

    public bool GetFeverState(){
        if (points >= TracksPosition[4]) return true;
        else return false;
    }
    /// <summary>
    /// Gets the track position.
    /// </summary>
    /// <returns>The track position.</returns>
    /// <param name="trackpos">Trackpos.</param>
    public float GetTrackPosition(int trackpos)
    {
        return TracksPosition[trackpos];
    }
}
