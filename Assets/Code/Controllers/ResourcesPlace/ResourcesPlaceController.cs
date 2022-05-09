using System;
using System.Collections.Generic;
using System.Linq;
using Code.View.ResourcesPlace;
using Controllers.Worker;
using Interfaces;
using ResurseSystem;
using UnityEngine;
using Views.BaseUnit;
using Views.BaseUnit.UI;


namespace Controllers.ResouresesPlace
{
    public class ResourcesPlaceController: IOnController, IDisposable, IUnitMovementDetected,IUnitSpawner
    {
        private int index;
        private int _currentCountOfNPC = 0;
        private readonly BuildingView _warehouse;
        private readonly UnitUISpawnerTest _unitUISpawnerTest;
        private List<WorkerController> _workerControllers;
        public ResourcesPlaceView PlaceView;
        public ResurseMine ResurseMine;
        public Action<Vector3,ResourcesPlaceController> AddUnitToMine;
        public Action<ResourcesPlaceController> LessUnitFromMine;
        
        public BuildingView Warehouse => _warehouse;

        public ResourcesPlaceController(int index,ResourcesPlaceView placeUnitView,BuildingView buildingView, UnitUISpawnerTest unitUISpawnerTest)
        {
            PlaceView = placeUnitView;
            _warehouse = buildingView;
            _unitUISpawnerTest = unitUISpawnerTest;
            PlaceView.IndexInArray = index;
            ResurseMine = PlaceView.gameObject.GetComponent<Mineral>().GetMineRes();
            _unitUISpawnerTest.addUnit += SendUnit;
            _unitUISpawnerTest.lessUnit += LessUnit;
            PlaceView.UnitInZone += ViewDetection;
            _workerControllers = new List<WorkerController>();
        }
        
        public void Dispose()
        {
            _unitUISpawnerTest.addUnit -= SendUnit;
            _unitUISpawnerTest.lessUnit -= LessUnit;
            PlaceView.UnitInZone -= ViewDetection;
        }

        public void AddNewUnit(WorkerController workerController)
        {
            Debug.Log("!");
            _workerControllers.Add(workerController);
        }

        public WorkerController GetLastUnit()
        {
            return _workerControllers.Last();
        }

        public void DeleteLastUnitFromList()
        {
            _workerControllers.Remove(_workerControllers.Last());
        }
        
        public void SendUnit()
        {
            if (PlaceView.Data.GetMaxCountOfNPC() >
                _currentCountOfNPC)
            {
                _currentCountOfNPC++;
                AddUnitToMine.Invoke(PlaceView.gameObject.transform.position,this);
            }
        }

        public void LessUnit()
        {
            if (_currentCountOfNPC <= 0)
            {
                return;
            }
            _currentCountOfNPC--;
            LessUnitFromMine.Invoke(this);
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