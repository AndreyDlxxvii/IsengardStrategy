using System;
using Code.View.ResourcesPlace;
using Interfaces;
using ResurseSystem;
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
        private readonly BuildingView _warehouse;
        public ResurseMine ResurseMine;
        public Action<Vector3,ResourcesPlaceController> Transaction;
        
        public BuildingView Warehouse => _warehouse;
        
        public ResourcesPlaceController(int index,ResourcesPlaceView placeUnitView,UnitUISpawnerTest uiSpawnerTest,BuildingView buildingView)
        {
            PlaceView = placeUnitView;
            UiSpawnerTest = uiSpawnerTest;
            _warehouse = buildingView;
            PlaceView.IndexInArray = index;
            ResurseMine = PlaceView.gameObject.GetComponent<Mineral>().GetMineRes();
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
        
        //TODO: COPY+PAST ALERT
        //NEED SET IT IN UTILS
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
            PlaceView.GetColliderParameters(out Vector3 center, out Vector3 size);
            var positionInZone = CalculatePositionOfPlacementCircle(PlaceView.Data.GetMaxCountOfNPC(),
                counter,size.x,center);
            counter++;
            unitMovement.EnterWorkZone.Invoke(positionInZone);
        }
    }
}