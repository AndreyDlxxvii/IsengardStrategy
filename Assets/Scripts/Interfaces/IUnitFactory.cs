using Enums.BaseUnit;
using Models.BaseUnit;
using UnityEngine;

namespace Interfaces
{
    public interface IUnitFactory
    {
        public GameObject CreateUnit(GameObject whichPrefab, Transform whereToPlace);
    }
}