using EquipmentSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResurseSystem
{ 
    [System.Serializable]
    public class ItemStock : Stock<ItemModel,Item�arrierHolder>
    {
        public ItemStock (List<Item�arrierHolder> models)
        {
            _holdersInStock = new List<Item�arrierHolder> (models);
        }
        public ItemStock (ItemStock itStock)
        {
            _holdersInStock = new List<Item�arrierHolder>(itStock.HoldersInStock);
        }
    }
}
