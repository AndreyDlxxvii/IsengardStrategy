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
        public Action EnterWorkZone = delegate {  };
        
        #endregion


        #region Methods

        private void Start()
        {
            EnterWorkZone += StopAgent;
        }

        private void OnDestroy()
        {
            EnterWorkZone -= StopAgent;
        }

        public void SetThePointWhereToGo()
        {
            _navMeshAgent.SetDestination(pointWhereToGo);
        }

        public void StopAgent()
        {
            _navMeshAgent.isStopped = true;
        }

        #endregion
        
    }
}