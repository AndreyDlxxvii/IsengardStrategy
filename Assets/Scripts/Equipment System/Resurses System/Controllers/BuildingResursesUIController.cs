using ResurseSystem;
using UnityEngine;


public class BuildingResursesUIController:IOnController
{    
    private ResurseStock currentStock;
    private ResurseMine currentMine;
    private BuildingsUI BuildingResUI;

    
    public BuildingResursesUIController(BuildingsUI UI)
    {
        BuildingResUI = UI;
    }
    public void SetValue(ResurseHolder holder)
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
    public void SetBuildingResUIView(ResurseStock stock)
    {
        BuildingResUI.SetBuildingFace(stock.Icon, stock.NameOfStock);
    }
    public void SetBuildingResUIView(ResurseMine mine)
    {
        BuildingResUI.SetBuildingFace(mine.Icon, mine.NameOfMine);
    }
    public void SetCurrentStock(ResurseStock stock)
    {        
        if (currentStock!=stock)
        {
            UnsubscriberStock();
            currentStock = stock;
            currentStock.ResursesChange += SetValue;
            Update(stock);
            BuildingResUI.gameObject.SetActive(true);
        }
    }
    public void SetCurrentMine(ResurseMine mine)
    {
        if (currentMine!=mine)
        {
            UnsubscriberMine();
            currentMine = mine;
            currentMine.resurseMined += SetValue;
            BuildingResUI.DisableAllHolders();
            Update(mine);
            BuildingResUI.gameObject.SetActive(true);
        }
    }
    public void SetActiveUI(Mineral mine)
    {
        BuildingResUI.gameObject.SetActive(true);
        SetCurrentMine(mine.GetMineRes());
    }
    public void SetActiveUI(ResurseStockView stock)
    {
        BuildingResUI.gameObject.SetActive(true);
        SetCurrentStock(stock.GetStock());
    }
    public void DisableMenu()
    {
        BuildingResUI.gameObject.SetActive(false);
    }
    public void Update (ResurseStock stock)
    {
        SetBuildingResUIView(stock);
        foreach (ResurseHolder holder in currentStock.ResursesInStock)
        {
            SetValue(holder);
        }

    }    public void Update(ResurseMine mine)
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
    }
    public void UnsubscriberStock()
    {
        if (currentStock!=null)
        { 
        currentStock.ResursesChange -= SetValue;
        }
    }

}
