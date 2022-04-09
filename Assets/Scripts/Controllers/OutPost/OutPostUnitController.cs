using System;
using Data;
using Interfaces;
using UnityEngine;
using Views.BaseUnit.UI;
using Views.Outpost;

namespace Controllers.OutPost
{
    public class OutPostUnitController: IInitialization
    {
        public UnitUISpawnerTest UiSpawnerTest;
        public OutpostUnitView OutpostUnitView;
        public Action<Vector3> Transaction = delegate {  };
        
        public void Initialize()
        {
            UiSpawnerTest.spawnUnit += BuyAUnit;
        }

        private void BuyAUnit()
        {
            if (OutpostUnitView.OutpostParametersData.GetMaxCountOfNPC() >
                OutpostUnitView.OutpostParametersData.GetCurrentCountOfNPC())
            {
                OutpostUnitView.OutpostParametersData.AddCurrentCountOfNPC(1);
                Transaction.Invoke(OutpostUnitView.gameObject.transform.position);
            }
        }
    }
}