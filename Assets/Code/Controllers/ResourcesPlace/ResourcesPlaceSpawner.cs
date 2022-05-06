﻿using System.Collections.Generic;
using Code.View.ResourcesPlace;
using Interfaces;
using ResurseSystem;
using Views.BaseUnit.UI;

namespace Controllers.ResouresesPlace
{
    public sealed class ResourcesPlaceSpawner: IOnController,IOnStart,ISpawnerLogicWorker
    {
        public List<ResourcesPlaceController> ResourcesPlaceControllers;
        private UnitUISpawnerTest _unitUISpawnerTest;
        
        public ResourcesPlaceSpawner(UnitUISpawnerTest unitUISpawnerTest)
        {
            _unitUISpawnerTest = unitUISpawnerTest;
        }
        
        public void OnStart()
        {
            ResourcesPlaceControllers = new List<ResourcesPlaceController>();
        }

        public void SpawnLogic(ISpawnerLogicView view,BuildingView warehouse)
        {
            var index = ResourcesPlaceControllers.Count;
            ResourcesPlaceControllers.Add(new ResourcesPlaceController(index,(ResourcesPlaceView)view,_unitUISpawnerTest,warehouse));
        }
    }
}