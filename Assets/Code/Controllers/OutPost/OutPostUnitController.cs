using System;
using Interfaces;
using UnityEngine;
using Views.BaseUnit;
using Views.BaseUnit.UI;
using Views.Outpost;

namespace Controllers.OutPost
{
    public class OutPostUnitController: IOnController, IDisposable, IUnitMovementDetected, IBuyUnit,IUnitSpawner
    {
        private int index;
        private int _currentCountOfNPC = 0;
        public UnitUISpawnerTest UiSpawnerTest;
        public OutpostUnitView OutpostUnitView;
        public Action<Vector3,OutPostUnitController> Transaction;

        public OutPostUnitController(int index,OutpostUnitView outpostUnitView,UnitUISpawnerTest uiSpawnerTest)
        {
            OutpostUnitView = outpostUnitView;
            OutpostUnitView.IndexInArray = index;
            OutpostUnitView.UnitInZone += ViewDetection;
            UiSpawnerTest = uiSpawnerTest;
            UiSpawnerTest.spawnUnit += BuyAUnit;
        }

        public void Dispose()
        {
            OutpostUnitView.UnitInZone -= ViewDetection;
            UiSpawnerTest.spawnUnit -= BuyAUnit;
        }
        
        public void BuyAUnit(IUnitSpawner UnitController)
        {
            if (this != UnitController)
            {
                return;
            }
            if (OutpostUnitView.OutpostParametersData.GetMaxCountOfNPC() >
                _currentCountOfNPC)
            {
                _currentCountOfNPC++;
                Transaction.Invoke(OutpostUnitView.gameObject.transform.position,this);
            }
        }

        private Vector2 CalculatePositionOfPlacementCircle(float maxCount, int currentNumberOfPoint,
            double radius, Vector3 center)
        {
            float x = (float)(Math.Cos(2 * Math.PI * currentNumberOfPoint / maxCount) * radius + center.x);
            float z = (float)(Math.Sin(2 * Math.PI * currentNumberOfPoint / maxCount) * radius + center.z);
            return new Vector2(x, z);
        }

        private int counter = 0;

        public void ViewDetection(UnitMovement unitMovement)
        {
            OutpostUnitView.GetColliderParameters(out Vector3 center, out Vector3 size);
            var positionInZone = CalculatePositionOfPlacementCircle(OutpostUnitView.OutpostParametersData.GetMaxCountOfNPC(),
                counter,size.x,center);
            counter++;
            unitMovement.EnterWorkZone.Invoke(positionInZone);
        }

      
    }
}