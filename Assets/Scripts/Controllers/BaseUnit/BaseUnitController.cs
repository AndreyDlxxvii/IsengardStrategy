using System;
using Enums.BaseUnit;
using Interfaces;
using Models.BaseUnit;
using UnityEngine;
using Views.BaseUnit;

namespace Controllers.BaseUnit
{
    public class BaseUnitController: IInitialization, IExecute
    {
        #region Fields

        private UnitStates _currentUnitState;
        private BaseUnitModel _unitModel;
        public UnitMovement UnitMovementView;
        public UnitAnimation UnitAnimation;

        #endregion

        #region Ctor

        public BaseUnitController(BaseUnitModel baseUnitModel, UnitMovement unitMovement, UnitAnimation unitAnimation)
        {
            _unitModel = baseUnitModel;
            UnitMovementView =  unitMovement;
            UnitAnimation = unitAnimation;
        }

        #endregion


        #region Interfaces

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Methods

        public virtual void SetStateMachine(UnitStates unitStates)
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

        #endregion
        
    }
}