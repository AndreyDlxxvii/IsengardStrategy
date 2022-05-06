using UnityEngine;
using UnityEngine.UI;

public class LeftUI : MonoBehaviour
{
    [SerializeField] private RectTransform _contentRectTransform;
    [SerializeField] private Button _buttonPrefab;

    public Button ButtonPrefab => _buttonPrefab;

    public RectTransform ContentRectTransform => _contentRectTransform;
}