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
    public sealed class WorkerController: BaseUnitController
    {
        private readonly BaseUnitModel _baseUnitModel;
        private readonly UnitMovement _unitMovement;
        private readonly UnitAnimation _unitAnimation;
        private readonly WorkerView _workerView;
        private readonly ResourcesPlaceController _resourcesPlaceController;
        private List<UnitHandler> _unitHandlers;
        private List<float> _timerPositions;
        private UnitStates _currentUnitState;

        public WorkerController(BaseUnitModel baseUnitModel, UnitMovement unitMovement, UnitAnimation unitAnimation,
             ResourcesPlaceController resourcesPlaceController,WorkerView workerView) :
            base(baseUnitModel, unitMovement, unitAnimation)
        {
            _baseUnitModel = baseUnitModel;
            _unitMovement = unitMovement;
            _unitAnimation = unitAnimation;
            _resourcesPlaceController = resourcesPlaceController;
            _workerView = workerView;
            _workerView.
                AddResource(new ResurseHolder(_resourcesPlaceController.ResurseMine.ResurseHolderMine.ResurseInHolder,
                    20,0));
            _unitHandlers = new List<UnitHandler>();
            _timerPositions = new List<float>();
        }

        public override void SetStateMachine(UnitStates unitStates)
        {
            //_unitMovement.WaitingForNextCommand = false;
            //base.SetStateMachine(unitStates);
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
            _unitMovement.PointWhereToGo.Add(whereToGo);
        }

        public override void SetEndTime(float time)
        {
            _timerPositions.Add(time);
        }

        public bool GetSomeStaff()
        {
            var holder = _workerView.GetResurseOutOfHolder();
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