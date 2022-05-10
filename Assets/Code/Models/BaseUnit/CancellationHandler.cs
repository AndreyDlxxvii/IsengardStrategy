namespace Models.BaseUnit
{
    public sealed class CancellationHandler: UnitHandler
    {
        private readonly UnitHandler _unitHandler;

        public CancellationHandler(UnitHandler unitHandler)
        {
            _unitHandler = unitHandler;
        }
        
        public override IUnitHandler Handle()
        {
            _unitHandler.Handle();
            return this;
        }
    }
}