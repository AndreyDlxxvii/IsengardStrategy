using System.Collections.Generic;
using UnityEngine;
using ResurseSystem;

namespace BuildingSystem
{ 
    [System.Serializable]
    [CreateAssetMenu(fileName = "Global Buildings Models ", menuName = "Buildings/Global Buildings Models", order = 1)]
    public class GlobalBuildingsModels : ScriptableObject
    {
        #region ѕол€ глобального списка зданий
        [SerializeField]
        private List<BuildingView> BuildingsUnderConstraction;
        [SerializeField]
        private List<BuildingView> ActiveBuildings;
        [SerializeField]
        private List<BuildingView> NeedResursesBuildings;
        [SerializeField]
        private List<BuildingView> ProduceBuildings;        
        [SerializeField]
        private List<IProduce> ProduceList;
        #endregion
        public GlobalBuildingsModels()
        {
            BuildingsUnderConstraction = new List<BuildingView>();
            ActiveBuildings = new List<BuildingView>();
            NeedResursesBuildings = new List<BuildingView>();
            ProduceBuildings = new List<BuildingView>();            
            ProduceList = new List<IProduce>();

        }
        #region доступ к пол€м
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
        public List<IProduce> GetProduceList()
        {
            return ProduceList;
        }
        #endregion

        #region ћетоды взаимодействи€ листов зданий
        public void AddNeedResurse(BuildingView building)
        {
            if (building.GetBuildingModel().ThisBuildingCost!=null)
            { 
                NeedResursesBuildings.Add(building);
                
            }
            else
            {
                ActiveBuildings.Add(building);
                CheckProducedBuildingsModel(building);
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
            building.ChangeBuildingVisual(building.GetBuildingModel());
            CheckProducedBuildingsModel(building);


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
                    buildmodel.ABuildingComplete -= CheckBuildsComplete;
                    BuildingComplete(building);
                    
                    break;
                }
            }
        }
        public void CheckProducedBuildingsModel(BuildingView buildingView)
        {
            var tempBuildModel = buildingView.GetBuildingModel();
            if (tempBuildModel is IProduce)
            {
                ProduceBuildings.Add(buildingView);
                ProduceList.Add((IProduce)tempBuildModel);
            }
            
        }
        public void DeleteProducedBuildingsModel(BuildingView buildingView)
        {
            var tempBuildModel = buildingView.GetBuildingModel();
            if (tempBuildModel is IProduce)
            {
                ProduceBuildings.Remove(buildingView);
                ProduceList.Remove((IProduce)tempBuildModel);
            }
        }
        #endregion

        public void ResetGlobalBuildingModel()
        {
            BuildingsUnderConstraction = new List<BuildingView>();
            ActiveBuildings = new List<BuildingView>();
            NeedResursesBuildings = new List<BuildingView>();
            ProduceBuildings = new List<BuildingView>();
            ProduceList = new List<IProduce>();
        }
    }
}
