using ResurseSystem;
using UnityEngine;


public class BuildingResursesUIController:IOnController
{    
    private BuildingModel currentBuilding;
    private ResurseMine currentMine;
    private BuildingsUI BuildingResUI;

    
    public BuildingResursesUIController(BuildingsUI UI)
    {
        BuildingResUI = UI;
    }
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
        }
        SetCurrentStock(currentBuilding.ThisBuildingStock);
        SetBuildingResUIView(currentBuilding);
        
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
        }
        currentBuilding = null;
    }
    public void UnsubscriberAll()
    {
        UnsubscriberMine();
        UnsubscriberBuilding();        
    }

}
