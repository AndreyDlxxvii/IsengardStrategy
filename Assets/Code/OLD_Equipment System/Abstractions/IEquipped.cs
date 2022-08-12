using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EquipSystem
{ 
    public interface IEquipped 
    {
        public EquipType HolderType { get; }
        public GameObject EquipHolderObject { get; }
        public IEquippable EquippableItem { get; }
    


        public void GetEquipped(IEquippable item);
        public void UnEquiped(IEquippable item);

        public abstract void UnEquiped();
    }
}