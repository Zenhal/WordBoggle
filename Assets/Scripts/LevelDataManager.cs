using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class LevelDataManager
{
    private LevelDataList levelDataList;
    private static string LEVELDATA_FILE_PATH = "Assets/Resources/levelData.json";

    public LevelDataManager()
    {
        LoadLevelData();
    }
    private void LoadLevelData()
    {
        var text = File.ReadAllText(LEVELDATA_FILE_PATH);
        levelDataList = JsonConvert.DeserializeObject<LevelDataList>(text);
        Debug.Log("Level Data : " + levelDataList.data.Count);
    }
    
    public LevelDataList GetLevelDataList() => levelDataList;
}
