using Controllers.BaseUnit;
using UnityEngine;

namespace Models.BaseUnit
{
    public sealed class CancellationHandler: UnitHandler
    {
        private readonly UnitHandler _unitHandler;
        private readonly BaseUnitController _baseUnitController;

        public CancellationHandler(UnitHandler unitHandler,BaseUnitController baseUnitController)
        {
            _unitHandler = unitHandler;
            _baseUnitController = baseUnitController;
        }
        
        public override IUnitHandler Handle()
        {
            Debug.Log("Handle CancellationHandler");
            _baseUnitController.CurrentUnitHandler = GetCurrent();
            _unitHandler.Handle();
            return this;
        }
    }
}