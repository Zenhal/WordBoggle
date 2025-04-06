using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    private InputHandler m_inputHandler;
    private List<Tile> selectedTiles = new List<Tile>();
    private Tile lastHoveredTile;
    
    public InputManager(InputHandler inputHandler)
    {
        m_inputHandler = inputHandler;
        AddInputListeners();
    }

    ~InputManager()
    {
        RemoveInputListeners();
    }
    
     private void AddInputListeners()
    {
        m_inputHandler.OnPointerDownEvent += HandlePointerDown;
        m_inputHandler.OnPointerDragEvent += HandlePointerDrag;
        m_inputHandler.OnPointerUpEvent += HandlePointerUp;
        m_inputHandler.OnDoubleClickEvent += HandleDoubleClick;
    }

    private void HandlePointerDown(Vector2 position, GameObject target)
    {
        Tile tile = GetTileFromGameObject(target);
        if (tile != null)
        {
            lastHoveredTile = tile;

            if (!selectedTiles.Contains(tile))
            {
                ClearSelection();
                AddTileToSelection(tile);
            }
        }
    }

    //TODO: Optimise this logic.
    private void HandlePointerDrag(Vector2 position, GameObject target)
    {
        Tile tile = GetTileFromGameObject(target);
        
        if (tile != null && tile != lastHoveredTile)
        {
            lastHoveredTile = tile;
            
            if (selectedTiles.Count == 0 || IsValidNextTile(selectedTiles[selectedTiles.Count - 1], tile))
            {
                if (selectedTiles.Contains(tile))
                {
                    while (selectedTiles[selectedTiles.Count - 1] != tile)
                    {
                        RemoveLastTileFromSelection();
                    }
                }
                else
                {
                    AddTileToSelection(tile);
                }
            }
        }
    }

    private void HandlePointerUp(Vector2 position, GameObject target)
    {
        if (m_inputHandler.IsDragging() || selectedTiles.Count > 0)
        {
            SubmitWord();
        }
    }

    private void HandleDoubleClick(Vector2 position, GameObject target)
    {
        ClearSelection();
    }

    private Tile GetTileFromGameObject(GameObject target)
    {
        if (target == null) return null;
        return target.GetComponentInParent<Tile>();
    }

    private bool IsValidNextTile(Tile currentTile, Tile nextTile)
    {
        if (currentTile == null || nextTile == null) return false;
        
        int rowDiff = Mathf.Abs(currentTile.x - nextTile.x);
        int colDiff = Mathf.Abs(currentTile.y - nextTile.y);

        return (rowDiff + colDiff) == 1;
    }

    private void AddTileToSelection(Tile tile)
    {
        if (tile == null) return;

        selectedTiles.Add(tile);
        tile.SetSelected(true);
    }

    private void RemoveLastTileFromSelection()
    {
        if (selectedTiles.Count == 0) return;

        Tile lastTile = selectedTiles[selectedTiles.Count - 1];
        lastTile.SetSelected(false);
        selectedTiles.RemoveAt(selectedTiles.Count - 1);
    }

    private void ClearSelection()
    {
        foreach (Tile tile in selectedTiles)
        {
            tile.SetSelected(false);
        }
        selectedTiles.Clear();
    }

    private void SubmitWord()
    {
        if (selectedTiles.Count < 3) // Minimum word length
        {
            ClearSelection();
            return;
        }

        var word = ConstructWordFromSelection();
        EventHandler.Instance.OnWordSubmitted.Invoke(word, selectedTiles);
        ClearSelection();
    }

    private string ConstructWordFromSelection()
    {
        string word = "";
        foreach (Tile tile in selectedTiles)
        {
            word += tile.GetTileData().letter.ToString();
        }
        return word;
    }

    // TODO: For mobile support
    public void OnTileTouched(Tile tile)
    {
        if (!m_inputHandler.IsDragging())
        {
            ClearSelection();
            AddTileToSelection(tile);
        }
    }

    private void RemoveInputListeners()
    {
        m_inputHandler.OnPointerDownEvent -= HandlePointerDown;
        m_inputHandler.OnPointerDragEvent -= HandlePointerDrag;
        m_inputHandler.OnPointerUpEvent -= HandlePointerUp;
        m_inputHandler.OnDoubleClickEvent -= HandleDoubleClick;
    }
    
}
