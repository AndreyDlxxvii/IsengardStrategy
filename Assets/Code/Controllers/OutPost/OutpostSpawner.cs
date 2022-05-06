using System.Collections.Generic;
using Interfaces;
using ResurseSystem;
using Views.BaseUnit.UI;
using Views.Outpost;

namespace Controllers.OutPost
{
    public class OutpostSpawner: IOnController, IOnStart, ISpawnerLogic
    {
        public List<OutPostUnitController> OutPostUnitControllers;
        private UnitUISpawnerTest _unitUISpawnerTest;

        public OutpostSpawner(UnitUISpawnerTest unitUISpawnerTest)
        {
            _unitUISpawnerTest = unitUISpawnerTest;
        }
        
        public void OnStart()
        {
            OutPostUnitControllers = new List<OutPostUnitController>();
        }

        public void SpawnLogic(ISpawnerLogicView unitView)
        {
            var index = OutPostUnitControllers.Count;
            OutPostUnitControllers.Add(new OutPostUnitController(index,(OutpostUnitView)unitView,_unitUISpawnerTest));
        }
    }
}