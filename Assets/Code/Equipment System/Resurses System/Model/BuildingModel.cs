using ResurseSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{ 
    [System.Serializable]    
    public abstract class BuildingModel : ScriptableObject, IBuildingModel, IResurseStockHolder
    {
        public ResurseCost ThisBuildingCost => _thisBuildinCost;
        public GameObject BasePrefab => _thisBuildingPrefab;
        public ResurseStock ThisBuildingStock => _thisBuildingStock;
        public string Name => _nameBuilding;
        public float Health => _currentHealth;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;

        public GameObject GotBuildPrefab => _gotBuildPrefab;

        public float BuildingTime => _buildingTime;

        public float CurrentBuildTime => _currentBuildTime;

        public Action<BuildingModel> AStartBuilding;
        public Action<BuildingModel> ABuildingComplete;

        [SerializeField] private ResurseCost _thisBuildinCost;        
        private GameObject _thisBuildingPrefab;
        [SerializeField] private ResurseStock _thisBuildingStock;
        [SerializeField] private float _currentHealth;
        [SerializeField] private float _maxHealth;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _nameBuilding;
        [SerializeField] private GameObject _gotBuildPrefab;
        [SerializeField] private float _buildingTime;
        [SerializeField] private float _currentBuildTime;
        [SerializeField] private GameObject _notGotBuildPrefab;
        private List<ResurseHolder> backUpStockHolders;
               

        public BuildingModel(BuildingModel baseBuilding)
        {
            _thisBuildinCost = new ResurseCost(baseBuilding.ThisBuildingCost);
            _thisBuildingStock = new ResurseStock(baseBuilding.ThisBuildingStock);
            backUpStockHolders = new List<ResurseHolder>(_thisBuildingStock.ResursesInStock);           
            _currentHealth = baseBuilding.Health;
            _maxHealth = baseBuilding.MaxHealth;
            _icon = baseBuilding.Icon;
            _thisBuildingPrefab = baseBuilding._thisBuildingPrefab;
            _gotBuildPrefab = baseBuilding._gotBuildPrefab;
            _buildingTime = baseBuilding.BuildingTime;
            _notGotBuildPrefab = baseBuilding._notGotBuildPrefab;
            _currentBuildTime = 0;
            AwakeModel();
        }
        public void SetName(string name)
        {
            _nameBuilding=name;
        }

        public void AwakeModel()
        {
            if (!ThisBuildingCost.PricePaidFlag)
            {
                _thisBuildingStock.ChangeHoldersInStock(ThisBuildingCost.CoastInResurse);
                _thisBuildingPrefab = _notGotBuildPrefab;
            }
        }
        public void StartBuilding(float value)
        {
            _currentBuildTime += value;
            if (CurrentBuildTime >= BuildingTime)
            {
                _thisBuildingPrefab = _gotBuildPrefab;
                _thisBuildingStock.ChangeHoldersInStock(backUpStockHolders);
                _currentBuildTime = 0;
                ABuildingComplete?.Invoke(this);
            }
        }

        public abstract void AddResurseInStock(IResurseHolder holder);

        public IResurseHolder GetResursesInStock(IResurse resurse, int value)
        {
            int tempResValue = _thisBuildingStock.GetResursesInStock(resurse.ResurseType, value);
            IResurseHolder tempResHolder = new ResurseHolder((ResurseCraft)resurse, tempResValue, tempResValue);
            return tempResHolder;
        }

        public void GetCostForBuilding()
        {
            if (ThisBuildingCost.PricePaidFlag)
            {
                ThisBuildingCost.GetNeededResurse(ThisBuildingStock);
                if (ThisBuildingCost.PricePaidFlag)
                {
                    AStartBuilding?.Invoke(this);
                }
            }
        }
    }
}
