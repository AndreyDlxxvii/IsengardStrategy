using ResurseSystem;
using System;
using UnityEngine;
using UnityEngine.UI;


 namespace ResurseSystem
{ 
    public class BuildingView : MonoBehaviour,IDisposable
    {
        [SerializeField]
        private BuildingModel BuildingModel;
        [SerializeField]
        private GlobalResurseStock _globalResurseStock;        
        public Action<ResurseStock> ResStockBorn;
        public Action<ResurseStock> ResStockDie;                

        private void Awake()
        {
            
            ResStockBorn += _globalResurseStock.AddResurseStock;
            ResStockDie += _globalResurseStock.DeleteStock;
            
            ResStockBorn?.Invoke(BuildingModel.ThisBuildingStock);
        }
        public IResurseStock GetStock()
        {
            return BuildingModel.ThisBuildingStock;
        }
        public void Dispose()
        {
            ResStockBorn -= _globalResurseStock.AddResurseStock;
            ResStockDie?.Invoke(BuildingModel.ThisBuildingStock);
            ResStockDie -= _globalResurseStock.DeleteStock;
        }
        public BuildingModel GetBuildingModel()
        {
            return BuildingModel;
        }        
    }
}


