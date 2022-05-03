using ResurseSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsUI : MonoBehaviour
{
    [SerializeField]
    private Image BuildingIconHolder;
    [SerializeField]
    private TextMeshProUGUI BuildingNameHolder;

    [SerializeField]
    private GameObject woodHolder;
    [SerializeField]
    private Image woodIcon;
    [SerializeField]
    private TextMeshProUGUI woodTextholder;

    [SerializeField]
    private GameObject ironholder;
    [SerializeField]
    private Image ironIcon;
    [SerializeField]
    private TextMeshProUGUI ironTextholder;

    [SerializeField]
    private GameObject deersholder;
    [SerializeField]
    private Image deersIcon;
    [SerializeField]
    private TextMeshProUGUI deersTextholder;

    [SerializeField]
    private GameObject horseholder;
    [SerializeField]
    private Image horseIcon;
    [SerializeField]
    private TextMeshProUGUI horseTextholder;

    [SerializeField]
    private GameObject textileholder;
    [SerializeField]
    private Image textileIcon;
    [SerializeField]
    private TextMeshProUGUI textileTextholder;

    [SerializeField]
    private GameObject steelholder;
    [SerializeField]
    private Image steelIcon;
    [SerializeField]
    private TextMeshProUGUI steelTextholder;

    [SerializeField]
    private GameObject magikstonesholder;
    [SerializeField]
    private Image magikStonesIcon;
    [SerializeField]
    private TextMeshProUGUI magikStoneTextholder;   
    
    public void SetWoodValue(Sprite icon, int currvalue,int maxvalue)
    {
        woodIcon.sprite = icon;
        woodTextholder.text = $"{currvalue} / {maxvalue}";
        woodHolder.SetActive(true);
    }
    public void SetIronValue(Sprite icon, int currvalue, int maxvalue)
    {
        ironIcon.sprite = icon;
        ironTextholder.text = $"{currvalue} / {maxvalue}";
        ironholder.SetActive(true);
    }
    public void SetDeersValue(Sprite icon, int currvalue, int maxvalue)
    {
        deersIcon.sprite = icon;
        deersTextholder.text = $"{currvalue} / {maxvalue}";
        deersholder.SetActive(true);
    }
    public void SetHorseValue(Sprite icon, int currvalue, int maxvalue)
    {
        horseIcon.sprite = icon;
        horseTextholder.text = $"{currvalue} / {maxvalue}";
        horseholder.SetActive(true);
    }
    public void SetSteelValue(Sprite icon, int currvalue, int maxvalue)
    {
        steelIcon.sprite = icon;
        steelTextholder.text = $"{currvalue} / {maxvalue}";
        steelholder.SetActive(true);
    }
    public void SetMagikStonesValue(Sprite icon, int currvalue, int maxvalue)
    {
        magikStonesIcon.sprite = icon;
        magikStoneTextholder.text = $"{currvalue} / {maxvalue}";
        magikstonesholder.SetActive(true);
    }
    public void SetTextileValue(Sprite icon, int currvalue, int maxvalue)
    {
        textileIcon.sprite = icon;
        textileTextholder.text = $"{currvalue} / {maxvalue}";
        textileholder.SetActive(true);
    }
    
    public void SetBuildingFace(Sprite icon,string name)
    {
        BuildingIconHolder.sprite = icon;
        BuildingNameHolder.text = name; 
    }
    public void DisableAllHolders()
    {
        textileholder.SetActive(false);
        magikstonesholder.SetActive(false);
        steelholder.SetActive(false);
        horseholder.SetActive(false);
        deersholder.SetActive(false);
        ironholder.SetActive(false);
        woodHolder.SetActive(false);
    }

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    

}
