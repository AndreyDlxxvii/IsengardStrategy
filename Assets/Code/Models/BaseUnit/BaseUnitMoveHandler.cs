using System;
using Controllers.BaseUnit;
using UnityEngine;
using UnityEngine.AI;
using Views.BaseUnit;

public sealed class BaseUnitMoveHandler: UnitHandler, IOnUpdate
{
    private readonly UnitMovement _unitMovement;
    private readonly BaseUnitController _baseUnitController;


    public BaseUnitMoveHandler(UnitMovement unitMovement,BaseUnitController baseUnitController)
    {
        _unitMovement = unitMovement;
        _baseUnitController = baseUnitController;
   
    }

    public void OnUpdate(float deltaTime)
    {
        if (!CancellationToken&&CancellationHandler==null)
        {
            _unitMovement.navMeshAgent.autoTraverseOffMeshLink = false;
            if (_unitMovement.navMeshAgent.isOnOffMeshLink)
            {
                OffMeshLinkData data = _unitMovement.navMeshAgent.currentOffMeshLinkData;
                Vector3 endPos = data.endPos;
                _baseUnitController.NormalSpeed(_unitMovement.navMeshAgent, endPos, deltaTime);

            }
            else if (_unitMovement.CalculateZoneOfDestination())
            {
                StoppedAtPosition();
            }
        }
        else
        {
            CancellationHandler.Handle();
        }

    }
      
    public override IUnitHandler Handle()
    {
        _baseUnitController.CurrentUnitHandler = GetCurrent();
        _unitMovement.SetThePointWhereToGo();
        return this;
    }
    
    
    private void StoppedAtPosition()
    {
        if (_unitMovement.CountOfSequence+1 < _baseUnitController.MoveCounter)
            _unitMovement.CountOfSequence++;
        else
            _unitMovement.CountOfSequence = 0;
        _baseUnitController.CurrentUnitHandler = null;
        base.Handle();
    }

  
}
