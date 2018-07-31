﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SatisfactionController))]
[RequireComponent(typeof(IntroController))]
[RequireComponent(typeof(AudioController))]
[RequireComponent(typeof(RhythmController))]
[RequireComponent(typeof(UIController))]
public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public List<LevelEventsAudio> levelEventsAudios;
    [HideInInspector]
    public List<LevelEventsEasing_01> levelEventsEasing1;
    [HideInInspector]
    public List<LevelEventsEasing_02> levelEventsEasing2;
    [HideInInspector]
    public List<LevelEventsEasing_03> levelEventsEasing3;
    [HideInInspector]
    public List<LevelEventsEasing_04> levelEventsEasing4;
    [HideInInspector]
    public List<LevelEventsMaterial> levelEventsMaterials;
    [HideInInspector]
    public List<LevelEventsAmbientColor> levelEventsAmbientColors;
    [HideInInspector]
    public List<LevelEventsLightsColor> levelEventsLightsColors;

    [Header("FPS Settings")]
    [SerializeField]
    private bool _allowFpsCap = false;
    [SerializeField]
    private int _fpsApplication = 0;

    [Header("Player Manager")]
    [SerializeField]
    private PlayerManager playerManager;

    [Header("Controllers")]
    private UIController uiController;
    private RhythmController rhythmController;
    private AudioController auController;
    private SatisfactionController satisController;
    private IntroController introController;

    [Header("God Mode (?)")]
    [SerializeField]
    private bool godMode = false;

    private bool gameStart = false;

    [Header("Time Section")]
    [Header("Dungeon Timer in Seconds")]
    [SerializeField]
    private float initDungeonTimer = 60f;
    private float dungeonTimer;


    bool flagTimeNearOver = true;

    private string[] cancelText = new string[3];

    private void Awake()
    {
        if(_allowFpsCap) Application.targetFrameRate = _fpsApplication;
        //DontDestroyOnLoad(this.gameObject);

        #if UNITY_STANDALONE_WIN
            cancelText[0] = "Cancel_WIN";
            cancelText[1] = "Cancel_DDR";
            cancelText[2] = "Cancel_SNES";
        #elif UNITY_STANDALONE_OSX
            cancelText[0] = "Cancel_MACOS";
            cancelText[1] = "Cancel_DDR";
            cancelText[2] = "Cancel_SNES";
        #endif
    }

    // Use this for initialization
    void Start()
    {
        uiController = GetComponent<UIController>();
        rhythmController = GetComponent<RhythmController>();
        auController = GetComponent<AudioController>();
        satisController = GetComponent<SatisfactionController>();
        introController = GetComponent<IntroController>();
        dungeonTimer = initDungeonTimer;
        auController.PointsSnapshotCheck(6);
        SetIntroCounter(0);
        uiController.SparklesUI(0);
        PlayerPrefs.SetInt("TotalScore", 0);
    }

    void Update()
    {
        if (Input.GetButtonDown(cancelText[PlayerPrefs.GetInt("ControllerType")]))
        {
            Pause();
        }

        if (gameStart)
        {
            if (playerManager.GetBlock() == true) playerManager.SetBlock(false);
            dungeonTimer -= Time.deltaTime;
            if (dungeonTimer <= 0)
            {
                for (int i = 0; i < levelEventsAudios.Count; i++)
                {
                    levelEventsAudios[i].TimeOver();
                }
				for (int i = 0; i < levelEventsMaterials.Count; i++)
                {
					levelEventsMaterials[i].TimeOver();
                }
                for (int i = 0; i < levelEventsAmbientColors.Count; i++)
                {
                    levelEventsAmbientColors[i].TimeOver();
                }
                for (int i = 0; i < levelEventsLightsColors.Count; i++)
                {
                    levelEventsLightsColors[i].TimeOver();
                }
                for (int i = 0; i < levelEventsEasing1.Count; i++)
                {
                    levelEventsEasing1[i].TimeOver();
                }
                for (int i = 0; i < levelEventsEasing2.Count; i++)
                {
                    levelEventsEasing2[i].TimeOver();
                }
                for (int i = 0; i < levelEventsEasing3.Count; i++)
                {
                    levelEventsEasing3[i].TimeOver();
                }
                for (int i = 0; i < levelEventsEasing4.Count; i++)
                {
                    levelEventsEasing4[i].TimeOver();
                }
                Dead();
                dungeonTimer = initDungeonTimer;
            }

            if (dungeonTimer < 5 && flagTimeNearOver)
            {
                for (int i = 0; i < levelEventsAudios.Count; i++)
                {
                    levelEventsAudios[i].TimeNearOver();
                }
				for (int i = 0; i < levelEventsMaterials.Count; i++)
                {
					levelEventsMaterials[i].TimeNearOver();
                }
                for (int i = 0; i < levelEventsAmbientColors.Count; i++)
                {
                    levelEventsAmbientColors[i].TimeNearOver();
                }
                for (int i = 0; i < levelEventsLightsColors.Count; i++)
                {
                    levelEventsLightsColors[i].TimeNearOver();
                }
                for (int i = 0; i < levelEventsEasing1.Count; i++)
                {
                    levelEventsEasing1[i].TimeNearOver();
                }
                for (int i = 0; i < levelEventsEasing2.Count; i++)
                {
                    levelEventsEasing2[i].TimeNearOver();
                }
                for (int i = 0; i < levelEventsEasing3.Count; i++)
                {
                    levelEventsEasing3[i].TimeNearOver();
                }
                for (int i = 0; i < levelEventsEasing4.Count; i++)
                {
                    levelEventsEasing4[i].TimeNearOver();
                }
                flagTimeNearOver = false;
            }
        }
        else playerManager.SetBlock(true);

        //if(Input.GetKeyDown(KeyCode.P)){
        //    Photo();
        //}
    }

    void Photo()
    {
        ScreenCapture.CaptureScreenshot("Photo.png", 16);
    }

    public void IntroBehaviour(int intro)
    {
        uiController.IntroUICheck(intro);
        if (GetIntroCounter() == 6)
        {
            for (int i = 0; i < levelEventsAudios.Count; i++)
            {
                levelEventsAudios[i].SetLevelState(LevelStates.LevelPlay);
                levelEventsAudios[i].IntroEnd();
                levelEventsAudios[i].StartPlay();
            }
			for (int i = 0; i < levelEventsMaterials.Count; i++)
            {
				levelEventsMaterials[i].SetLevelState(LevelStates.LevelPlay);
				levelEventsMaterials[i].IntroEnd();
				levelEventsMaterials[i].StartPlay();
            }
            for (int i = 0; i < levelEventsAmbientColors.Count; i++)
            {
                levelEventsAmbientColors[i].SetLevelState(LevelStates.LevelPlay);
                levelEventsAmbientColors[i].IntroEnd();
                levelEventsAmbientColors[i].StartPlay();
            }
            for (int i = 0; i < levelEventsLightsColors.Count; i++)
            {
                levelEventsLightsColors[i].SetLevelState(LevelStates.LevelPlay);
                levelEventsLightsColors[i].IntroEnd();
                levelEventsLightsColors[i].StartPlay();
            }
            for (int i = 0; i < levelEventsEasing1.Count; i++)
            {
                levelEventsEasing1[i].SetLevelState(LevelStates.LevelPlay);
                levelEventsEasing1[i].IntroEnd();
                levelEventsEasing1[i].StartPlay();
            }
            for (int i = 0; i < levelEventsEasing2.Count; i++)
            {
                levelEventsEasing2[i].SetLevelState(LevelStates.LevelPlay);
                levelEventsEasing2[i].IntroEnd();
                levelEventsEasing2[i].StartPlay();
            }
            for (int i = 0; i < levelEventsEasing3.Count; i++)
            {
                levelEventsEasing3[i].SetLevelState(LevelStates.LevelPlay);
                levelEventsEasing3[i].IntroEnd();
                levelEventsEasing3[i].StartPlay();
            }
            for (int i = 0; i < levelEventsEasing4.Count; i++)
            {
                levelEventsEasing4[i].SetLevelState(LevelStates.LevelPlay);
                levelEventsEasing4[i].IntroEnd();
                levelEventsEasing4[i].StartPlay();
            }
            gameStart = true;
            auController.PointsSnapshotCheck(1);
            rhythmController.SetIntroRhythm(false);
            rhythmController.SetRhythm(true);
            satisController.PointEvents();
        }
    }

    private void Pause()
    {
        if (gameStart)
        {
            for (int i = 0; i < levelEventsAudios.Count; i++)
            {
                levelEventsAudios[i].SetLevelState(LevelStates.LevelPaused);
            }
            for (int i = 0; i < levelEventsMaterials.Count; i++)
            {
                levelEventsMaterials[i].SetLevelState(LevelStates.LevelPaused);
            }
            for (int i = 0; i < levelEventsAmbientColors.Count; i++)
            {
                levelEventsAmbientColors[i].SetLevelState(LevelStates.LevelPaused);
            }
            for (int i = 0; i < levelEventsLightsColors.Count; i++)
            {
                levelEventsLightsColors[i].SetLevelState(LevelStates.LevelPaused);
            }
            for (int i = 0; i < levelEventsEasing1.Count; i++)
            {
                levelEventsEasing1[i].SetLevelState(LevelStates.LevelPaused);
            }
            for (int i = 0; i < levelEventsEasing2.Count; i++)
            {
                levelEventsEasing2[i].SetLevelState(LevelStates.LevelPaused);
            }
            for (int i = 0; i < levelEventsEasing3.Count; i++)
            {
                levelEventsEasing3[i].SetLevelState(LevelStates.LevelPaused);
            }
            for (int i = 0; i < levelEventsEasing4.Count; i++)
            {
                levelEventsEasing4[i].SetLevelState(LevelStates.LevelPaused);
            }
            gameStart = false;
            auController.MuteSound();
            uiController.PauseUI();
        }

        else if(!gameStart)
        {
            for (int i = 0; i < levelEventsAudios.Count; i++)
            {
                levelEventsAudios[i].SetLevelState(LevelStates.LevelPlay);
            }
            for (int i = 0; i < levelEventsMaterials.Count; i++)
            {
                levelEventsMaterials[i].SetLevelState(LevelStates.LevelPlay);
            }
            for (int i = 0; i < levelEventsAmbientColors.Count; i++)
            {
                levelEventsAmbientColors[i].SetLevelState(LevelStates.LevelPlay);
            }
            for (int i = 0; i < levelEventsLightsColors.Count; i++)
            {
                levelEventsLightsColors[i].SetLevelState(LevelStates.LevelPlay);
            }
            for (int i = 0; i < levelEventsEasing1.Count; i++)
            {
                levelEventsEasing1[i].SetLevelState(LevelStates.LevelPlay);
            }
            for (int i = 0; i < levelEventsEasing2.Count; i++)
            {
                levelEventsEasing2[i].SetLevelState(LevelStates.LevelPlay);
            }
            for (int i = 0; i < levelEventsEasing3.Count; i++)
            {
                levelEventsEasing3[i].SetLevelState(LevelStates.LevelPlay);
            }
            for (int i = 0; i < levelEventsEasing4.Count; i++)
            {
                levelEventsEasing4[i].SetLevelState(LevelStates.LevelPlay);
            }
            gameStart = true;
            auController.UnmuteSound();
            uiController.ResetUI();
        }
    }

    public void Win()
    {
        for (int i = 0; i < levelEventsAudios.Count; i++)
        {
            levelEventsAudios[i].SetLevelState(LevelStates.LevelEnd);
            levelEventsAudios[i].WinLevel();
            levelEventsAudios[i].Reset();
        }
		for (int i = 0; i < levelEventsMaterials.Count; i++)
        {
			levelEventsMaterials[i].SetLevelState(LevelStates.LevelEnd);
			levelEventsMaterials[i].WinLevel();
        }
        for (int i = 0; i < levelEventsAmbientColors.Count; i++)
        {
            levelEventsAmbientColors[i].SetLevelState(LevelStates.LevelEnd);
            levelEventsAmbientColors[i].WinLevel();
        }
        for (int i = 0; i < levelEventsLightsColors.Count; i++)
        {
            levelEventsLightsColors[i].SetLevelState(LevelStates.LevelEnd);
            levelEventsLightsColors[i].WinLevel();
        }
        for (int i = 0; i < levelEventsEasing1.Count; i++)
        {
            levelEventsEasing1[i].SetLevelState(LevelStates.LevelEnd);
            levelEventsEasing1[i].WinLevel();
        }
        for (int i = 0; i < levelEventsEasing2.Count; i++)
        {
            levelEventsEasing2[i].SetLevelState(LevelStates.LevelEnd);
            levelEventsEasing2[i].WinLevel();
        }
        for (int i = 0; i < levelEventsEasing3.Count; i++)
        {
            levelEventsEasing3[i].SetLevelState(LevelStates.LevelEnd);
            levelEventsEasing3[i].WinLevel();
        }
        for (int i = 0; i < levelEventsEasing4.Count; i++)
        {
            levelEventsEasing4[i].SetLevelState(LevelStates.LevelEnd);
            levelEventsEasing4[i].WinLevel();
        }
        gameStart = false;
        rhythmController.SetRhythm(false);
        uiController.WinUI();
        auController.MuteSound();
        PlayerPrefs.SetInt("TotalScore", playerManager.GetSparkles());
    }
   

    public void Dead()
    {
        if (!godMode)
        {
            gameStart = false;
            rhythmController.SetRhythm(false);
            auController.MuteSound();
            uiController.DeadUI();
            playerManager.Lose();
        }
    }

    public void GameDeadReset()
    {
        dungeonTimer = initDungeonTimer;
        flagTimeNearOver = true;
        for (int i = 0; i < levelEventsAudios.Count; i++)
        {
            levelEventsAudios[i].SetLevelState(LevelStates.LevelStart);
            levelEventsAudios[i].Reset();
        }
		for (int i = 0; i < levelEventsMaterials.Count; i++)
        {
			levelEventsMaterials[i].SetLevelState(LevelStates.LevelStart);
        }
        for (int i = 0; i < levelEventsAmbientColors.Count; i++)
        {
            levelEventsAmbientColors[i].SetLevelState(LevelStates.LevelStart);
        }
        for (int i = 0; i < levelEventsLightsColors.Count; i++)
        {
            levelEventsLightsColors[i].SetLevelState(LevelStates.LevelStart);
        }
        for (int i = 0; i < levelEventsEasing1.Count; i++)
        {
            levelEventsEasing1[i].SetLevelState(LevelStates.LevelStart);
        }
        for (int i = 0; i < levelEventsEasing2.Count; i++)
        {
            levelEventsEasing2[i].SetLevelState(LevelStates.LevelStart);
        }
        for (int i = 0; i < levelEventsEasing3.Count; i++)
        {
            levelEventsEasing3[i].SetLevelState(LevelStates.LevelStart);
        }
        for (int i = 0; i < levelEventsEasing4.Count; i++)
        {
            levelEventsEasing4[i].SetLevelState(LevelStates.LevelStart);
        }
        satisController.ResetSatisfaction();
        auController.UnmuteSound();
        auController.PointsSnapshotCheck(6);
        uiController.ResetUI();
        uiController.SparklesUI(0);
        uiController.CollectibleUI(0);
        SetIntroCounter(0);
        rhythmController.SetIntroRhythm(true);
        StopCoroutine("Reset");
    }

    public void AddPoint()
    {
        satisController.AddPoint();
        uiController.AddPointUI();
        auController.PointsSnapshotCheck(satisController.pointsflag);
    }

    public void RemovePoint()
    {
        satisController.RemovePoint();
        uiController.RemovePointUI();
        auController.PointsSnapshotCheck(satisController.pointsflag);
    }

    public void SparkleBehaviour(int sparkles)
    {
        uiController.SparklesUI(sparkles);
    }
    public void CollectibleBehaviour(int collectible)
    {
        uiController.CollectibleUI(collectible);
    }
    public void DoorBehaviour()
    {
        for (int i = 0; i < levelEventsAudios.Count; i++)
        {
            levelEventsAudios[i].Door();
        }
		for (int i = 0; i < levelEventsMaterials.Count; i++)
        {
			levelEventsMaterials[i].Door();
        }
        for (int i = 0; i < levelEventsAmbientColors.Count; i++)
        {
            levelEventsAmbientColors[i].Door();
        }
        for (int i = 0; i < levelEventsLightsColors.Count; i++)
        {
            levelEventsLightsColors[i].Door();
        }
        for (int i = 0; i < levelEventsEasing1.Count; i++)
        {
            levelEventsEasing1[i].Door();
        }
        for (int i = 0; i < levelEventsEasing2.Count; i++)
        {
            levelEventsEasing2[i].Door();
        }
        for (int i = 0; i < levelEventsEasing3.Count; i++)
        {
            levelEventsEasing3[i].Door();
        }
        for (int i = 0; i < levelEventsEasing4.Count; i++)
        {
            levelEventsEasing4[i].Door();
        }
    }

