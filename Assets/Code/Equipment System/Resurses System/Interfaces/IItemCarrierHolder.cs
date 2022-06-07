using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EquipSystem
{ 
    public interface IItemCarrierHolder 
    {
        public IEquippable Item { get; }
        public int CurrentValue { get; }
        public int MaxItemValue { get; }
    }
}
