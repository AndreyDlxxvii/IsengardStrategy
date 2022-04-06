using Enums.BaseUnit;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GroupData", menuName = "Group/GroupData", order = 0)]
    public class GroupData : ScriptableObject
    {
        public Vector3 CentralPosition;
        public int MaxUnitInPlace;
    }
}