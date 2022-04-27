using Interfaces;
using UnityEngine;

namespace Models.BaseUnit
{
    public class BaseUnitFactory: IUnitFactory
    {
        public GameObject CreateUnit(GameObject whichPrefab , Vector3 whereToPlace)
        {
            return GameObject.Instantiate(whichPrefab,whereToPlace,new Quaternion());
        }
    }
}