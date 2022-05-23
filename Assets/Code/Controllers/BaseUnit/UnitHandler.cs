using UnityEngine;

public class UnitHandler: IUnitHandler
{
    #region Fields

    private IUnitHandler _nextHandler;
    private bool _cancellationToken = false;
    private IUnitHandler _cancellationHandler;

    #endregion

    #region Properties

    public bool CancellationToken => _cancellationToken;
    public IUnitHandler CancellationHandler => _cancellationHandler;

    #endregion

    #region IUnitHandler

    public virtual IUnitHandler Handle()
    {
        if (_nextHandler != null)
        {
            _nextHandler.Handle();
        }
        return _nextHandler;
    }

    public IUnitHandler SetNext(IUnitHandler nextHandler)
    {
        _nextHandler = nextHandler;
        return nextHandler;
    }

    public void SetCancellationToken(IUnitHandler cancellationToken)
    {
        _cancellationHandler = cancellationToken;
    }

    public void SetCancellationTokenFlag(bool flag)
    {
        _cancellationToken = flag;
    }

    #endregion

    #region Methods

    public IUnitHandler GetCurrent()
    {
        return this;
    }

    #endregion
    
}
