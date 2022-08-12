using ResurseSystem;
using EquipmentSystem;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New WareHouse Building Model", menuName = "Buildings/WareHouseBuildingModel", order = 1)]
    public class WareHouseBuildModel : BuildingModel
    {
        public System.Action<ResurseHolder> AddReusurseAction;
        public System.Action<Item—arrierHolder> AddItemAction;

        public ResurseStock WareHouseStock=>_warehouseStock;

        [SerializeField] private ResurseStock _warehouseStock;

        public WareHouseBuildModel (WareHouseBuildModel baseBuilding):base(baseBuilding)
        {
            _warehouseStock = new ResurseStock(baseBuilding.WareHouseStock);

        }

        public void AddInStock(ResurseHolder holder)
        {
            //AddReusurseAction?.Invoke(holder);
            _globalResurseStock.AddResurseToGlobalResurseStock(holder);
        }
        public void AddInStock(Item—arrierHolder holder)
        {
            //AddItemAction?.Invoke(holder);
            _globalResurseStock.AddItemToGlobalResurseStock(holder);
        }

        public override void AwakeModel()
        {
           if (_warehouseStock!=null)
            {
                _globalResurseStock.ChangeMaxValueOfGlobalResurseStock(_warehouseStock);
            }
        }
    }
}
