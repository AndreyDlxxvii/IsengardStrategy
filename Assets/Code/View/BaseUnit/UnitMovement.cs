using System;
using System.Collections.Generic;
using Enums.BaseUnit;
using UnityEngine;
using UnityEngine.AI;

namespace Views.BaseUnit
{
    public class UnitMovement : MonoBehaviour
    {
        #region Fields

        [SerializeField] private NavMeshAgent _navMeshAgent;
        [NonSerialized] public List<Vector3> PointWhereToGo;
        [NonSerialized] public int CountOfSequence;
        public Action<Vector2> EnterWorkZone = delegate {  };
        public Action<UnitStates> StoppedAtPosition;
        
        #endregion

        #region Properties

        public NavMeshAgent navMeshAgent => _navMeshAgent;

        #endregion

        #region Methods

        private void Start()
        {
            CountOfSequence = 0;
            EnterWorkZone += SetPositionInZone;
        }

        private void OnDestroy()
        {
            EnterWorkZone -= SetPositionInZone;
        }
        
        public void SetThePointWhereToGo()
        {
            _navMeshAgent.SetDestination(PointWhereToGo[CountOfSequence]);
        }

        private void SetPositionInZone(Vector2 endPosition)
        {
            PointWhereToGo[CountOfSequence] = new Vector3(
                PointWhereToGo[CountOfSequence].x+endPosition.x,
                PointWhereToGo[CountOfSequence].y
                ,PointWhereToGo[CountOfSequence].z+endPosition.y);
            _navMeshAgent.SetDestination(PointWhereToGo[CountOfSequence]);
        }

        public bool CalculateZoneOfDestination() //when we have not got the zone to stay, like near mine
        {
            //maybe better use colliders
            var distance = Math.Sqrt( Math.Pow( (transform.position.x-_navMeshAgent.destination.x ), 2 ) + 
                                  Math.Pow( (transform.position.z - _navMeshAgent.destination.z), 2 ) ); 
            if(distance <= 0.1f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public void StopAgent()
        {
            _navMeshAgent.isStopped = true;
        }

        #endregion

     
    }
}