using System.Collections.Generic;
using UnityEngine;
using ResurseSystem;

namespace BuildingSystem
{ 
    [System.Serializable]
    [CreateAssetMenu(fileName = "Global Buildings Models ", menuName = "Buildings/Global Buildings Models", order = 1)]
    public class GlobalBuildingsModels : ScriptableObject
    {
        [SerializeField]
        private List<BuildingView> BuildingsUnderConstraction;
        [SerializeField]
        private List<BuildingView> ActiveBuildings;
        [SerializeField]
        private List<BuildingView> NeedResursesBuildings;
        [SerializeField]
        private List<ResurseProduceBuildingModel> ProduceResurseBuildings;
        [SerializeField]
        private List<ProduceItemBuildingModel> ProduceItemBuildings;
        [SerializeField]
        private List<BuildingView> ProduceList;

        public GlobalBuildingsModels()
        {
            BuildingsUnderConstraction = new List<BuildingView>();
            ActiveBuildings = new List<BuildingView>();
            NeedResursesBuildings = new List<BuildingView>();
            ProduceResurseBuildings = new List<ResurseProduceBuildingModel>();
            ProduceItemBuildings = new List<ProduceItemBuildingModel>();
            ProduceList = new List<BuildingView>();

        }
        public List<BuildingView> GetActiveBuildings()
        {
            return ActiveBuildings;
        }
        public List<BuildingView> GetBuildingsUnderConstraction()
        {
            return BuildingsUnderConstraction;
        }
        public List<BuildingView> GetNeedResursesBuildings()
        {
            return NeedResursesBuildings;
        }
        

        public void AddNeedResurse(BuildingView building)
        {
            if (building.GetBuildingModel().ThisBuildingCost!=null)
            { 
                NeedResursesBuildings.Add(building);
                
            }
            else
            {
                ActiveBuildings.Add(building);
            }
        }
        public void BuildingCostPaid(BuildingView building)
        {
            NeedResursesBuildings.Remove(building);
            StartBuildBuilding(building);
        }
        public void StartBuildBuilding(BuildingView building)
        {
            BuildingsUnderConstraction.Add(building);
            building.GetBuildingModel().ABuildingComplete += CheckBuildsComplete;
        }
        public void BuildingComplete(BuildingView building)
        {
            BuildingsUnderConstraction.Remove(building);
            
            ActiveBuildings.Add(building);
        }
        public void BuildingDestroy(BuildingView building)
        {
            ActiveBuildings.Remove(building);
            DeleteProducedBuildingsModel(building);
        }
        public void CheckBuildingsCostPaid(ResurseCost costPaid)
        {
            foreach (BuildingView building in NeedResursesBuildings)
            {
                if (building.GetBuildingModel().ThisBuildingCost == costPaid)
                {
                    BuildingCostPaid(building);
                    
                    break;
                }
            }
        }
        public void CheckBuildsComplete(BuildingModel buildmodel)
        {
            foreach (BuildingView building in BuildingsUnderConstraction)
            {
                if (building.GetBuildingModel() == buildmodel)
                {
                    BuildingComplete(building);
                    
                    break;
                }
            }
        }
        public void CheckProducedBuildingsModel(BuildingView buildingView)
        {
            var tempBuildModel = buildingView.GetBuildingModel();
            if (typeof(ResurseProduceBuildingModel).IsAssignableFrom(tempBuildModel.GetType()))
            {
                var tempProduceBuildModel = (ResurseProduceBuildingModel)tempBuildModel;
                ProduceResurseBuildings.Add(tempProduceBuildModel);                
                
            }
            else
            { 
                if (typeof(ProduceItemBuildingModel).IsAssignableFrom(tempBuildModel.GetType()))
                {
                    var tempProduceBuildModel = (ProduceItemBuildingModel)tempBuildModel;
                    ProduceItemBuildings.Add(tempProduceBuildModel);
                }
            }
        }
        public void DeleteProducedBuildingsModel(BuildingView buildingView)
        {
            var tempBuildModel = buildingView.GetBuildingModel();
            if (typeof(ResurseProduceBuildingModel).IsAssignableFrom(tempBuildModel.GetType()))
            {
                ProduceResurseBuildings.Remove((ResurseProduceBuildingModel)tempBuildModel);
            }
            else
            {
                if (typeof(ProduceItemBuildingModel).IsAssignableFrom(tempBuildModel.GetType()))
                {
                    var tempProduceBuildModel = (ProduceItemBuildingModel)tempBuildModel;
                    
                    ProduceItemBuildings.Remove(tempProduceBuildModel);
                }
            }
        }
        public void StartProduceList(ResurseCost resCost)
        {

        }
        public void ResetGlobalBuildingModel()
        {
            BuildingsUnderConstraction = new List<BuildingView>();
            ActiveBuildings = new List<BuildingView>();
            NeedResursesBuildings = new List<BuildingView>();
        }
    }
}
