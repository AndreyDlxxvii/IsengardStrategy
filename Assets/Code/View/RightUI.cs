using System;
using UnityEngine;
using UnityEngine.UI;

public class RightUI : MonoBehaviour
{
    [SerializeField] private Button _buttonSelectTileFirst;
    [SerializeField] private Button _buttonSelectTileSecond;
    [SerializeField] private Button _buttonSelectTileThird;

    public Button ButtonSelectTileFirst => _buttonSelectTileFirst;

    public Button ButtonSelectTileSecond => _buttonSelectTileSecond;

    public Button ButtonSelectTileThird => _buttonSelectTileThird;
}
