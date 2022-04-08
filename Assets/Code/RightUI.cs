using System;
using UnityEngine;
using UnityEngine.UI;

public class RightUI : MonoBehaviour
{
    [SerializeField] private Button buttonSelectTileFirst;
    [SerializeField] private Button buttonSelectTileSecond;
    [SerializeField] private Button buttonSelectTileThird;

    public Sprite FirstBtnSprite
    {
        set => _firstBtnSprite = value;
    }

    public Sprite SecondBtnSprite
    {
        set => _secondBtnSprite = value;
    }

    public Sprite ThirdBtnSprite
    {
        set => _thirdBtnSprite = value;
    }

    private Sprite _firstBtnSprite;
    private Sprite _secondBtnSprite;
    private Sprite _thirdBtnSprite;
    
    public event Action<int> TileSelected;
    
    void Start()
    {
        buttonSelectTileFirst.image.sprite = _firstBtnSprite;
        buttonSelectTileSecond.image.sprite = _secondBtnSprite;
        buttonSelectTileThird.image.sprite = _thirdBtnSprite;
        
        buttonSelectTileFirst.onClick.AddListener( () => TileSelected?.Invoke(0));
        buttonSelectTileSecond.onClick.AddListener(() => TileSelected?.Invoke(1));
        buttonSelectTileThird.onClick.AddListener(() => TileSelected?.Invoke(2));
    }

    private void OnDestroy()
    {
        buttonSelectTileFirst.onClick.RemoveAllListeners();
        buttonSelectTileSecond.onClick.RemoveAllListeners();
        buttonSelectTileThird.onClick.RemoveAllListeners();
    }
}
