using Enums.BaseUnit;
using Models.BaseUnit;
using UnityEngine;
using Views.BaseUnit;

namespace Controllers.BaseUnit
{
    public class BaseUnitController: IOnController,IOnStart, IOnUpdate
    {
        #region Fields

        private UnitStates _currentUnitState;
        private BaseUnitModel _unitModel;
        private UnitMovement _unitMovementView;
        private UnitAnimation _unitAnimation;

        #endregion

        #region Ctor

        public BaseUnitController(BaseUnitModel baseUnitModel, UnitMovement unitMovement, UnitAnimation unitAnimation)
        {
            _unitModel = baseUnitModel;
            _unitMovementView =  unitMovement;
            _unitAnimation = unitAnimation;
        }

        #endregion


        #region Interfaces

        public void OnStart()
        {
            
        }

        public void OnUpdate(float deltaTime)
        {
            
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

        public void SetDestination(Vector3 whereToGo)
        {
            _unitMovementView.pointWhereToGo = whereToGo;
            _unitMovementView.SetThePointWhereToGo();
        }
        
        #endregion
    }
}