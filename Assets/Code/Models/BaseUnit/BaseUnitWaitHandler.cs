using Controllers;
using Controllers.BaseUnit;


public sealed class BaseUnitWaitHandler : UnitHandler, IOnUpdate
{
    private TimeController _timeController;
    private readonly BaseUnitController _baseUnitController;
    private float _time;

    public BaseUnitWaitHandler(TimeController timeController, float time, BaseUnitController baseUnitController)
    {
        _timeController = timeController;
        _time = time;
        _baseUnitController = baseUnitController;
    }

    public void OnUpdate(float deltaTime)
    {
        _timeController.OnUpdate(deltaTime);
        if (_timeController.TimerIsDone())
            TimeIsUp();
    }
    
    private void TimeIsUp()
    {
        base.Handle();
    }

    public override IUnitHandler Handle()
    {
        _baseUnitController.CurrentUnitHandler = GetCurrent();
        _timeController.AddTimer(_time);
        _timeController.TurnOnTimer();
        return this;
    }

}
