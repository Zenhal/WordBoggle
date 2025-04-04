using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    public int rows = 4;
    public int columns = 4;
    public GameObject letterTilePrefab;
    public Transform boardParent;
    private char[,] board;

    private string[] boggleDice = new string[]
    {
        "AAEEGN", "ELRTTY", "AOOTTW", "ABBJOO", "EHRTVW", "CIMOTU",
        "DISTTY", "EIOSST", "DELRVY", "ACHOPS", "HIMNQU", "EEINSU",
        "EEGHNW", "AFFKPS", "HLNNRZ", "DEILRX"
    };

    public void Init()
    {
        GenerateBoard();
    }
    
    

    private void GenerateBoard()
    {
        board = new char[rows, columns];
        List<string> diceList = new List<string>(boggleDice);
        System.Random rand = new System.Random();

        float tileSize = 220f;
        float boardWidth = columns * tileSize;
        float boardHeight = rows * tileSize;
        Vector2 startPosition = new Vector2(-boardWidth / 2 + tileSize / 2, boardHeight / 2 - tileSize / 2);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                //int diceIndex = rand.Next(diceList.Count);
                //string dice = diceList[diceIndex];
                //board[i, j] = dice[rand.Next(dice.Length)];
                //diceList.RemoveAt(diceIndex);

                Vector2 position = new Vector2(startPosition.x + j * tileSize, startPosition.y - i * tileSize);
                SpawnTile(board[i, j], position);
            }
        }
    }

    private void SpawnTile(char letter, Vector2 position)
    {
        GameObject tileGameObject = Instantiate(letterTilePrefab, boardParent);
        tileGameObject.transform.localPosition = position;
        var tile = tileGameObject.GetComponent<Tile>();
        tile.InitialiseTile(GetTileData());
    }

    private TileData GetTileData()
    {
        var tileData = new TileData();
        tileData.tileType = "0";
        tileData.score = 1;
        tileData.letter = GetRandomLetter();
        return tileData;
    }
    
    private char GetRandomLetter()
    {
        return (char)('A' + Random.Range(0, 26));
    }
}
