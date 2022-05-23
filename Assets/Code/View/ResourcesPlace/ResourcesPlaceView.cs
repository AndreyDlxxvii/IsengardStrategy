using System;
using Data;
using Interfaces;
using UnityEngine;
using Views.BaseUnit;

namespace Code.View.ResourcesPlace
{
    public sealed class ResourcesPlaceView: MonoBehaviour, ISpawnerLogicView
    {
        public OutpostParametersData Data;
        public Action<UnitMovement> UnitInZone;
        [NonSerialized]
        public bool IsActive = false;
        [NonSerialized]
        public int IndexInArray;

        private void OnTriggerEnter(Collider other)
        {
            var unitMovement = other.gameObject.GetComponent<UnitMovement>();
            if (unitMovement)
            {
                UnitInZone.Invoke(unitMovement);
            }
        }

        public void GetColliderParameters(out Vector3 center,out Vector3 size)
        {
            var collider = gameObject.GetComponent<BoxCollider>();
            center = collider.center;
            size = collider.size;
        }
    }
}