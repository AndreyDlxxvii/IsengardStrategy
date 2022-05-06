using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnLeftUIController : IOnController, IDisposable
{
    private LeftUI _leftUI;
    private GameConfig _gameConfig;
    private List<Button> _buttons = new List<Button>();
    
    public event Action<Building> BuildingSelected;
    public BtnLeftUIController(LeftUI leftUI, GameConfig gameConfig)
    {
        _leftUI = leftUI;
        _gameConfig = gameConfig;
        for (int i = 0; i < _gameConfig.Buildings.Count; i++)
        {
            var building = _gameConfig.Buildings[i];
            Button btn = GameObject.Instantiate(_leftUI.ButtonPrefab, _leftUI.ContentRectTransform);
            btn.onClick.AddListener(() => BuildingSelected(building));
            _buttons.Add(btn);
        }
    }

    public void Dispose()
    {
        foreach (var btn in _buttons)
        {
            btn.onClick.RemoveAllListeners();
        }
    }
}