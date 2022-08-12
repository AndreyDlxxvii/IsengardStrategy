using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EquipSystem;
public interface IStateHolder : IAttacked,IHealthHolder,ISelectable,IEquipped
{
    public int MaxLevel{ get; }
    public int CurrLevel { get; }
    public int Strength { get; }
    public int Intelligence { get; }
    public int Stamina { get; }
    public int Dexterity { get; }


}
