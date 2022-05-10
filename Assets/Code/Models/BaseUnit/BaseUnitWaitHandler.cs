using Controllers.BaseUnit;
using UnityEngine;


public sealed class BaseUnitWaitHandler : UnitHandler, IOnUpdate
{
    #region Fields

    private readonly BaseUnitController _baseUnitController;
    private TimeRemaining _timeHolder;
    private float _time;

    #endregion


    #region Ctor

    public BaseUnitWaitHandler(float time, BaseUnitController baseUnitController)
    {
        _time = time;
        _baseUnitController = baseUnitController;
    }

    #endregion


    #region IOnUpdate

    public void OnUpdate(float deltaTime)
    {
        if (CancellationToken && CancellationHandler != null)
        {
            TimeRemainingExtensions.RemoveTimeRemaining(_timeHolder);
            CancellationHandler.Handle();
        }
    }

    #endregion


    #region UnitHandler
    
    public override IUnitHandler Handle()
    {
        _baseUnitController.CurrentUnitHandler = GetCurrent();
        _timeHolder = new TimeRemaining(TimeIsUp, _time, false);
        TimeRemainingExtensions.AddTimeRemaining(_timeHolder);
        return this;
    }

    #endregion
    
    
    #region Methods

    private void TimeIsUp()
    {
        _baseUnitController.CurrentUnitHandler = null;
        base.Handle();
    }

    #endregion
}
