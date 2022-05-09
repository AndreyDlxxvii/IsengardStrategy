using System;
using System.Collections.Generic;
using Controllers.BaseUnit;
using Controllers.ResouresesPlace;
using Data;
using Enums.BaseUnit;
using Models.BaseUnit;
using ResurseSystem;
using UnityEngine;
using Views.BaseUnit;

namespace Controllers.Worker
{
    public sealed class WorkerController: BaseUnitController,IOnStart, IOnUpdate, IOnLateUpdate, IDisposable
    {
        private readonly BaseUnitModel _baseUnitModel;
        private readonly UnitMovement _unitMovement;
        private readonly UnitAnimation _unitAnimation;
        private readonly WorkerView _workerView;
        private ResourcesPlaceController _resourcesPlaceController;
        private List<UnitHandler> _unitHandlers;
        private List<float> _timerPositions;
        private UnitStates _currentUnitState;

        public WorkerView WorkerView => _workerView;

        public ResourcesPlaceController ResourcesPlaceController
        {
            get => _resourcesPlaceController;
            set => _resourcesPlaceController = value;
        }
        
        public WorkerController(BaseUnitModel baseUnitModel, UnitMovement unitMovement, UnitAnimation unitAnimation,
             WorkerView workerView) :
            base(baseUnitModel, unitMovement, unitAnimation)
        {
            _baseUnitModel = baseUnitModel;
            _unitMovement = unitMovement;
            _unitAnimation = unitAnimation;
            //_resourcesPlaceController = resourcesPlaceController;
            _workerView = workerView;
            _unitHandlers = new List<UnitHandler>();
            _timerPositions = new List<float>();
        }
        
        #region Interfaces

        public void OnStart()
        {
            _unitMovement.StoppedAtPosition += SetStateMachine;
        }

        public void OnUpdate(float deltaTime)
        {
           
            Check(deltaTime);
        }
        
        public void OnLateUpdate(float deltaTime)
        {
            
        }

        public void Dispose()
        {
            _unitMovement.StoppedAtPosition += SetStateMachine;
        }
        
        #endregion

        public override void SetStateMachine(UnitStates unitStates)
        {
            _currentUnitState = unitStates;
            switch (_currentUnitState)
            {
                case UnitStates.IDLE:
                    //Anim state, looking for target, waiting destination
                    break;
            
                case UnitStates.MOVING:
                    //AnimState
                    break;
                case UnitStates.DEAD:
                    //AnimeState, destroy
                    break;
            }
        }

        public override void SetUnitSequence(List<UnitStates> workerActionsList)
        {
            int timerCount = 0;
            foreach (var workerAction in workerActionsList)
            {
               
                switch (workerAction)
                {
                        case UnitStates.MOVING:
                            //_unitHandlers.Add(new BaseUnitDoActionHandler(SetStateMachine));
                            _unitHandlers.Add(new BaseUnitMoveHandler(_unitMovement, this));
                            MoveCounter++;
                            break;
                        case UnitStates.ATTAKING:
                            _unitHandlers.Add(new BaseUnitWaitHandler(_timerPositions[timerCount], this));
                            timerCount++;
                            break;
                        case UnitStates.GETSTAFF:
                            _unitHandlers.Add(new BaseUnitDoActionHandler(GetSomeStaff));
                            break;
                        case UnitStates.SETSTAFF:
                            _unitHandlers.Add(new BaseUnitDoActionHandler(PutSomeStaff));
                            break;
                }
            }
            _unitHandlers[0].Handle();
            for (int i = 1; i < _unitHandlers.Count; i++)
            {
                if (i != _unitHandlers.Count)
                    _unitHandlers[i - 1].SetNext(_unitHandlers[i]);
            }
            _unitHandlers[_unitHandlers.Count - 1].SetNext(_unitHandlers[0]);
        }

        public override void SetDestination(Vector3 whereToGo)
        {
            if (_unitMovement.PointWhereToGo==null)
                _unitMovement.PointWhereToGo = new List<Vector3>();
            _unitMovement.PointWhereToGo.Add(whereToGo);
        }

        public override void SetEndTime(float time)
        {
            _timerPositions.Add(time);
        }

        public bool GetSomeStaff()
        {
            var holder = _workerView.GetResurseOutOfHolder();
            // сделать остановку скрипта.
            var countOfAdding = _resourcesPlaceController.ResurseMine.MinedResurse(holder.MaxResurseCount);
            holder.AddResurse(countOfAdding,out var addedRes);
            _workerView.AddResource(holder);
            return true;
        }

        
        
        public bool PutSomeStaff()
        {
            var resource= _workerView.GetResurseOutOfHolder();
            _resourcesPlaceController.Warehouse.GetStock().
                AddResursesCount(resource.ResurseInHolder.ResurseType,resource.CurrentResurseCount);
            resource.AddResurse(-resource.MaxResurseCount,out var addedRes);
            _workerView.AddResource(resource);
            return true;
        }

        public override void Check(float deltaTime)
        {
            if (CurrentUnitHandler is BaseUnitMoveHandler moveHandler)
            {
                moveHandler.OnUpdate(deltaTime);
            }
        }
    }
}