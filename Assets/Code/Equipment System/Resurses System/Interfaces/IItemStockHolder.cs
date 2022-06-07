using System.Collections;
using System.Collections.Generic;
using EquipSystem;
using UnityEngine;


public interface IItemStockHolder
{
    public List<IItemCarrierHolder> ItemStock { get; }

    public void AddItemToStock(IItemCarrierHolder holder);
    public IItemCarrierHolder GetItemFromStock(IEquippable item, int value);
}
