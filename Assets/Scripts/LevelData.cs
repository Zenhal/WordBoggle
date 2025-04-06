using System;
using System.Collections.Generic;


[Serializable]
public class LevelDataList
{
    public List<LevelData> data;
}

[Serializable]
public class LevelData
{
    public int bugCount { get; set; }
    public int wordCount { get; set; }
    public int timeSec { get; set; }
    public int totalScore { get; set; }
    public GridSize gridSize { get; set; }
    public List<GridData> gridData { get; set; }
    public int? levelType { get; set; }
}

[Serializable]
public class GridData
{
    public int tileType { get; set; }
    public string letter { get; set; }
}

[Serializable]
public class GridSize
{
    public int x { get; set; }
    public int y { get; set; }
}


