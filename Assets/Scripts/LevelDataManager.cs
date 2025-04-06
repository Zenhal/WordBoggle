using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class LevelDataManager
{
    private LevelDataList levelDataList;
    private static string LEVELDATA_FILE_NAME = "levelData";

    public LevelDataManager()
    {
        LoadLevelData();
    }
    private void LoadLevelData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(LEVELDATA_FILE_NAME);
        levelDataList = JsonConvert.DeserializeObject<LevelDataList>(jsonFile.text);
        Debug.Log("Level Data : " + levelDataList.data.Count);
    }
    
    public LevelDataList GetLevelDataList() => levelDataList;
}
