using System;
using System.Collections.Generic;
using Enums.BaseUnit;
using UnityEngine;

namespace Controllers.BaseUnit
{
    public class UnitController: IOnController, IOnStart, IDisposable
    {

        #region Fields

        private List<BaseUnitController> _baseUnitControllers;
        public BaseUnitSpawner BaseUnitSpawner;

        #endregion


        #region UnityMethods

        public UnitController()
        {
            _baseUnitControllers = new List<BaseUnitController>();
        }
        
        public void OnStart()
        {
            BaseUnitSpawner.unitWasSpawned += SetCommandLooking;
        }
        
        public void Dispose()
        {
            BaseUnitSpawner.unitWasSpawned -= SetCommandLooking;
        }

        #endregion


        #region Methods

        private void SetCommandLooking(int id, Vector3 endPos)
        {
            SetEndPosition(id,endPos);
        }

        public List<BaseUnitController> GetBaseUnitController()
        {
            return _baseUnitControllers;
        }

        private void SetEndPosition(int id, Vector3 endpos)
        {
            _baseUnitControllers[id].SetStateMachine(UnitStates.MOVING);
            _baseUnitControllers[id].SetDestination(endpos);
        }

        #endregion
    }
}