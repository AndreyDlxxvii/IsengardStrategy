using Controllers.BaseUnit;
using Enums.BaseUnit;
using Interfaces;
using UnityEngine;

namespace Models.BaseUnit
{
    public class BaseUnitFactory: IUnitFactory
    {
        public GameObject CreateUnit(GameObject whichPrefab , Transform whereToPlace)
        {
            var gameObject = GameObject.Instantiate(whichPrefab,whereToPlace.position,whereToPlace.rotation);
            return gameObject;
        }
    }
}