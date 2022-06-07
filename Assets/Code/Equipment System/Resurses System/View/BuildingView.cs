using ResurseSystem;
using System;
using UnityEngine;
using UnityEngine.UI;


 namespace BuildingSystem
{ 
    [System.Serializable]
    public class BuildingView : MonoBehaviour,IDisposable
    {
        [SerializeField]
        private BuildingModel baseBuildingModel;
        [SerializeField]
        private BuildingModel ThisBuildingModel;
        [SerializeField]
        private GameObject BuildingVisual;
        [SerializeField]
        private GlobalResurseStock _globalResurseStock;        

        [SerializeField]
        private GlobalBuildingsModels _globalBuildingsModels;
        public Action<ResurseStock> ResStockBorn;
        public Action<ResurseStock> ResStockDie;
        public Action<BuildingView> BuildingBorn;
        public Action<BuildingView> BuildingDie;

        private void Awake()
        {
            
            AwakeBuildModel();         
            if (ThisBuildingModel.ThisBuildingStock!=null)
            {     
            ResStockBorn += _globalResurseStock.AddResurseStock;
            ResStockDie += _globalResurseStock.DeleteStock;
            }
            BuildingBorn += _globalBuildingsModels.AddNeedResurse;
            BuildingDie += _globalBuildingsModels.BuildingDestroy;
            BuildingBorn?.Invoke(this);
            ResStockBorn?.Invoke(ThisBuildingModel.ThisBuildingStock);
        }
        public IResurseStock GetStock()
        {
            return ThisBuildingModel.ThisBuildingStock;
        }
        public void Dispose()
        {
            ResStockBorn -= _globalResurseStock.AddResurseStock;
            BuildingBorn -= _globalBuildingsModels.AddNeedResurse;
            ResStockDie?.Invoke(ThisBuildingModel.ThisBuildingStock);
            BuildingDie?.Invoke(this);
            BuildingDie -= _globalBuildingsModels.BuildingDestroy;
            ResStockDie -= _globalResurseStock.DeleteStock;
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
            if (baseBuildingModel is WareHouseBuildModel)
            {
                ThisBuildingModel = new WareHouseBuildModel(baseBuildingModel);
            }
            else
            {
                if (baseBuildingModel is ResurseProduceBuildingModel)
                {
                    ThisBuildingModel = new ResurseProduceBuildingModel((ResurseProduceBuildingModel)baseBuildingModel);
                }
                else
                    if (baseBuildingModel is ProduceItemBuildingModel)
                {
                    ThisBuildingModel = new ProduceItemBuildingModel((ProduceItemBuildingModel)baseBuildingModel);
                }
            }
        }
    }
}


