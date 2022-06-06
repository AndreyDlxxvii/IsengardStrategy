using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EquipSystem
{ 
    public interface IItem—arrierHolder 
    {
        public IEquippable Item { get; }
        public int CurrentValue { get; }
        public int MaxItemValue { get; }
    }
}
