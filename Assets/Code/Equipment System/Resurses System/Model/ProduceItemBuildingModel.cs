using EquipSystem;
using ResurseSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Produce Item Building Model", menuName = "Buildings/ProduceItemBuildingModel", order = 1)]
    public class ProduceItemBuildingModel : BuildingModel, IProduceItem
    {
        public ResurseCost NeeddedResursesForProduce => _needdedResursesForProduce;

        public ItemÑarrierHolder ProducedItem => _producedItem;

        public int ProducedValue => _producedItemValue;

        public float ProducingTime => _producingTime;

        public float CurrentProduceTime => _currentProduceTime;

        public bool autoProduce => _thisBuildingAutoproduce;

        [SerializeField]
        private ResurseCost _needdedResursesForProduce;
        [SerializeField]
        private ItemÑarrierHolder _producedItem;
        [SerializeField]
        private int _producedItemValue;
        [SerializeField]
        private float _producingTime;
        [SerializeField]
        private float _currentProduceTime;
        [SerializeField]
        private bool _thisBuildingAutoproduce;

        public ProduceItemBuildingModel (ProduceItemBuildingModel basebuilding):base(basebuilding)
        {
            _needdedResursesForProduce = new ResurseCost(basebuilding.NeeddedResursesForProduce);
            _producedItem = new ItemÑarrierHolder(basebuilding.ProducedItem);
            _producedItemValue = basebuilding.ProducedValue;
            _producingTime = basebuilding.ProducingTime;
            _currentProduceTime = 0f;
        }

        public void CheckResurseForProduce()
        {
            foreach (ResurseHolder holderCost in _needdedResursesForProduce.CoastInResurse)
            {
                var tempResCount = holderCost.MaxResurseCount - holderCost.CurrentResurseCount;
                Debug.Log($"Need {tempResCount} of {holderCost.ResurseInHolder.NameOFResurse}");
            }
        }

        public ItemÑarrierHolder GetItem(int value)
        {
            return (ItemÑarrierHolder)_producedItem.GetItemInHolder(value);
        }
        public void GetResurseForProduce()
        {
            NeeddedResursesForProduce.GetNeededResurse(ThisBuildingStock);            
        }
        public void StartProduce(float time)
        {
            if (NeeddedResursesForProduce.PricePaidFlag)
            { 
                _currentProduceTime += time;
                if (CurrentProduceTime>=ProducingTime)
                { 
                    _producedItem.AddItemValue(_producedItem.Item, _producedItemValue);
                    _needdedResursesForProduce.ResetPaid();
                    _currentProduceTime = 0;
                }
            }
        }

        public override void AddResurseInStock(IResurseHolder holder)
        {
            ThisBuildingStock.AddResursesFromHolder(holder);
            GetCostForBuilding();
            if (autoProduce)
            {
                GetResurseForProduce();
            }
        }

        public void SetAutoProduceFlag()
        {
            _thisBuildingAutoproduce = !_thisBuildingAutoproduce;
        }
    }
}
