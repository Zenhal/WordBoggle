using System.IO;
using UnityEngine;

public class CustomLogger : MonoBehaviour
{
    string logFilePath;

    void Awake()
    {
        DontDestroyOnLoad(this);
        logFilePath = Path.Combine(Application.persistentDataPath, "game_log.txt");
        Application.logMessageReceived += HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        string logEntry = $"{System.DateTime.Now}: [{type}] {logString}\n";
        File.AppendAllText(logFilePath, logEntry);
    }

    void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }
}