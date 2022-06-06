using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
    public class BuildingsController
    {

        [SerializeField]
        private GlobalBuildingsModels globalBuildingsModels;

        public BuildingsController (GlobalBuildingsModels _globalBuildModels)
        {
            globalBuildingsModels = _globalBuildModels;
        }
        public void OnFixedUpdate(float fixedDeltaTime)
        {
            
        }

        public void OnStart()
        {
            globalBuildingsModels.ResetGlobalBuildingModel();
        }

        public void BuildingComplete (BuildingModel model)
        {
           
        }
    }
}
