using System;

public enum LevelObjectiveType
{
    Word,
    Score
}

[Serializable]
public class LevelObjective
{
    public LevelObjectiveType type;
    public int value;
    public int timeValue;
}