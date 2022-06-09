using BuildingSystem;
using ResurseSystem;
using UnityEngine;


public class BuildingResursesUIController:IOnController, IOnFixedUpdate
{    
    private BuildingModel currentBuilding;
    private ResurseMine currentMine;
    private BuildingsUI BuildingResUI;
    private GlobalBuildingsModels globalBuildingsModels;

    
    public BuildingResursesUIController(BuildingsUI UI,GlobalBuildingsModels _globalBuildingModels)
    {
        BuildingResUI = UI;
        globalBuildingsModels = _globalBuildingModels;
    }
    #region Работа с UI ресурсов
    public void SetValue(IResurseHolder holder)
    {
        switch (holder.ResurseInHolder.ResurseType)
        {
            case ResurseType.Wood:
                BuildingResUI.SetWoodValue(holder.ResurseInHolder.Icon,holder.CurrentResurseCount,holder.MaxResurseCount);
                break;
            case ResurseType.Iron:
                BuildingResUI.SetIronValue(holder.ResurseInHolder.Icon, holder.CurrentResurseCount, holder.MaxResurseCount);
                break;
            case ResurseType.Deer:
                BuildingResUI.SetDeersValue(holder.ResurseInHolder.Icon, holder.CurrentResurseCount, holder.MaxResurseCount);
                break;
            case ResurseType.Horse:
                BuildingResUI.SetHorseValue(holder.ResurseInHolder.Icon, holder.CurrentResurseCount, holder.MaxResurseCount);
                break;
            case ResurseType.MagikStones:
                BuildingResUI.SetMagikStonesValue(holder.ResurseInHolder.Icon, holder.CurrentResurseCount, holder.MaxResurseCount);
                break;
            case ResurseType.Steele:
                BuildingResUI.SetSteelValue(holder.ResurseInHolder.Icon, holder.CurrentResurseCount, holder.MaxResurseCount);
                break;
            case ResurseType.Textile:
                BuildingResUI.SetTextileValue(holder.ResurseInHolder.Icon, holder.CurrentResurseCount, holder.MaxResurseCount);
                break;
        }
    }
    public void SetBuildingResUIView(BuildingModel building)
    {
        BuildingResUI.SetBuildingFace(building.Icon, building.Name);
    }
    public void SetBuildingResUIView(ResurseMine mine)
    {
        BuildingResUI.SetBuildingFace(mine.Icon, mine.NameOfMine);
    }
    public void SetCurrentStock(ResurseStock stock)
    {                           
            
            Update(stock);                    
    }
    public void SetCurrentMine(ResurseMine mine)
    {  
        BuildingResUI.DisableAllHolders();
        Update(mine);        
     }    
    public void SetActiveUI(Mineral mine)
    {
        BuildingResUI.gameObject.SetActive(true);
        UnsubscriberBuilding();
        var _tempMine = mine.GetMineRes();
        if (_tempMine != currentMine)
        {
            UnsubscriberMine();
            currentMine = _tempMine;
            currentMine.resurseMined += SetValue;
        }
        SetCurrentMine(currentMine);
        BuildingResUI.DisableBuildUI();
        BuildingResUI.DisableProduceUI();
    }
    public void SetActiveUI(BuildingView building)
    {
        BuildingResUI.gameObject.SetActive(true);
        UnsubscriberMine();
        var tempBuilding = building.GetBuildingModel();
        if (currentBuilding != tempBuilding)
        {
            UnsubscriberBuilding();
            currentBuilding = tempBuilding;
            currentBuilding.ThisBuildingStock.ResursesChange += SetValue;
            SubscribeProduceButton();
        }
        SetCurrentStock(currentBuilding.ThisBuildingStock);
        SetBuildingResUIView(currentBuilding);
        SetCurrentBuildingProduce();
        SetBuildUnderConstraction();
    }
    public void DisableMenu()
    {
        
        BuildingResUI.gameObject.SetActive(false);
    }
    public void Update (ResurseStock stock)
    {        
        foreach (ResurseHolder holder in currentBuilding.ThisBuildingStock.ResursesInStock)
        {
            SetValue(holder);
        }

    }    
    public void Update(ResurseMine mine)
    {
        SetBuildingResUIView(mine);        
        SetValue(mine.ResurseHolderMine);
    }
    public void UpdateValue(ResurseHolder holder)
    {
        SetValue(holder);
    }
    public void UnsubscriberMine()
    {
        if (currentMine!=null)
        { 
        currentMine.resurseMined -= SetValue;
        }
        currentMine = null;
    }
    public void UnsubscriberBuilding()
    {
        if (currentBuilding != null)
        {
            currentBuilding.ThisBuildingStock.ResursesChange -= SetValue;
            UnsubscribeProduceButton();
        }
        
        currentBuilding = null;
    }
    public void UnsubscriberAll()
    {
        UnsubscriberMine();
        UnsubscriberBuilding();        
    }

