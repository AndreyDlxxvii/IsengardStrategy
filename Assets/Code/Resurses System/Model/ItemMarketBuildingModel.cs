using EquipmentSystem;
using ResurseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{ 
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Item Market Building Model", menuName = "Buildings/ItemMarketBuildingModels", order = 1)]
    public class ItemMarketBuildingModel : MarketBuildingModel<ItemModel>
    {

        public ItemMarketBuildingModel(ItemMarketBuildingModel baseBuilding) : base(baseBuilding)
        {
            
      
        }
        public override void AddProductInBasket(ItemModel obj)
        {
            ItemProduct product = new ItemProduct(obj, _buyObjectCount, 0);
            _productsInBasket.Add(product);
            float tempCost = product.ObjectProduct.CostInGold.Cost + _marketCostModification;
            _currentBuyCost.ChangeCost(_currentBuyCost.Cost + tempCost);

        }
        
         
        
    }
}
