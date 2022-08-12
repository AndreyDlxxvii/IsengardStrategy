using ResurseSystem;
using System;
using UnityEngine;
using UnityEngine.UI;


 namespace BuildingSystem
{ 
    [System.Serializable]
    public class BuildingView : MonoBehaviour,IDisposable
    {
        public BuildingModel ThisBuildingModel => _thisBuildingModel;

        [SerializeField]
        private BuildingModel baseBuildingModel;
        [SerializeField]
        private BuildingModel _thisBuildingModel;
        [SerializeField]
        private GameObject BuildingVisual;
        [SerializeField]
        private GlobalBuildingsModels _globalBuildingsModels;        
        public Action<BuildingModel> BuildingBorn;
        public Action<BuildingModel> BuildingDie;

        private void Awake()
        {
            
            AwakeBuildModel();         
            
            BuildingBorn += _globalBuildingsModels.AddBuildingInNeedResurseForBuildingList;
            BuildingDie += _globalBuildingsModels.BuildingDestroy;
            BuildingBorn?.Invoke(ThisBuildingModel);
            
        }
        
        public void Dispose()
        {
           
            BuildingBorn -= _globalBuildingsModels.AddBuildingInNeedResurseForBuildingList;
           
            BuildingDie?.Invoke(_thisBuildingModel);
            BuildingDie -= _globalBuildingsModels.BuildingDestroy;
            
        }
        public BuildingModel GetBuildingModel()
        {
            return ThisBuildingModel;
        }
        public void ChangeBuildingVisual(BuildingModel bmodel)
        {
            BuildingVisual = bmodel.BasePrefab;
        }
        public void AwakeBuildModel()
        {
            if (baseBuildingModel is MainBuildingModel)
            {
                _thisBuildingModel = new MainBuildingModel((MainBuildingModel)baseBuildingModel);
                return;
            }            
            if (baseBuildingModel is ResurseMarkeBuildingModel)
            {
                _thisBuildingModel = new ResurseMarkeBuildingModel((ResurseMarkeBuildingModel)baseBuildingModel);
                return;
            }
            if (baseBuildingModel is ItemMarketBuildingModel)
            {
                _thisBuildingModel = new ItemMarketBuildingModel((ItemMarketBuildingModel)baseBuildingModel);
                return;
            }
            if (baseBuildingModel is ResurseProduceBuildingModel)
            {
                _thisBuildingModel = new ResurseProduceBuildingModel((ResurseProduceBuildingModel)baseBuildingModel);
                return;
            }
            if (baseBuildingModel is ProduceItemBuildingModel)
            {
                _thisBuildingModel = new ProduceItemBuildingModel((ProduceItemBuildingModel)baseBuildingModel);
                return;
            }
            if (baseBuildingModel is HouseBuildingModel)
            {
                _thisBuildingModel = new HouseBuildingModel((HouseBuildingModel)baseBuildingModel);
                return;
            }
            if (baseBuildingModel is WareHouseBuildModel)
            {
                _thisBuildingModel = new WareHouseBuildModel((WareHouseBuildModel)baseBuildingModel);
                return;
            }
        }
    }
}


