using System;
using TMPro;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI letterText;
    [SerializeField] private Image mainImage;
    [SerializeField] private TileData tileData;

    public void InitialiseTile(TileData tileData)
    {
        this.tileData = tileData;
        SetLetterTextUI();
    }

    private void SetLetterTextUI()
    {
        letterText.text = tileData.letter.ToString();
    }

    public TileData GetTileData()
    {
        if(tileData == null)
            Debug.LogError("Tile Data is null");

        return tileData;

    }

    public void SetSelected(bool isSelected)
    {
        if(isSelected)
            Highlight();
        else
            DeHighlight();
    }

    
    private void Highlight() => mainImage.color = Color.green;

    private void DeHighlight() => mainImage.color = Color.white;

    public void DestroyTile()
    {
        Destroy(gameObject);
    }
}
