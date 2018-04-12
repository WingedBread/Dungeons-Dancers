public enum ScreenStates
{
    MainMenu,
    WorldMap,
    Level,
    LevelScore
}

public enum LevelStates
{
    LevelStart,
    LevelPaused,
    LevelPlay,
    LevelEnd
}

public enum LevelEvents
{
    IntroStart, IntroEnd, StartPlay, OnBeat, BeatBehaviour, OnCheckpoint, WinLevel, GetSparkle, GetKey, TimeNearOver,
    TimeOver, SatisfactionZero, SatisfactionLvl1, SatisfactionLvl2, SatisfactionLvl3, SatisfactionClimax, OnHit, PerfectMove, GoodMove, WrongMove, OnShoot
}

public enum PlayerStates
{
    Dancing,
    Hit,
    Succeed
}


