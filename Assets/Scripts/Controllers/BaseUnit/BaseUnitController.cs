using System;
using Enums.BaseUnit;
using Models.BaseUnit;
using UnityEngine;

namespace Controllers.BaseUnit
{
    public class BaseUnitController
    {
        public BaseUnitModel UnitModel;

        public BaseUnitController()
        {
            UnitModel = new BaseUnitModel();
        }

        public virtual void SetStateMachine(UnitStates unitStates)
        {
            switch (unitStates)
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
    }
}