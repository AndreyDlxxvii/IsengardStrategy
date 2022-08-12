using BuildingSystem;
using ResurseSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingResursesUIController:IOnController, IOnUpdate,IOnStart,IDisposable 
{    
    private BuildingModel _currentBuilding;
    private ResurseMine _currentMine;   
    private BuildingsUI _BuildingResUI;
    private GlobalBuildingsModels _globalBuildingsModels;
    private TopResUiVew _TopResUI;
    private ProduceItemButtonView _currentProduceButton;
    private GlobalResurseStock _globalResStock; 





    public BuildingResursesUIController(BuildingsUI UI,GlobalBuildingsModels globalBuildingModels,TopResUiVew topUI, GlobalResurseStock globalResStock)
    {
        _BuildingResUI = UI;
        _globalBuildingsModels = globalBuildingModels;
        _globalResStock = globalResStock;
        _TopResUI = topUI;


    }
    #region ������ � UI ��������
    /// <summary>
    /// Set value of StockSpace UI of current building stock holder 
    /// </summary>
    /// <param name="holder"></param>
    public void SetStockValue(ResurseHolder holder)
    {
        switch (holder.ObjectInHolder.ResurseType)
        {
            case ResurseType.Wood:
                _BuildingResUI.SetWoodValue(holder.ObjectInHolder.Icon, holder.CurrentValue, holder.MaxValue);
                break;
            case ResurseType.Iron:
                _BuildingResUI.SetIronValue(holder.ObjectInHolder.Icon, holder.CurrentValue, holder.MaxValue);
                break;
            case ResurseType.Deer:
                _BuildingResUI.SetDeersValue(holder.ObjectInHolder.Icon, holder.CurrentValue, holder.MaxValue);
                break;
            case ResurseType.Horse:
                _BuildingResUI.SetHorseValue(holder.ObjectInHolder.Icon, holder.CurrentValue, holder.MaxValue);
                break;
            case ResurseType.MagikStones:
                _BuildingResUI.SetMagikStonesValue(holder.ObjectInHolder.Icon, holder.CurrentValue, holder.MaxValue);
                break;
            case ResurseType.Steele:
                _BuildingResUI.SetSteelValue(holder.ObjectInHolder.Icon, holder.CurrentValue, holder.MaxValue);
                break;
            case ResurseType.Textile:
                _BuildingResUI.SetTextileValue(holder.ObjectInHolder.Icon, holder.CurrentValue, holder.MaxValue);
                break;
            case ResurseType.Gold:
                _BuildingResUI.SetGoldValue(holder.ObjectInHolder.Icon, holder.CurrentValue, holder.MaxValue);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// Set building icon , name , Hp and others
    /// </summary>
    /// <param name="building"></param>
    public void SetBuildingResUIView(BuildingModel building)
    {
        _BuildingResUI.SetBuildingFace(building.Icon, building.Name);
        _BuildingResUI.SetHPSliderValue(building.MaxHealth, building.Health);
        _BuildingResUI.SetActiveHPSpace(true);
        string hpValuetxt = $"{building.Health} / {building.MaxHealth}";
        _BuildingResUI.SetHPValueTxt(hpValuetxt);
    }
    /// <summary>
    /// Set mine icon and name 
    /// </summary>
    /// <param name="mine"></param>
    public void SetBuildingResUIView(ResurseMine mine)
    {
        _BuildingResUI.SetBuildingFace(mine.Icon, mine.NameOfMine);
        _BuildingResUI.SetActiveHPSpace(true);
        _BuildingResUI.SetHPValueTxt("");
    }
    /// <summary>
    /// Set current stock fot Stock UI of Building UI
    /// </summary>
    /// <param name="stock"></param>
    public void SetCurrentStock(ResurseStock stock)
    {
        _BuildingResUI.SetActiveStockSpace(true);
        _BuildingResUI.DisableAllStockHolders();
        UpdateStock(stock);                    
    }
    /// <summary>
    /// Update max worker value for Global res UI(TopResUI)
    /// </summary>
    public void UpdateGlobalWorkerValue()
    {
        _TopResUI.UpdatePeopleCount(_globalBuildingsModels.GetGlobalMaxWorkerCount());
    }
    /// <summary>
    /// Set current mine stock for Stock UI of building UI
    /// </summary>
    /// <param name="mine"></param>
    public void SetCurrentMine(ResurseMine mine)
    {  
        _BuildingResUI.DisableAllStockHolders();
        UpdateStock(mine);        
    }
    /// <summary>
    /// Activation building UI for mine
    /// </summary>
    /// <param name="mine"></param>
    public void SetActiveUI(Mineral mine)
    {
        DisactivatedBuildingInfo();
        _BuildingResUI.SetActiveStockSpace(true);
        _BuildingResUI.SetActivBuildingUI(true);
        var _tempMine = mine.GetMineRes();
        if (_tempMine != _currentMine)
        {
            UnsubscriberMine();
            _currentMine = _tempMine;
            _currentMine.resurseMined += SetStockValue;
        }
        SetCurrentMine(_currentMine);
                
    }
    /// <summary>
    /// Set Info current house for Building UI
    /// </summary>
    public void SetCurrentHouse()
    {
        _BuildingResUI.SetActiveStockSpace(false);
        _BuildingResUI.SetActiveLoadUI(false);
        _BuildingResUI.SetActiveProduceUI(false);        
    }
    /// <summary>
    /// Set info current market for Building UI
    /// </summary>
    public void SetCurrentMarket()
    {
        _BuildingResUI.SetActiveStockSpace(false);
        _BuildingResUI.SetActiveLoadUI(false);
        _BuildingResUI.SetActiveProduceUI(false);
    }
    /// <summary>
    /// Set info current Warehouse for Building UI
    /// </summary>
    public void SetCurrentWarehouse()
    {
        _BuildingResUI.SetActiveLoadUI(false);
        _BuildingResUI.SetActiveProduceUI(false);
        WareHouseBuildModel warehouse = (WareHouseBuildModel)_currentBuilding;
        warehouse.WareHouseStock.ResursesChange += SetStockValue;
        SetCurrentStock(warehouse.WareHouseStock);
    }    
    /// <summary>
    /// "State machine" for building UI
    /// </summary>
    /// <param name="building"></param>
    public void CheckBuildingModelForUI(BuildingModel building)
    {
        if (_currentBuilding != building)
        {
            _currentBuilding = building;
            SetBuildingResUIView(_currentBuilding);
            UnsubscribeProduceButton();
            SetBuildUnderConstraction();
            
            if (building is WareHouseBuildModel)
            {
                SetCurrentWarehouse();
            }
            if (building is ProduceItemBuildingModel | building is ResurseProduceBuildingModel)
            {
                SetCurrentBuildingProduce();
            }
            if (building is HouseBuildingModel)
            {
                SetCurrentHouse();
            }
            if (building is ItemMarketBuildingModel | building is ResurseMarkeBuildingModel)
            {
                SetCurrentMarket();
            }
        }
    }
    public void DisactivatedBuildingInfo()
    {        
        _BuildingResUI.SetActiveProduceUI(false);
        _BuildingResUI.SetActiveLoadUI(false);
        _currentBuilding=null;
    }
    /// <summary>
    /// activation building UI for buildings
    /// </summary>
    /// <param name="building"></param>
    public void SetActiveUI(BuildingView building)
    {
        _BuildingResUI.SetActivBuildingUI(true);
        UnsubscriberMine();        
        var tempBuilding = building.GetBuildingModel();
        CheckBuildingModelForUI(tempBuilding);
        
    }
    /// <summary>
    /// Diasable core building UI gameObject
    /// </summary>
    public void DisableMenu()
    {
        _BuildingResUI.gameObject.SetActive(false);
    }
    /// <summary>
    /// Update value of current building stock for Stock UI of Building UI
    /// </summary>
    /// <param name="stock"></param>
    public void UpdateStock (ResurseStock stock)
    {         
        foreach (ResurseHolder holder in stock.HoldersInStock)
        {
            SetStockValue(holder);
        }
    }
    /// <summary>
    /// Update value of current mine stock for Stock UI of Building UI
    /// </summary>
    /// <param name="mine"></param>
    public void UpdateStock(ResurseMine mine)
    {
        SetBuildingResUIView(mine);
        SetStockValue(mine.ResurseHolderMine);
    }
    /// <summary>
    /// Update value of holder in stock for Stock UI of Building UI
    /// </summary>
    /// <param name="holder"></param>
    public void UpdateValue(ResurseHolder holder)
    {
        SetStockValue(holder);
    }
    /// <summary>
    /// Unsubscribe of current mine actions|current mine resets to null
    /// </summary>
    public void UnsubscriberMine()
    {
        if (_currentMine != null)
        {
            _currentMine.resurseMined -= SetStockValue;
        }
        _currentMine = null;
    }
    
    /// <summary>
    /// Unsubscribe of all actions current building and mine
    /// </summary>
    public void UnsubscriberAll()
    {
        UnsubscriberMine();
       DisactivatedBuildingInfo();        
    }

    #endregion

    #region �������� ������������� � ������������
    /// <summary>
    /// Delegate time for build to under constraction building
    /// </summary>
    /// <param name="time"></param>
    public void StartBuilding(float time)
    {
        _globalBuildingsModels.StartGlobalBuild(time);
        SetBuildUnderConstraction();
        
    }
    /// <summary>
    /// Delegate time for producing to produce products building and stock for pay the price of producing
    /// </summary>
    /// <param name="time"></param>
    /// <param name="stock"></param>
    public void StartProducing(float time,GlobalResurseStock stock)
    {
        _globalBuildingsModels.StartGlobalProducing(time,stock);        
    }
    #endregion

    #region ������ � UI ������������ � �������������
    /// <summary>
    /// Check current building model for produce building
    /// </summary>
    public void SetCurrentBuildingProduce()
    {

        _BuildingResUI.DisableProduceButtons();
        if (_currentBuilding is ProduceItemBuildingModel)
        {
            var tempBuildingModel = (ProduceItemBuildingModel)_currentBuilding;
            var tempListButtons = _BuildingResUI.GetListProduceButtons();

            for (int i=0;i<tempBuildingModel.ProduceProduct.Count;i++)
            {
                tempListButtons[i].SetProductInButton(tempBuildingModel.ProduceProduct[i]);
            }
            _BuildingResUI.SetActiveProduceUI(true);
            SubscribeProduceButton();

        }
        else
        { 
            if (_currentBuilding is ResurseProduceBuildingModel)
            {
                var tempBuildingModel = (ResurseProduceBuildingModel)_currentBuilding;
                var tempListButtons = _BuildingResUI.GetListProduceButtons();

                for (int i = 0; i < tempBuildingModel.ProduceProduct.Count; i++)
                {
                    tempListButtons[i].SetProductInButton(tempBuildingModel.ProduceProduct[i]);
                }
                _BuildingResUI.SetActiveProduceUI(true);
                SubscribeProduceButton();
            }
            else
            {
                UnsubscribeProduceButton();
                _currentProduceButton = null;
                _BuildingResUI.SetActiveProduceUI(false);
            }
        }
    }
    /// <summary>
    /// unsubscribe of buttons and action producing UI and building
    /// </summary>
    public void ProduceCurrentBuildProductsUnSubscribe()
    {
        var tempListButtons = _BuildingResUI.GetListProduceButtons();
        foreach (ProduceItemButtonView view in tempListButtons)
        {
            view.producedObjButton.onClick.RemoveAllListeners();
        }
    }
    /// <summary>
    /// Set value current produce button
    /// </summary>
    /// <param name="button"></param>
    public void SetCurrentProduceButton(ProduceItemButtonView button)
    {
        _currentProduceButton = button;
        SetCurrentProduceCost();


    }
    /// <summary>
    /// Set current produce cost in UI
    /// </summary>
    public void SetCurrentProduceCost()
    {
        ItemProduct tempIproduct = _currentProduceButton.GetCurrentItemProductInButton();
        ResurseProduct tempResProduct = _currentProduceButton.GetCurrentResurseProductInButton();
        string costProductText = "";
        if (tempIproduct != null)
        {
            costProductText = tempIproduct.ProducePrice.GetCostInText();
        }
        if (tempResProduct != null)
        {
            costProductText = tempResProduct.ProducePrice.GetCostInText();
        }
        _BuildingResUI.SetProduceCostTitle(costProductText);
    }
    /// <summary>
    /// Cancel all produce process for current builing
    /// </summary>
    public void CancelProduceButtonMetod()
    {
        _currentProduceButton = null;
        _BuildingResUI.SetLoadSliderValue(0f);
        if (_currentBuilding is ProduceItemBuildingModel)
        {
            var tempBuild = (ProduceItemBuildingModel)_currentBuilding;
            tempBuild.CancelProduce(_globalResStock);
        }
        if (_currentBuilding is ResurseProduceBuildingModel)
        {
            var tempBuild = (ResurseProduceBuildingModel)_currentBuilding;
            tempBuild.CancelProduce(_globalResStock);
        }
    }
    /// <summary>
    /// Adding product for producing in current building produce product list
    /// </summary>
    public void AddProductInCurrentBasket()
    {
        if (_currentProduceButton != null)
        {
            ItemProduct tempIProduct = _currentProduceButton.GetCurrentItemProductInButton();
            ResurseProduct tempRProduct = _currentProduceButton.GetCurrentResurseProductInButton();
            int tempValue = _BuildingResUI.GetProduceSliderValue();
            if (tempIProduct != null && _currentBuilding is ProduceItemBuildingModel)
            {
                ProduceItemBuildingModel tempBuilding = (ProduceItemBuildingModel)_currentBuilding;
                for (int i = 0; i < tempValue; i++)
                {
                    tempBuilding.AddProductForProduce(tempIProduct);
                }
            }
            if (tempRProduct != null && _currentBuilding is ResurseProduceBuildingModel)
            {
                ResurseProduceBuildingModel tempBuilding = (ResurseProduceBuildingModel)_currentBuilding;
                for (int i = 0; i < tempValue; i++)
                {
                    tempBuilding.AddProductForProduce(tempRProduct);
                }
            }
        }
    }
    /// <summary>
    /// Subscribe controller on product buttons on CLick
    /// </summary>
    public void SubscribeOnProductButtons()
    {
        List<ProduceItemButtonView> tempList = _BuildingResUI.GetListProduceButtons();
        foreach (ProduceItemButtonView button in tempList)
        {
            button.producedObjButton.onClick.AddListener(() => SetCurrentProduceButton(button));
        }
    }
    /// <summary>
    /// Subscribe on produce buttons and actions for current building
    /// </summary>
    public void SubscribeProduceButton()
    {
        if (_currentBuilding is ProduceItemBuildingModel)
        {
            var tempBuilding = (ProduceItemBuildingModel)_currentBuilding;
            _BuildingResUI.AutoProduceToggle.isOn = tempBuilding.AutoProduceFlag;
            _BuildingResUI.AutoProduceToggle.onValueChanged.AddListener(tempBuilding.SetAutoproduceFlag);
            _BuildingResUI.StartProduceButton.onClick.AddListener(()=>AddProductInCurrentBasket());
            _BuildingResUI.CancelProduceButton.onClick.AddListener(()=>CancelProduceButtonMetod());
        }
        if (_currentBuilding is ResurseProduceBuildingModel)
        {
            var tempBuilding = (ResurseProduceBuildingModel)_currentBuilding;
            _BuildingResUI.AutoProduceToggle.onValueChanged.AddListener(tempBuilding.SetAutoproduceFlag);
            _BuildingResUI.StartProduceButton.onClick.AddListener(AddProductInCurrentBasket);
            _BuildingResUI.CancelProduceButton.onClick.AddListener(CancelProduceButtonMetod);
        }
        
    }
    /// <summary>
    /// Unsubscribe for all 
    /// </summary>
    public void UnsubscribeProduceButton()
    {
        _BuildingResUI.AutoProduceToggle.onValueChanged.RemoveAllListeners();
        _BuildingResUI.StartProduceButton.onClick.RemoveAllListeners();
        _BuildingResUI.CancelProduceButton.onClick.RemoveAllListeners();

    }    
    /// <summary>
    /// Check state of constraction building and update build time value of current building for load slider value 
    /// </summary>
    public void SetBuildUnderConstraction()
    {
        _BuildingResUI.SetActiveLoadUI(false);
        if (!_currentBuilding.ThisBuildingCost.PricePaidFlag)
        {
            _BuildingResUI.SetActiveLoadUIValue(_currentBuilding.BuildingTime);
        }
    }
    
    //public void SetCostBuilding (ResurseCost cost)
    //{
    //    if (cost.CostInResurse.Count==1)
    //    {
    //        BuildingResUI.SetCostInfo(cost.CostInResurse[0].ObjectInHolder.Icon, cost.CostInResurse[0].MaxValue);
    //    }
    //    if (cost.CostInResurse.Count==2)
    //    {
    //        BuildingResUI.SetCostInfo(cost.CostInResurse[0].ObjectInHolder.Icon, cost.CostInResurse[0].MaxValue, 
    //            cost.CostInResurse[1].ObjectInHolder.Icon, cost.CostInResurse[1].MaxValue);
    //    }
    //    if (cost.CostInResurse.Count==3)
    //    {
    //        BuildingResUI.SetCostInfo(cost.CostInResurse[0].ObjectInHolder.Icon, cost.CostInResurse[0].MaxValue,
    //            cost.CostInResurse[1].ObjectInHolder.Icon, cost.CostInResurse[1].MaxValue,
    //            cost.CostInResurse[2].ObjectInHolder.Icon, cost.CostInResurse[2].MaxValue);
    //    }
    //}
    #endregion
    public void OnUpdate(float fixedDeltaTime)
    {
        float tempTime = Time.deltaTime;
        _globalBuildingsModels.StartGlobalProducing(tempTime,_globalResStock);
        _globalBuildingsModels.StartGlobalBuild(tempTime);
    }

    public void OnStart()
    {
        _globalBuildingsModels.ChangeMaxWorkerValue += _TopResUI.UpdatePeopleCount;
        _globalBuildingsModels.ResetGlobalBuildingModel();
        SubscribeOnProductButtons();


    }

    public void Dispose()
    {
        _globalBuildingsModels.ChangeMaxWorkerValue -= _TopResUI.UpdatePeopleCount;
    }
}
