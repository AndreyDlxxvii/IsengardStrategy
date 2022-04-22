using System;
using UnityEngine;
using Views.BaseUnit;
using Views.BaseUnit.UI;
using Views.Outpost;

namespace Controllers.OutPost
{
    public class OutPostUnitController: IOnController, IDisposable
    {
        private int index;
        private int _currentCountOfNPC = 0;
        public UnitUISpawnerTest UiSpawnerTest;
        public OutpostUnitView OutpostUnitView;
        public Action<Vector3> Transaction;

        public OutPostUnitController(int index,OutpostUnitView outpostUnitView,UnitUISpawnerTest uiSpawnerTest)
        {
            OutpostUnitView = outpostUnitView;
            OutpostUnitView.IndexInArray = index;
            OutpostUnitView.UnitInZone += OutpostViewDetection;
            UiSpawnerTest = uiSpawnerTest;
            UiSpawnerTest.spawnUnit += BuyAUnit;
        }

        public void Dispose()
        {
            OutpostUnitView.UnitInZone -= OutpostViewDetection;
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

        private void OutpostViewDetection(UnitMovement unitMovement)
        {
            unitMovement.EnterWorkZone.Invoke();
        }
        
    }
}