public interface IUnitHandler
{
        IUnitHandler Handle();
        IUnitHandler SetNext(IUnitHandler nextHandler);

        void SetCancellationToken(IUnitHandler cancellationToken);
        
        void SetCancellationTokenFlag(bool flag);
}
