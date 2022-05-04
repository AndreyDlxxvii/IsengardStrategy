using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using ResurseSystem;

public class TopResUiVew : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI woodCount;
    [SerializeField]
    private TextMeshProUGUI ironCount;
    [SerializeField]
    private TextMeshProUGUI deercount;
    [SerializeField]
    private TextMeshProUGUI horsecount;
    [SerializeField]
    private TextMeshProUGUI textilecount;
    [SerializeField]
    private TextMeshProUGUI steelcount;
    [SerializeField]
    private TextMeshProUGUI magikStonescount;

   
    public void UpdateResursesCount(ResurseType type,int count)
    {
        switch(type)
        {
            case ResurseType.Wood:
                woodCount.text = $"{count}";
                break;
            case ResurseType.Iron:
                ironCount.text = $"{count}";
                break;
            case ResurseType.Deer:
                deercount.text = $"{count}";
                break;
            case ResurseType.Horse:
                horsecount.text = $"{count}";
                break;
            case ResurseType.Textile:
                textilecount.text = $"{count}";
                break;
            case ResurseType.Steele:
                steelcount.text = $"{count}";
                break;
            case ResurseType.MagikStones:
                magikStonescount.text = $"{count}";
                break;
        }
    }
}
