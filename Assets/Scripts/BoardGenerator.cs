using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private int rows = 4;
    [SerializeField] private int columns = 4;
    [SerializeField] private GameObject letterTilePrefab;
    [SerializeField] private Transform boardParent;

    private List<Tile> boardTiles;

    public void Init(LevelData levelData)
    {
        if (levelData == null) return;
        
        boardTiles = new List<Tile>();
        SetGridSize(levelData.gridSize.x, levelData.gridSize.y);
        GenerateBoard();
        InitialiseTileData(levelData.gridData);
    }

    private void SetGridSize(int row, int column)
    {
        rows = row;
        columns = column;
    }

    public void InitialiseRandomBoard(int row, int column)
    {
        boardTiles = new List<Tile>();
        SetGridSize(row, column);
        GenerateBoard();
        InitializeRandomData();
    }
    
    private void GenerateBoard()
    {
        var tileSize = 220f;
        var boardWidth = columns * tileSize;
        var boardHeight = rows * tileSize;
        var startPosition = new Vector2(-boardWidth / 2 + tileSize / 2, boardHeight / 2 - tileSize / 2);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector2 position = new Vector2(startPosition.x + j * tileSize, startPosition.y - i * tileSize);
                SpawnTile(i, j, position);
            }
        }
    }

    private void SpawnTile(int row, int column, Vector2 position)
    {
        GameObject tileGameObject = Instantiate(letterTilePrefab, boardParent);
        tileGameObject.transform.localPosition = position;
        var tile = tileGameObject.GetComponent<Tile>();
        tile.SetRowColumn(row, column);
        boardTiles.Add(tile);
    }

    private void InitialiseTileData(List<GridData> gridData)
    {
        for (int i = 0; i < gridData.Count; i++)
        {
            boardTiles[i].InitialiseTile(GetTileData(gridData[i].tileType, gridData[i].letter));
        }
    }

    private void InitializeRandomData()
    {
        for (int i = 0; i < boardTiles.Count; i++)
        {
            boardTiles[i].InitialiseTile(GetTileData(0, GetRandomLetter().ToString()));
        }
    }

    public void SetNewTileData(List<Tile> tiles)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].InitialiseTile(GetTileData(0, GetRandomLetter().ToString()));
        }
    }

    private TileData GetTileData(int tileType, string letter)
    {
        var tileData = new TileData();
        tileData.tileType = tileType;
        tileData.letter = letter;
        return tileData;
    }

    public void ClearBoard()
    {
        for (int i = 0; i < boardTiles.Count; i++)
        {
            Destroy(boardTiles[i].gameObject);
        }
        boardTiles.Clear();
    }
    
    private char GetRandomLetter()
    {
        return (char)('A' + Random.Range(0, 26));
    }
}
