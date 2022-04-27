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
        //TODO: hasPath - потестировать
        //if (Math.Abs(Agent.stoppingDistance - _stoppingDistance) > Mathf.Epsilon)
        // {
        //     Agent.stoppingDistance = _stoppingDistance;
        // }
        if (_unitMovement.navMeshAgent.destination.x == _unitMovement.transform.position.x&&
            _unitMovement.navMeshAgent.destination.z == _unitMovement.transform.position.z)
            StoppedAtPosition();
        
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
        base.Handle();
    }

  
}
