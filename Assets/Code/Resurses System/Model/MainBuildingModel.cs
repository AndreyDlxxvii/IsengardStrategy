using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Main Building Model", menuName = "Buildings/MainBuildingModel", order = 1)]
    public class MainBuildingModel : WareHouseBuildModel,IProduceWorkers
    {
        public int CurrentWorkerValue => _currentWorkerValue;

        [SerializeField] private int _currentWorkerValue;
        public MainBuildingModel(MainBuildingModel baseBuilding):base(baseBuilding)
        {
            _currentWorkerValue = baseBuilding.CurrentWorkerValue;
        }
    }
}
