using EquipmentSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResurseSystem
{ 
    [System.Serializable]
    public class ItemStock : Stock<ItemModel,ItemÑarrierHolder>
    {
        public ItemStock (List<ItemÑarrierHolder> models)
        {
            _holdersInStock = new List<ItemÑarrierHolder> (models);
        }
        public ItemStock (ItemStock itStock)
        {
            _holdersInStock = new List<ItemÑarrierHolder>(itStock.HoldersInStock);
        }
    }
}
