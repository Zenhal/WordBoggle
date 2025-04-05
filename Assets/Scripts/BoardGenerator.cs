using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private int rows = 4;
    [SerializeField] private int columns = 4;
    [SerializeField] private GameObject letterTilePrefab;
    [SerializeField] private Transform boardParent;


    public void Init()
    {
        GenerateBoard();
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
                SpawnTile(position);
            }
        }
    }

    private void SpawnTile(Vector2 position)
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
