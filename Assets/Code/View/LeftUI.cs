using UnityEngine;
using UnityEngine.UI;

public class LeftUI : MonoBehaviour
{
    [SerializeField] private Button _buildFirstButton;
    [SerializeField] private Button _buildSecondButton;
    
    public Button BuildFirstButton => _buildFirstButton;

    public Button BuildSecondButton => _buildSecondButton;
    
    
}