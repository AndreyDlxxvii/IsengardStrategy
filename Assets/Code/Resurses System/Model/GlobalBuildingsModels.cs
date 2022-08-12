using System.Collections.Generic;
using UnityEngine;
using ResurseSystem;
using System;

namespace BuildingSystem
{ 
    [System.Serializable]
    [CreateAssetMenu(fileName = "Global Buildings Models ", menuName = "Buildings/Global Buildings Models", order = 1)]
    public class GlobalBuildingsModels : ScriptableObject
    {
        #region ���� ����������� ������ ������
        [SerializeField]
        private List<BuildingModel> _ActiveBuildings;        
        [SerializeField]
        private List<BuildingModel> _NeedResursesBuildings;
        [SerializeField]
        private List<BuildingModel> _BuildingsUnderConstraction;
        [SerializeField]
        private List<WareHouseBuildModel> _StockBuildings;
        [SerializeField]
        private List<ResurseProduceBuildingModel> _ProduceResurseBuildings;
        
        [SerializeField]
        private List<ProduceItemBuildingModel> _ProduceItemBuildings;
        [SerializeField]
        private List<MarketBuildingModel<ScriptableObject>> _MarketsList;
        [SerializeField]
        private List<IProduceWorkers> _ProduceWorkerBuildings;
        [SerializeField]
        private int _globalMaxWorkersValue;

        public Action<int> ChangeMaxWorkerValue;
        #endregion
        public GlobalBuildingsModels()
        {
            _ActiveBuildings = new List<BuildingModel>();            
            _NeedResursesBuildings = new List<BuildingModel>();
            _BuildingsUnderConstraction = new List<BuildingModel>();
            _ProduceResurseBuildings = new List<ResurseProduceBuildingModel>();
            _ProduceItemBuildings = new List<ProduceItemBuildingModel>();
            _StockBuildings = new List<WareHouseBuildModel>();
            _ProduceWorkerBuildings = new List<IProduceWorkers>();
            _MarketsList = new List<MarketBuildingModel<ScriptableObject>>();
            _globalMaxWorkersValue = 0;
            ChangeMaxWorkerValue?.Invoke(_globalMaxWorkersValue);

        }
        #region ������ � �����
        public List<BuildingModel> GetActiveBuildings()
        {
            return _ActiveBuildings;
        }
        public List<BuildingModel> GetBuildingsUnderConstraction()
        {
            return _BuildingsUnderConstraction;
        }
        public List<BuildingModel> GetNeedResursesBuildings()
        {
            return _NeedResursesBuildings;
        }
        public List<ResurseProduceBuildingModel> GetProduceResurseBuildings()
        {
            return _ProduceResurseBuildings;
        }
        public List<ProduceItemBuildingModel> GetProduceItemBuildings()
        {
            return _ProduceItemBuildings;
        }
        public List<IProduceWorkers> GetAllHouse()
        {
            return _ProduceWorkerBuildings;
        }
        public List<MarketBuildingModel<ScriptableObject>> GetAllMarkets()
        {
            return _MarketsList;
        }
        public int GetMaxWorkerCount()
        {
            return _globalMaxWorkersValue;
        }
        #endregion

        #region ������ �������������� ������ ������
        
        /// <summary>
        /// ����� ��� ������� �������� �� ����������� ����� ��� �������������
        /// </summary>
        /// <param name="globalStock"></param>
        public void AddNeedForBuildResurse(GlobalResurseStock globalStock)
        {
            if (_NeedResursesBuildings!=null)
            { 
            foreach (BuildingModel model in _NeedResursesBuildings)
            {
                model.ThisBuildingCost.GetNeededResurse(globalStock.GlobalResStock);
            }
            CheckBuildingsCostPaid();
            }
        }
        public void AddBuildingInNeedResurseForBuildingList(BuildingModel building)
        {
            if (building.ThisBuildingCost.PricePaidFlag)
            {
                _ActiveBuildings.Add(building);
                CheckBuildingsModel(building);
            }
            else
            { 
                _NeedResursesBuildings.Add(building);
            }
        }
        /// <summary>
        /// ����� �������� ������ �� ����������� � �������� ��� ������������� � ���������� � ������ �� �������������
        /// </summary>
        /// <param name="building"></param>
        public void BuildingCostPaid(BuildingModel building)
        {
            _NeedResursesBuildings.Remove(building);
            StartBuildBuilding(building);
        }
        
        /// <summary>
        /// ����� ���������� ������ � ���� ���������� (� ������ ��� �������� ������ ���������� ������������� ������ �� ����� ���������� ������������� ������) 
        /// </summary>
        /// <param name="building"></param>
        public void StartBuildBuilding(BuildingModel building)
        {
            _BuildingsUnderConstraction.Add(building);
            building.ABuildingComplete += CheckBuildsComplete;
        }
      
        /// <summary>
        /// ����� �������� ������ �� ����������, ���������� � ������ �������� ������, �������� � ���� ����� �������� ������ ������
        /// </summary>
        /// <param name="building"></param>
        public void BuildingComplete(BuildingModel building)
        {
            _BuildingsUnderConstraction.Remove(building);            
            _ActiveBuildings.Add(building);
            CheckBuildingsModel(building);
        }
        
        /// <summary>
        /// ����� �������� ������ �� ��������, ��� �� �������� ����� �������� ������ � ������ ������
        /// </summary>
        /// <param name="building"></param>
        public void BuildingDestroy(BuildingModel building)
        {
            _ActiveBuildings.Remove(building);
            DeleteBuildingsModel(building);
        }
       
