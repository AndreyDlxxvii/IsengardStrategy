using System;
using Data;
using Interfaces;
using UnityEngine;
using Views.BaseUnit.UI;
using Views.Outpost;

namespace Controllers.OutPost
{
    public class OutPostUnitController: IInitialization, IDisposable
    {
        private int index;
        private int _currentCountOfNPC = 0;
        public UnitUISpawnerTest UiSpawnerTest;
        public OutpostUnitView OutpostUnitView;
        public Action<Vector3> Transaction = delegate {  };
        
        
        public OutPostUnitController(int index,OutpostUnitView outpostUnitView)
        {
            OutpostUnitView = outpostUnitView;
            OutpostUnitView.IndexInArray = index;
        }
        
        public void Initialize()
        {
            UiSpawnerTest.spawnUnit += BuyAUnit;
        }

        public void Dispose()
        {
            UiSpawnerTest.spawnUnit -= BuyAUnit;
        }
        
        private void BuyAUnit(OutPostUnitController outPostUnitController)
        {
            if (this != outPostUnitController)
            {
                return;
            }
            if (OutpostUnitView.OutpostParametersData.GetMaxCountOfNPC() >
                _currentCountOfNPC)
            {
                _currentCountOfNPC++;
                Transaction.Invoke(OutpostUnitView.gameObject.transform.position);
            }
        }
    }
}