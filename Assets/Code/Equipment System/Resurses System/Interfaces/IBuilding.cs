using ResurseSystem;
using System;
using UnityEngine;

public interface IBuildingModel :IIconHolder,ISelectable,IHealthHolder,INameHolder
{
    public ResurseCost ThisBuildingCost { get; }
    public ResurseStock ThisBuildingStock { get; }
    public GameObject BasePrefab { get; }
    public GameObject GotBuildPrefab { get; }


}
