using System;
using UnityEngine;
using UnityEngine.AI;

namespace Views.BaseUnit
{
    public class UnitMovement : MonoBehaviour
    {
        #region Fields

        [SerializeField] private NavMeshAgent _navMeshAgent;
        [NonSerialized] public Vector3 pointWhereToGo;
        public NavMeshAgent navMeshAgent => _navMeshAgent;

        #endregion


        #region Methods

        public void SetThePointWhereToGo()
        {
            _navMeshAgent.SetDestination(pointWhereToGo);
        }

        #endregion
        
    }
}