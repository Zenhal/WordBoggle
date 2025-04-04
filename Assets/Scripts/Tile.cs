using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI letterText;
    [SerializeField] private Image mainImage;
    [SerializeField, ReadOnly] private TileData tileData;
    
    private bool isSelected;

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

    public void SetSelected(bool b)
    {
        isSelected = b;
        if(isSelected)
            Highlight();
        else
            DeHighlight();
    }

    
    private void Highlight() => mainImage.color = Color.green;

    private void DeHighlight() => mainImage.color = Color.white;
}
