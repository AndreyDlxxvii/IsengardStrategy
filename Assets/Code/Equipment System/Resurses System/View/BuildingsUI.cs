using ResurseSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsUI : MonoBehaviour
{
    #region Поля UI
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

    [SerializeField]
    private GameObject ProduceUI;
    [SerializeField]
    private TextMeshProUGUI ProduceTitle;
    [SerializeField]
    public Button StartProduceButton;
    [SerializeField]
    public Button AutoProduceButton;
    [SerializeField]
    private Slider ProduceLoadSlider;
    [SerializeField]
    private Image ProduceIcon;
    [SerializeField]
    private TextMeshProUGUI ProduceValue;

    [SerializeField]
    private GameObject Cost1Holder;
    [SerializeField]
    private Image Cost1Icon;
    [SerializeField]
    private TextMeshProUGUI Cost1Value;

    [SerializeField]
    private GameObject Cost2Holder;
    [SerializeField]
    private Image Cost2Icon;
    [SerializeField]
    private TextMeshProUGUI Cost2Value;

    [SerializeField]
    private GameObject Cost3Holder;
    [SerializeField]
    private Image Cost3Icon;
    [SerializeField]
    private TextMeshProUGUI Cost3Value;

    [SerializeField]
    private GameObject BuildSpace;
    [SerializeField]
    private Slider BuildSlider;
    #endregion
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

    public void SetValueProduceLoad(float value)
    {
        ProduceLoadSlider.value = value;
    }
    public void SetProduceInfo(Sprite icon, int produceValue,float produceTime)
    {
        ProduceIcon.sprite = icon;
        ProduceValue.text = $"{produceValue}";
        ProduceLoadSlider.maxValue = produceTime;
    }
    public void SetCostInfo(Sprite icon1, int value1, Sprite icon2, int value2)
    {
        Cost1Icon.sprite=icon1;
        Cost1Value.text = $"{value1}";
        Cost2Icon.sprite = icon2;
        Cost2Value.text = $"{value2}";
        Cost1Holder.SetActive(true);
        Cost2Holder.SetActive(true);
        Cost3Holder.SetActive(false);
    }
    public void SetCostInfo(Sprite icon1, int value1, Sprite icon2, int value2, Sprite icon3, int value3)
    {
        Cost1Icon.sprite = icon1;
        Cost1Value.text = $"{value1}";
        Cost2Icon.sprite = icon2;
        Cost2Value.text = $"{value2}";
        Cost3Icon.sprite = icon3;
        Cost3Value.text = $"{value3}";
        Cost1Holder.SetActive(true);
        Cost2Holder.SetActive(true);
        Cost3Holder.SetActive(true);
    }
    public void SetCostInfo(Sprite icon1, int value1)
    {
        Cost1Icon.sprite = icon1;
        Cost1Value.text = $"{value1}";
        Cost1Holder.SetActive(true);
        Cost2Holder.SetActive(false);
        Cost3Holder.SetActive(false);
    }
    
    public void DisableProduceUI()
    {
        ProduceUI.SetActive(false);
    }
    public void SetActiveProduceUI()
    {
        ProduceUI.SetActive(true);
    }
    public void DisableBuildUI()
    {
        BuildSpace.SetActive(false);
    }
    public void SetActiveBuildUI(float buildTime)
    {
        BuildSpace.SetActive(true);
        BuildSlider.maxValue = buildTime;
    }
    public void SetBuildSliderValue (float value)
    {
        BuildSlider.value = value;
    }

    private void Awake()
    {
        gameObject.SetActive(false);
        DisableProduceUI();
    }
    

}
