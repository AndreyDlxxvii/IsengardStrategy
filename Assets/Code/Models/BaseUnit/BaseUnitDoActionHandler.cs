using System;

namespace Models.BaseUnit
{
    public class BaseUnitDoActionHandler: UnitHandler
    {
        private readonly Func<bool> _whatToDo;

        public BaseUnitDoActionHandler(Func<bool> whatToDo)
        {
            _whatToDo = whatToDo;
        }

        public void DoActionBool()
        {
            _whatToDo();
            base.Handle();
        }
        
        public override IUnitHandler Handle()
        {
            DoActionBool();
            return this;
        }
    }
}