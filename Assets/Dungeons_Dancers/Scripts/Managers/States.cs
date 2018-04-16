public enum ScreenStates
{
    MainMenu,
    WorldMap,
    Level,
    LevelScore
}

public enum LevelStates
{
    None,
    LevelStart,
    LevelPaused,
    LevelPlay,
    LevelEnd, 

}

public enum SatisfactionStates
{
    None,
    SatisfactionLvl1,
    SatisfactionLvl2,
    SatisfactionLvl3,
    SatisfactionClimax
}
public enum LevelEvents
{
    IntroStart, IntroEnd, StartPlay, OnBeat, BeatBehaviour, OnCheckpoint, WinLevel, GetSparkle, GetKey, TimeNearOver,
    TimeOver, SatisfactionZero, SatisfactionLvl1, SatisfactionLvl2, SatisfactionLvl3, SatisfactionClimax, OnHit, PerfectMove, GoodMove, WrongMove, OnShoot, Door
}

public enum PlayerStates
{
    None, 
    Dancing,
    Hit,
    Succeed
}


