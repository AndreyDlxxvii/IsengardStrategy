using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Views.BaseUnit.UI;
using Views.Outpost;

namespace Controllers.OutPost
{
    public class OutpostSpawner: MonoBehaviour
    {
        [NonSerialized]
        public List<OutPostUnitController> OutPostUnitControllers;
        public UnitUISpawnerTest UnitUISpawnerTest;

        private void Awake()
        {
            OutPostUnitControllers = new List<OutPostUnitController>();
        }

        public void SpawnLogic(OutpostUnitView unitView)
        {
            var index = OutPostUnitControllers.Count;
            OutPostUnitControllers.Add(new OutPostUnitController(index,unitView));
            OutPostUnitControllers[index].UiSpawnerTest = UnitUISpawnerTest;
            OutPostUnitControllers[index].Initialize();
        }
    }
}