        /// <summary>
        /// ����� �������� ��������� ���������� ������������� ������ �� ������ ����������� � ��������.
        /// </summary>
        public void CheckBuildingsCostPaid()
        {
            foreach (BuildingModel building in _NeedResursesBuildings)
            {
                if (building.ThisBuildingCost.PricePaidFlag)
                {
                    BuildingCostPaid(building);                    
                }
            }
        }
        
        /// <summary>
        /// ����� ������� �� ������ ���������� ������������� ������, ������� ���������. �������� ����� ���������� ������ � ��������
        /// </summary>
        /// <param name="buildmodel"></param>
        public void CheckBuildsComplete(BuildingModel buildmodel)
        {
            
            buildmodel.ABuildingComplete -= CheckBuildsComplete;
            BuildingComplete(buildmodel);
                   
            
        }
        
        /// <summary>
        /// ����� �������� ������ ������ � ������������� �� �������� ������ ������ �� ���������������
        /// </summary>
        /// <param name="building"></param>
        public void CheckBuildingsModel(BuildingModel building)
        {            
            if (building is ProduceItemBuildingModel)
            {
                _ProduceItemBuildings.Add((ProduceItemBuildingModel)building);               
            }
            if (building is ResurseProduceBuildingModel)
            {
                _ProduceResurseBuildings.Add((ResurseProduceBuildingModel)building);                
            }
            if (building is WareHouseBuildModel)
            {
                _StockBuildings.Add((WareHouseBuildModel)building);
            }
            if (building is IProduceWorkers)
            {
               
                _ProduceWorkerBuildings.Add((IProduceWorkers)building);
                _globalMaxWorkersValue += ((IProduceWorkers)building).CurrentWorkerValue;
                ChangeMaxWorkerValue?.Invoke(_globalMaxWorkersValue);
            }
            if (building is MarketBuildingModel<ScriptableObject>)
            {
                _MarketsList.Remove((MarketBuildingModel<ScriptableObject>)building);
            }

        }
        
        /// <summary>
        /// ����� �������� ������ �� �������� ������ �� ��������������
        /// </summary>
        /// <param name="building"></param>
        public void DeleteBuildingsModel(BuildingModel building)
        {
            
            if (building is ProduceItemBuildingModel)
            {
                _ProduceItemBuildings.Remove((ProduceItemBuildingModel)building);                
            }
            if (building is ResurseProduceBuildingModel)
            {
                _ProduceResurseBuildings.Remove((ResurseProduceBuildingModel)building);
            }
            if (building is WareHouseBuildModel)
            {
                _StockBuildings.Remove((WareHouseBuildModel)building);
            }
            if (building is IProduceWorkers)
            {
                
                _ProduceWorkerBuildings.Remove((IProduceWorkers)building);
                _globalMaxWorkersValue -= ((IProduceWorkers)building).CurrentWorkerValue;
                ChangeMaxWorkerValue?.Invoke(_globalMaxWorkersValue);
            }
            if (building is MarketBuildingModel<ScriptableObject>)
            {
                _MarketsList.Remove((MarketBuildingModel<ScriptableObject>)building);
            }
        }
        
        /// <summary>
        /// ����� ������� � ��������� ������������� ���������� ���������� ������
        /// </summary>
        /// <returns></returns>
        public int GetGlobalMaxWorkerCount()
        {
            return _globalMaxWorkersValue;
        }
        /// <summary>
        /// ����� �������\����������� ������������ ���������
        /// </summary>
        /// <param name="Time">�����</param>
        public void StartGlobalProducing(float time,GlobalResurseStock stock)
        {
            if (_ProduceItemBuildings!=null&& _ProduceItemBuildings.Count>0)
            {
                foreach(ProduceItemBuildingModel building in _ProduceItemBuildings)
                {
                    building.StartProduce(time);
                    building.GetPaidForProducts(stock);
                }
            }
            if (_ProduceResurseBuildings != null && _ProduceResurseBuildings.Count > 0)
            {
                foreach (ResurseProduceBuildingModel building in _ProduceResurseBuildings)
                {
                    building.StartProduce(time);
                    building.GetPaidForProducts(stock);
                }
            }            
        }
        public void StartGlobalBuild(float time)
        {
            foreach(BuildingModel building in _BuildingsUnderConstraction)
            {
                building.StartBuilding(time);
            }
        }
        #endregion
        
        /// <summary>
        /// ����� "������" ���������� ������ ��� ������ ����
        /// </summary>
        public void ResetGlobalBuildingModel()
        {
            _ActiveBuildings = new List<BuildingModel>();
            _NeedResursesBuildings = new List<BuildingModel>();
            _BuildingsUnderConstraction = new List<BuildingModel>();
            _ProduceResurseBuildings = new List<ResurseProduceBuildingModel>();
            _ProduceItemBuildings = new List<ProduceItemBuildingModel>();
            _ProduceWorkerBuildings = new List<IProduceWorkers>();
            _StockBuildings = new List<WareHouseBuildModel>();
            _MarketsList = new List<MarketBuildingModel<ScriptableObject>>();
            _globalMaxWorkersValue = 0;
            ChangeMaxWorkerValue?.Invoke(_globalMaxWorkersValue);
        }
    }
}
