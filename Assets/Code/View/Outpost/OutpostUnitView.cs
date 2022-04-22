using System;
using Data;
using UnityEngine;
using Views.BaseUnit;

namespace Views.Outpost
{
    public class OutpostUnitView : MonoBehaviour
    {
        public OutpostParametersData OutpostParametersData;
        public Action<UnitMovement> UnitInZone;
        public int IndexInArray;

        private void OnCollisionEnter(Collision other)
        {
            var unitMovement = other.gameObject.GetComponent<UnitMovement>();
            if (unitMovement)
            {
                UnitInZone.Invoke(unitMovement);
            }
        }
    }
}