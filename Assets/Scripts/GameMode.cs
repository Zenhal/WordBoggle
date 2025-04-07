using System.Collections.Generic;

public abstract class GameMode
{
    private LevelData currentLevelData;

    protected int currentScore = 0;
    protected List<string> wordsAdded = new List<string>();

    public abstract void InitializeGame(LevelData levelData);
    public abstract void ProcessValidWord(List<Tile> wordTiles, string word);
    public abstract void Update();
    public abstract void EndGame(bool isWin);

    protected virtual void AddScore(int points)
    {
        currentScore += points;
    }

    protected virtual void RegisterWord(string word)
    {
        wordsAdded.Add(word);
    }

    public virtual void RestartGame()
    {
        currentScore = 0;
        wordsAdded.Clear();
    }
    
    //TODO: Add correct logic
    protected virtual int CalculateScore(string word)
    {
        int length = word.Length;

        if (length == 3) return 10;
        if (length == 4) return 20;
        if (length == 5) return 40;
        if (length == 6) return 60;
        if (length == 7) return 90;
        return 200; // 8+ letters
    }
}