using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EquipSystem
{ 
    public class InventoryModel
    {
        private List<IEquippable> itemList;
        public event EventHandler OnItemListChanged;
        public InventoryModel()
        {
            itemList = new List<IEquippable>();
        }

        public void AddItem(IEquippable item)
        {
            itemList.Add(item);
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }
        public List<IEquippable> GetItemList()
        {
            return itemList;
        }
    }
}