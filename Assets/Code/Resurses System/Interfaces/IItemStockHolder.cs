using System.Collections;
using System.Collections.Generic;
using EquipmentSystem;
using UnityEngine;


public interface IItemStockHolder
{
    public List<IItemCarrierHolder> ItemStock { get; }

    public void AddItemToStock(IItemCarrierHolder holder);
    public IItemCarrierHolder GetItemFromStock(ItemModel item, int value);
}