    #endregion

    #region контроль Строительства и производства
    public void StartBuilding(float time)
    {
        var tempBuildingList = globalBuildingsModels.GetBuildingsUnderConstraction();
        if (tempBuildingList!=null)
        { 
        foreach (BuildingView build in tempBuildingList)
        {
            build.GetBuildingModel().StartBuilding(time);
        }
        SetBuildUnderConstraction();
        }
    }
    public void StartProducing(float time)
    {
        var tempBuildList = globalBuildingsModels.GetProduceList();
        if (tempBuildList!=null)
        foreach (IProduce produceBuildModel in tempBuildList)
        {
            produceBuildModel.StartProduce(time);
        }
        if (currentBuilding is IProduce)
        {
            var tempBuilding = (IProduce)currentBuilding;
            BuildingResUI.SetValueProduceLoad(tempBuilding.CurrentProduceTime);
        }
    }
    #endregion

    #region Работа с UI Производства и строительства
    public void SetCurrentBuildingProduce()
    {
        BuildingResUI.DisableProduceUI();
        if (currentBuilding is ProduceItemBuildingModel)
        {
            var tempBuildingModel = (ProduceItemBuildingModel)currentBuilding;
            SetCostBuilding(tempBuildingModel.NeeddedResursesForProduce);
            BuildingResUI.SetProduceInfo(tempBuildingModel.ProducedItem.Item.Icon, tempBuildingModel.ProducedValue,tempBuildingModel.ProducingTime);
            BuildingResUI.SetActiveProduceUI();
            
        }
        else        
            if (currentBuilding is ResurseProduceBuildingModel)
        {
            var tempBuildingModel = (ResurseProduceBuildingModel)currentBuilding;
            SetCostBuilding(tempBuildingModel.NeeddedResursesForProduce);
            BuildingResUI.SetProduceInfo(tempBuildingModel.ProducedResurse.Icon, tempBuildingModel.ProducedValue, tempBuildingModel.ProducingTime);
            BuildingResUI.SetActiveProduceUI();
        }
    }
    public void SubscribeProduceButton()
    {
        if (currentBuilding is IProduce)
        {
            var tempBuilding = (IProduce)currentBuilding;
            BuildingResUI.AutoProduceButton.onClick.AddListener(() => tempBuilding.SetAutoProduceFlag());
            BuildingResUI.StartProduceButton.onClick.AddListener(() => tempBuilding.GetResurseForProduce());
        }
    }
    public void UnsubscribeProduceButton()
    {
        BuildingResUI.AutoProduceButton.onClick.RemoveAllListeners();
        BuildingResUI.StartProduceButton.onClick.RemoveAllListeners();
        
    }    
    public void SetBuildUnderConstraction()
    {
        BuildingResUI.DisableBuildUI();
        if (!currentBuilding.ThisBuildingCost.PricePaidFlag)
        {
            BuildingResUI.SetActiveBuildUI(currentBuilding.BuildingTime);
        }
    }
    
    public void SetCostBuilding (ResurseCost cost)
    {
        if (cost.CoastInResurse.Count==1)
        {
            BuildingResUI.SetCostInfo(cost.CoastInResurse[0].ResurseInHolder.Icon, cost.CoastInResurse[0].MaxResurseCount);
        }
        if (cost.CoastInResurse.Count==2)
        {
            BuildingResUI.SetCostInfo(cost.CoastInResurse[0].ResurseInHolder.Icon, cost.CoastInResurse[0].MaxResurseCount, 
                cost.CoastInResurse[1].ResurseInHolder.Icon, cost.CoastInResurse[1].MaxResurseCount);
        }
        if (cost.CoastInResurse.Count==3)
        {
            BuildingResUI.SetCostInfo(cost.CoastInResurse[0].ResurseInHolder.Icon, cost.CoastInResurse[0].MaxResurseCount,
                cost.CoastInResurse[1].ResurseInHolder.Icon, cost.CoastInResurse[1].MaxResurseCount,
                cost.CoastInResurse[2].ResurseInHolder.Icon, cost.CoastInResurse[2].MaxResurseCount);
        }
    }
    #endregion
    public void OnFixedUpdate(float fixedDeltaTime)
    {
        StartBuilding(fixedDeltaTime);
        StartProducing(fixedDeltaTime);
    }
}
