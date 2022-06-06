using ResurseSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New WareHouse Building Model", menuName = "Buildings/WareHouseBuildingModel", order = 1)]
    public class WareHouseBuildModel : BuildingModel
    {
    

        public WareHouseBuildModel (BuildingModel baseBuilding):base(baseBuilding)
        {

        }

        public override void AddResurseInStock(IResurseHolder holder)
        {
            ThisBuildingStock.AddResursesFromHolder(holder);
            GetCostForBuilding();
        }
    }
}
