using System;
using Code.View.ResourcesPlace;
using Interfaces;
using UnityEngine;
using Views.BaseUnit;
using Views.BaseUnit.UI;


namespace Controllers.ResouresesPlace
{
    public class ResourcesPlaceController: IOnController, IDisposable, IUnitMovementDetected,IBuyUnit,IUnitSpawner
    {
        private int index;
        private int _currentCountOfNPC = 0;
        public ResourcesPlaceView PlaceView;
        public UnitUISpawnerTest UiSpawnerTest;
        public Action<Vector3,ResourcesPlaceController> Transaction;
        
        public ResourcesPlaceController(int index,ResourcesPlaceView placeUnitView,UnitUISpawnerTest uiSpawnerTest)
        {
            PlaceView = placeUnitView;
            UiSpawnerTest = uiSpawnerTest;
            PlaceView.IndexInArray = index;
            PlaceView.UnitInZone += ViewDetection;
            UiSpawnerTest.spawnUnit += BuyAUnit;
        }
        
        public void Dispose()
        {
            UiSpawnerTest.spawnUnit -= BuyAUnit;
            PlaceView.UnitInZone -= ViewDetection;
        }

        public void BuyAUnit(IUnitSpawner UnitController)
        {
            if (this != UnitController)
            {
                return;
            }
            if (PlaceView.Data.GetMaxCountOfNPC() >
                _currentCountOfNPC)
            {
                _currentCountOfNPC++;
                Transaction.Invoke(PlaceView.gameObject.transform.position,this);
            }
        }

        public void ViewDetection(UnitMovement unitMovement)
        {
            throw new NotImplementedException();
        }
    }
}