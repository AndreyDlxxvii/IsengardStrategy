using System.Collections.Generic;
using Code.View.ResourcesPlace;
using Interfaces;
using Views.BaseUnit.UI;

namespace Controllers.ResouresesPlace
{
    public sealed class ResourcesPlaceSpawner: IOnController,IOnStart,ISpawnerLogic
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

        public void SpawnLogic(ISpawnerLogicView view)
        {
            var index = ResourcesPlaceControllers.Count;
            ResourcesPlaceControllers.Add(new ResourcesPlaceController(index,(ResourcesPlaceView)view,_unitUISpawnerTest));
        }
    }
}