using System;
using System.Collections;
using System.Collections.Generic;
using ResurseSystem;
using UnityEngine;

namespace BuildingSystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Produce Resurse Building Model", menuName = "Buildings/ProduceResBuildingModel", order = 1)]
    public class ResurseProduceBuildingModel : BuildingModel, IProduceResurse
    {
        public ResurseCost NeeddedResursesForProduce => _needdedResursesForProduce;

        public ResurseCraft ProducedResurse => _producedResurse;

        public float ProducingTime => _producingTime;

        public float CurrentProduceTime => _currentProduceTime;

        public Action<BuildingModel> AStartProducing { get ; set ; }

        public int ProducedValue => _produceResurseValue;

        public bool autoProduce => _thisBuildingAutoproduce;

        [SerializeField]
        private ResurseCost _needdedResursesForProduce;
        [SerializeField]
        private ResurseCraft _producedResurse;
        [SerializeField]
        private int _produceResurseValue;
        [SerializeField]
        private float _producingTime;
        [SerializeField]
        private float _currentProduceTime;
        [SerializeField]
        private bool _thisBuildingAutoproduce;





        public ResurseProduceBuildingModel(ResurseProduceBuildingModel basebuilding):base(basebuilding)
        {
            _needdedResursesForProduce = basebuilding.NeeddedResursesForProduce;
            _producedResurse = basebuilding.ProducedResurse;
            _producingTime = basebuilding.ProducingTime;
            _produceResurseValue = basebuilding.ProducedValue;
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
        public void GetResurseForProduce()
        {
            NeeddedResursesForProduce.GetNeededResurse(ThisBuildingStock);
            if (NeeddedResursesForProduce.PricePaidFlag)
            {
                AStartProducing?.Invoke(this);
            }
        }

        public void StartProduce(float time)
        {
            _currentProduceTime += time;
            if (CurrentProduceTime>=ProducingTime)
            {
                var tempHolder = new ResurseHolder(ProducedResurse, ProducedValue, ProducedValue);
                ThisBuildingStock.AddResursesFromHolder(tempHolder);
                _currentProduceTime = 0;                
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
    }
}
