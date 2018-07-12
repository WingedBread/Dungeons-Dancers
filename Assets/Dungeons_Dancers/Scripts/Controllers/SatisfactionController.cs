using System.Collections.Generic;
using UnityEngine;

public class SatisfactionController : MonoBehaviour {

    [Header("Game Manager")]
    private GameManager gameManager;

    [Header("Climax Confeti")]
    [SerializeField]
    private GameObject climaxConfeti;

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

    private int points;
    private int feverPoints;

    [HideInInspector]
    public int pointsflag = 0;

    bool afterClimax = false;

    // Use this for initialization
    void Awake()
    {
        points = initPoints;
        feverPoints = ClimaxMaxFails;
        afterClimax = false;
    }
	void Start () {
        gameManager = GetComponent<GameManager>();
    }
	
    public void AddPoint()
    {
        //Debug.Log("Ponts Before: " + points);
        if (afterClimax) PlayerPrefs.SetInt("MovesAfterClimax", PlayerPrefs.GetInt("MovesAfterClimax") + 1);

        if (points >= TracksPosition[4])
        {
            FeverState();
        }
        else
        {
            //Debug.Log("Satisfaction Points Accuracy: " + gameManager.GetRhythmAccuracy());
            switch (gameManager.GetRhythmAccuracy())
            {
                case 0:
                    points = points + ScoreGood;
                    //Debug.Log("Ponts After Good: " + points);
                    PlayerPrefs.SetInt("NumGoodMoves", PlayerPrefs.GetInt("NumGoodMoves") + 1);
                    PointEvents();
                    break;
                case 1:
                    points = points + ScorePerfect;
                    //Debug.Log("Ponts After Perfect: " + points);
                    PlayerPrefs.SetInt("NumPerfectMoves", PlayerPrefs.GetInt("NumPerfectMoves") + 1);
                    for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
                    {
                        gameManager.levelEventsAudios[i].PerfectMove();
                    }
					for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
                    {
						gameManager.levelEventsMaterials[i].PerfectMove();
                    }
                    for (int i = 0; i < gameManager.levelEventsAmbientColors.Count; i++)
                    {
                        gameManager.levelEventsAmbientColors[i].PerfectMove();
                    }
                    for (int i = 0; i < gameManager.levelEventsLightsColors.Count; i++)
                    {
                        gameManager.levelEventsLightsColors[i].PerfectMove();
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
                    PointEvents();
                    break;
                case 2:
                    points = points + ScoreGreat;
                    //Debug.Log("Ponts After Great: " + points);
                    PlayerPrefs.SetInt("NumGreatMoves", PlayerPrefs.GetInt("NumGreatMoves") + 1);
                    PointEvents();
                    break;
            }
            if (points >= TracksPosition[4]) 
            { 
                points = (int)TracksPosition[4];
                gameManager.ClimaxUIBehaviour(feverPoints, true);
                //FeverState();
                PointEvents();

                if (!afterClimax)
                {
                    PlayerPrefs.SetInt("MovesAfterClimax", PlayerPrefs.GetInt("MovesAfterClimax") + 1);
                    afterClimax = true;
                }
            }
        }
    }

    public void PointEvents()
    {
        if (points < TracksPosition[2]  && points > TracksPosition[0] && pointsflag != 1) 
        {
            for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
            {
                gameManager.levelEventsAudios[i].SatisfactionLvl1();
                gameManager.levelEventsAudios[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl1);
            }
			for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
            {
				gameManager.levelEventsMaterials[i].SatisfactionLvl1();
				gameManager.levelEventsMaterials[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl1);
            }
            for (int i = 0; i < gameManager.levelEventsAmbientColors.Count; i++)
            {
                gameManager.levelEventsAmbientColors[i].SatisfactionLvl1();
                gameManager.levelEventsAmbientColors[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl1);
            }
            for (int i = 0; i < gameManager.levelEventsLightsColors.Count; i++)
            {
                gameManager.levelEventsLightsColors[i].SatisfactionLvl1();
                gameManager.levelEventsLightsColors[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl1);
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
        else if (points < TracksPosition[3] && points > TracksPosition[2] && pointsflag != 2)
        {
            for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
            {
                gameManager.levelEventsAudios[i].SatisfactionLvl2();
                gameManager.levelEventsAudios[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl2);
            }
			for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
            {
				gameManager.levelEventsMaterials[i].SatisfactionLvl2();
				gameManager.levelEventsMaterials[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl2);
            }
            for (int i = 0; i < gameManager.levelEventsAmbientColors.Count; i++)
            {
                gameManager.levelEventsAmbientColors[i].SatisfactionLvl2();
                gameManager.levelEventsAmbientColors[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl2);
            }
            for (int i = 0; i < gameManager.levelEventsLightsColors.Count; i++)
            {
                gameManager.levelEventsLightsColors[i].SatisfactionLvl2();
                gameManager.levelEventsLightsColors[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl2);
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
        else if (points < TracksPosition[4] && points > TracksPosition[3] &&pointsflag != 3)
        {
            for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
            {
                gameManager.levelEventsAudios[i].SatisfactionLvl3();
                gameManager.levelEventsAudios[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl3);
            }
			for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
            {
				gameManager.levelEventsMaterials[i].SatisfactionLvl3();
				gameManager.levelEventsMaterials[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl3);
            }
            for (int i = 0; i < gameManager.levelEventsAmbientColors.Count; i++)
            {
                gameManager.levelEventsAmbientColors[i].SatisfactionLvl3();
                gameManager.levelEventsAmbientColors[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl3);
            }
            for (int i = 0; i < gameManager.levelEventsLightsColors.Count; i++)
            {
                gameManager.levelEventsLightsColors[i].SatisfactionLvl3();
                gameManager.levelEventsLightsColors[i].SetSatisfactionState(SatisfactionStates.SatisfactionLvl3);
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
        else if (points >= TracksPosition[4] && pointsflag != 4)
        {
            
            for (int i = 0; i < climaxConfeti.transform.childCount; i++)
            {
                climaxConfeti.transform.GetChild(i).GetComponent<ParticleSystem>().Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
                climaxConfeti.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
            } 

            for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
            {
                gameManager.levelEventsAudios[i].SatisfactionClimax();
                gameManager.levelEventsAudios[i].SetSatisfactionState(SatisfactionStates.SatisfactionClimax);
            }
			for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
            {
				gameManager.levelEventsMaterials[i].SatisfactionClimax();
				gameManager.levelEventsMaterials[i].SetSatisfactionState(SatisfactionStates.SatisfactionClimax);
            }
            for (int i = 0; i < gameManager.levelEventsAmbientColors.Count; i++)
            {
                gameManager.levelEventsAmbientColors[i].SatisfactionClimax();
                gameManager.levelEventsAmbientColors[i].SetSatisfactionState(SatisfactionStates.SatisfactionClimax);
            }
            for (int i = 0; i < gameManager.levelEventsLightsColors.Count; i++)
            {
                gameManager.levelEventsLightsColors[i].SatisfactionClimax();
                gameManager.levelEventsLightsColors[i].SetSatisfactionState(SatisfactionStates.SatisfactionClimax);
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
        if (afterClimax) PlayerPrefs.SetInt("MovesAfterClimax", PlayerPrefs.GetInt("MovesAfterClimax") + 1);

        PlayerPrefs.SetInt("NumBadMoves", PlayerPrefs.GetInt("NumBadMoves") + 1);

        if (points >= TracksPosition[4])
        {
            feverPoints--;
            gameManager.ClimaxUIBehaviour(feverPoints, true);
            FeverState();
            PointEvents();
        }
        else
        {
            points = points - ScoreBad;
            //Debug.Log("Ponts After Bad: " + points);
            PointEvents();

            if (points <= TracksPosition[0]) 
            {
                for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
                {
                    gameManager.levelEventsAudios[i].SatisfactionZero();
                    gameManager.levelEventsAudios[i].SetSatisfactionState(SatisfactionStates.None);
                }
                for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
                {
                    gameManager.levelEventsMaterials[i].SatisfactionZero();
                    gameManager.levelEventsMaterials[i].SetSatisfactionState(SatisfactionStates.None);
                }
                for (int i = 0; i < gameManager.levelEventsAmbientColors.Count; i++)
                {
                    gameManager.levelEventsAmbientColors[i].SatisfactionZero();
                    gameManager.levelEventsAmbientColors[i].SetSatisfactionState(SatisfactionStates.None);
                }
                for (int i = 0; i < gameManager.levelEventsLightsColors.Count; i++)
                {
                    gameManager.levelEventsLightsColors[i].SatisfactionZero();
                    gameManager.levelEventsLightsColors[i].SetSatisfactionState(SatisfactionStates.None);
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

        else
        {
            PlayerPrefs.SetInt("MovesInClimax", PlayerPrefs.GetInt("MovesInClimax") + 1);
            switch (gameManager.GetRhythmAccuracy())
            {
                case 0:
                    PlayerPrefs.SetInt("NumGoodMoves", PlayerPrefs.GetInt("NumGoodMoves") + 1);
                    break;
                case 1:
                    PlayerPrefs.SetInt("NumPerfectMoves", PlayerPrefs.GetInt("NumPerfectMoves") + 1);
                    for (int i = 0; i < gameManager.levelEventsAudios.Count; i++)
                    {
                        gameManager.levelEventsAudios[i].PerfectMove();
                    }
                    for (int i = 0; i < gameManager.levelEventsMaterials.Count; i++)
                    {
                        gameManager.levelEventsMaterials[i].PerfectMove();
                    }
                    for (int i = 0; i < gameManager.levelEventsAmbientColors.Count; i++)
                    {
                        gameManager.levelEventsAmbientColors[i].PerfectMove();
                    }
                    for (int i = 0; i < gameManager.levelEventsLightsColors.Count; i++)
                    {
                        gameManager.levelEventsLightsColors[i].PerfectMove();
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
                    PlayerPrefs.SetInt("NumGreatMoves", PlayerPrefs.GetInt("NumGreatMoves") + 1);
                    break;
            }
        }
    }

    public void ResetSatisfaction(){
        points = initPoints;
        feverPoints = ClimaxMaxFails;
        afterClimax = false;
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