#region Getters & Setters
    public int GetPoints(int min, int current, int max)
    {
        if (satisController == null) satisController = GetComponent<SatisfactionController>();
        if (min == 1 && current == 0 && max == 0) return satisController.GetSatisfactionPoints(1, 0, 0);
        else if (min == 0 && current == 1 && max == 0) return satisController.GetSatisfactionPoints(0, 1, 0);
        else if (min == 0 && current == 0 && max == 1) return satisController.GetSatisfactionPoints(0, 0, 1);

        else return 0;
    }
    public void CalculateRhythm()
    {
        rhythmController.CalculateTiming();
    }
    public bool GetPlayerBlock()
    {
        return playerManager.GetBlock();
    }

    public bool GetSatisfactionFever()
    {
        return satisController.GetFeverState();
    }

    public int GetIntroCounter()
    {
        return introController.GetCounter();
    }

    public bool GetRhythmActiveInput()
    {
        return rhythmController.ActivePlayerInputEvent();
    }
    public bool GetRhythmActiveBeat()
    {
        return rhythmController.ActivePlayerBeatEvent();
    }

    public int GetRhythmAccuracy()
    {
        return rhythmController.GetAccuracy();
    }

    public bool GetGameStatus()
    {
        return gameStart;
    }

    public bool GetPlayerInputFlag()
    {
        return playerManager.GetPlayerInputFlag();
    }

    public float GetInitDungeonTime(){
        return initDungeonTimer;
    }

    public float GetDungeonTime()
    {
        return dungeonTimer;
    }

    public void SetIntroCounter(int setintro)
    {
        introController.SetCounter(setintro);
    }

    public void SetPlayerBlock(bool block)
    {
        playerManager.SetBlock(block);
    }

    public void PlayIntroClip()
    {
        auController.PlayIntro();
    }
    public float GetSatisfactionTrackPos(int trackpos)
    {
        return satisController.GetTrackPosition(trackpos);
    }

    public void ClimaxUIBehaviour(int feverpoints, bool climax){
        uiController.ClimaxUIBehaviour(feverpoints, climax);
    }

    public void SelectScene(int i)
    {
        SceneManager.LoadScene(i);
    }

#endregion
}
