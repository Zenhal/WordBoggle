using System.Collections.Generic;

public class EndlessMode : GameMode
{
    private BoardGenerator boardGenerator;
    public EndlessMode(BoardGenerator boardGenerator)
    {
        this.boardGenerator = boardGenerator;
    }
    public override void InitializeGame(LevelData levelData)
    {
        
    }

    public override void ProcessValidWord(List<Tile> wordTiles, string word)
    {
        RegisterWord(word);
        AddScore(CalculateScore(word));
        EventHandler.Instance.OnObjectiveUpdated.Invoke(currentScore, wordsAdded.Count);
        boardGenerator.SetNewTileData(wordTiles);
    }
    
    public override void Update()
    {
        
    }

    public override void EndGame(bool isWin)
    {
        
    }
    

    public virtual void RestartGame()
    {
        
    }
}
